# Terraform: EC2 + nginx

This folder contains a minimal Terraform configuration to create an AWS EC2 instance and install nginx using user-data.

Files:

- `main.tf` - provider, security group, EC2 instance and EIP; installs nginx via user_data
- `variables.tf` - variables (region, instance_type, ami, key_name)
- `outputs.tf` - outputs (instance id, public ip, public dns)

Quick usage (PowerShell):

```powershell
# set your AWS credentials in env or via AWS CLI shared credentials
cd .\terraform
terraform init
terraform plan -var 'region=us-east-1'
terraform apply -var 'region=us-east-1' -auto-approve
```

Notes and assumptions:

- This configuration does NOT include AWS credentials. Set them via environment variables (e.g. `AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`) or the shared credentials file before running.
- `key_name` variable is optional; if you want to SSH into the instance set an existing key pair name in AWS.
- By default this uses the latest Amazon Linux 2 AMI for the selected region. You can override with `-var 'ami=ami-xxxx'`.
- Security group opens SSH (22) and HTTP (80) to the world for convenience. Lock these down before production use.

To destroy:

```powershell
terraform destroy -auto-approve
```
