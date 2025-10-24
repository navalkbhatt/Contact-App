# Contact App API - Deployment Pipeline

This repository contains a comprehensive CI/CD pipeline for deploying the .NET Core Contact App API.

## Pipeline Overview

### 1. Main CI/CD Pipeline (`03-workflow-runner.yml`)

This pipeline automatically triggers on:
- Push to `main` or `develop` branches
- Pull requests to `main` branch
- Changes in `Server/**` or `.github/workflows/**` paths

**Pipeline Stages:**

1. **Build and Test**
   - Sets up .NET 8.0 environment
   - Restores NuGet packages with caching
   - Builds the application
   - Runs unit tests with code coverage
   - Publishes build artifacts

2. **Code Analysis**
   - Runs security scans with DevSkim
   - Uploads SARIF results to GitHub Security tab

3. **Docker Build and Push**
   - Builds Docker image using the provided Dockerfile
   - Pushes to GitHub Container Registry (ghcr.io)
   - Only runs on main branch pushes

4. **Deploy to Staging**
   - Automatically deploys to staging environment
   - Runs smoke tests
   - Requires GitHub environment protection rules

5. **Deploy to Production**
   - Deploys to production after staging success
   - Requires manual approval (configure in GitHub environments)
   - Runs health checks

### 2. Manual Deployment Pipeline (`manual-deployment.yml`)

Allows manual deployment or rollback operations:
- Choose environment (staging/production)
- Specify Docker image version
- Perform rollback if needed
- Triggered via GitHub Actions UI

### 3. Database Migration Pipeline (`database-migration.yml`)

Manages database schema changes:
- Run migrations on staging/production
- Rollback to specific migration
- View migration status
- Manual trigger only

## Setup Instructions

### 1. GitHub Repository Setup

1. **Enable GitHub Packages:**
   - Go to Settings > Actions > General
   - Enable "Read and write permissions" for GITHUB_TOKEN

2. **Create Environments:**
   - Go to Settings > Environments
   - Create `staging` and `production` environments
   - Configure protection rules (required reviewers for production)

3. **Add Repository Secrets:**
   ```
   DATABASE_CONNECTION_STRING_STAGING=your_staging_connection_string
   DATABASE_CONNECTION_STRING_PRODUCTION=your_production_connection_string
   ```

### 2. Docker Image Access

The pipeline pushes Docker images to GitHub Container Registry:
```
ghcr.io/navalkbhatt/contact-app-api:latest
ghcr.io/navalkbhatt/contact-app-api:main-<sha>
```

### 3. Health Check Endpoint

The application now includes a health check endpoint at `/health` for deployment verification.

## Deployment Targets

### Azure App Service

1. **Create Azure Resources:**
   ```bash
   az group create --name ContactApp-RG --location "East US"
   az appservice plan create --name ContactApp-Plan --resource-group ContactApp-RG --sku B1 --is-linux
   az webapp create --resource-group ContactApp-RG --plan ContactApp-Plan --name contact-app-api-prod --deployment-container-image-name ghcr.io/navalkbhatt/contact-app-api:latest
   ```

2. **Add Azure secrets to GitHub:**
   - `AZURE_WEBAPP_PUBLISH_PROFILE`

### AWS ECS

1. **Create ECS Cluster and Service**
2. **Add AWS secrets to GitHub:**
   - `AWS_ACCESS_KEY_ID`
   - `AWS_SECRET_ACCESS_KEY`

### Kubernetes

1. **Create Kubernetes cluster**
2. **Add Kubernetes config to GitHub:**
   - `KUBE_CONFIG`

See `deployment-examples.md` for detailed configuration examples.

## Local Development

### Run with Docker Compose
```bash
cd Server/ContactAppApi
docker-compose up -d
```

### Build and Run Locally
```bash
cd Server/ContactAppApi
dotnet restore
dotnet build
dotnet run --project ContactAppApi
```

## Monitoring and Troubleshooting

### Pipeline Monitoring
- Check GitHub Actions tab for pipeline status
- Review job logs for detailed information
- Monitor container registry for image builds

### Application Monitoring
- Health check: `GET /health`
- Swagger UI: `/swagger`
- Application logs in deployment platform

### Common Issues
1. **Build Failures**: Check .NET version compatibility
2. **Test Failures**: Review test output in job artifacts
3. **Docker Build Issues**: Verify Dockerfile paths and dependencies
4. **Deployment Failures**: Check environment variables and secrets

## Security Features

- Automated security scanning with DevSkim
- Container image vulnerability scanning
- GitHub token permissions follow least privilege
- Environment-based secret management
- Manual approval gates for production deployments

## Customization

To customize the pipeline for your specific needs:

1. **Modify environment variables** in workflow files
2. **Update deployment commands** in deploy jobs
3. **Configure additional testing** stages
4. **Add notification integrations** (Slack, Teams, email)
5. **Implement blue-green deployments** for zero-downtime updates

## Support

For issues with the pipeline:
1. Check GitHub Actions logs
2. Review deployment platform logs
3. Verify environment configuration
4. Test locally with same Docker image