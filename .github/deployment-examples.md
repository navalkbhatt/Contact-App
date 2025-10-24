# Deployment Configuration Examples

## Azure App Service Deployment

### Using Azure CLI
```bash
# Login to Azure
az login

# Create resource group
az group create --name ContactApp-RG --location "East US"

# Create App Service plan
az appservice plan create --name ContactApp-Plan --resource-group ContactApp-RG --sku B1 --is-linux

# Create Web App
az webapp create --resource-group ContactApp-RG --plan ContactApp-Plan --name contact-app-api-prod --deployment-container-image-name ghcr.io/navalkbhatt/contact-app-api:latest

# Configure app settings
az webapp config appsettings set --resource-group ContactApp-RG --name contact-app-api-prod --settings ASPNETCORE_ENVIRONMENT=Production
```

### Using GitHub Actions (Add to deploy jobs)
```yaml
- name: Deploy to Azure App Service
  uses: azure/webapps-deploy@v2
  with:
    app-name: 'contact-app-api-prod'
    slot-name: 'production'
    publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
    images: 'ghcr.io/${{ github.repository_owner }}/contact-app-api:latest'
```

## AWS ECS Deployment

### Task Definition (task-definition.json)
```json
{
  "family": "contact-app-api",
  "networkMode": "awsvpc",
  "requiresCompatibilities": ["FARGATE"],
  "cpu": "256",
  "memory": "512",
  "executionRoleArn": "arn:aws:iam::ACCOUNT:role/ecsTaskExecutionRole",
  "containerDefinitions": [
    {
      "name": "contact-app-api",
      "image": "ghcr.io/navalkbhatt/contact-app-api:latest",
      "portMappings": [
        {
          "containerPort": 5000,
          "protocol": "tcp"
        }
      ],
      "environment": [
        {
          "name": "ASPNETCORE_ENVIRONMENT",
          "value": "Production"
        }
      ],
      "logConfiguration": {
        "logDriver": "awslogs",
        "options": {
          "awslogs-group": "/ecs/contact-app-api",
          "awslogs-region": "us-east-1",
          "awslogs-stream-prefix": "ecs"
        }
      }
    }
  ]
}
```

### GitHub Actions ECS Deploy
```yaml
- name: Deploy to Amazon ECS
  uses: aws-actions/amazon-ecs-deploy-task-definition@v1
  with:
    task-definition: task-definition.json
    service: contact-app-service
    cluster: contact-app-cluster
    wait-for-service-stability: true
```

## Kubernetes Deployment

### Deployment YAML (k8s-deployment.yaml)
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: contact-app-api
  labels:
    app: contact-app-api
spec:
  replicas: 3
  selector:
    matchLabels:
      app: contact-app-api
  template:
    metadata:
      labels:
        app: contact-app-api
    spec:
      containers:
      - name: contact-app-api
        image: ghcr.io/navalkbhatt/contact-app-api:latest
        ports:
        - containerPort: 5000
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: "Production"
        resources:
          requests:
            memory: "256Mi"
            cpu: "250m"
          limits:
            memory: "512Mi"
            cpu: "500m"
---
apiVersion: v1
kind: Service
metadata:
  name: contact-app-api-service
spec:
  selector:
    app: contact-app-api
  ports:
    - protocol: TCP
      port: 80
      targetPort: 5000
  type: LoadBalancer
```

### GitHub Actions Kubernetes Deploy
```yaml
- name: Deploy to Kubernetes
  run: |
    kubectl apply -f k8s-deployment.yaml
    kubectl rollout status deployment/contact-app-api
```

## Docker Compose for Local/Development

### docker-compose.yml
```yaml
version: '3.8'
services:
  contact-app-api:
    image: ghcr.io/navalkbhatt/contact-app-api:latest
    ports:
      - "5000:5000"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
    depends_on:
      - database
      
  database:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
      SA_PASSWORD: "YourPassword123!"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql

volumes:
  sqldata:
```

## Environment Variables

Create these secrets in your GitHub repository:

### Required Secrets
- `AZURE_WEBAPP_PUBLISH_PROFILE` (for Azure deployment)
- `AWS_ACCESS_KEY_ID` (for AWS deployment)
- `AWS_SECRET_ACCESS_KEY` (for AWS deployment)
- `KUBE_CONFIG` (for Kubernetes deployment)
- `DATABASE_CONNECTION_STRING` (for database connections)

### Environment-specific Settings
- **Staging**: Less restrictive, auto-deploy from main branch
- **Production**: Manual approval required, health checks, rollback capability

## Health Check Endpoint

Ensure your application has a health check endpoint at `/health` or `/api/health` for deployment verification.