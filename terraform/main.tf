terraform {
  required_version = ">= 1.1.0"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region = var.region
}

data "aws_ami" "amazon_linux" {
  most_recent = true
  owners      = ["amazon"]
  filter {
    name   = "name"
    values = ["amzn2-ami-hvm-*-x86_64-gp2"]
  }
}

resource "aws_security_group" "web_sg" {
  name        = "terraform-web-sg"
  description = "Allow HTTP and SSH access"

  ingress {
    description = "HTTP"
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "SSH"
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    description = "all outbound"
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_instance" "web" {
  ami           = var.ami != "" ? var.ami : data.aws_ami.amazon_linux.id
  instance_type = var.instance_type

  # Optional: set an existing key pair name to allow SSH access. Leave empty to skip.
  key_name = var.key_name != "" ? var.key_name : null

  # Attach the security group allowing HTTP/SSH
  vpc_security_group_ids = [aws_security_group.web_sg.id]

  tags = {
    Name = "terraform-nginx-server"
  }

  user_data = <<-EOF
    #!/bin/bash
    set -e
    yum update -y
    yum install -y nginx
    systemctl enable nginx
    systemctl start nginx
    echo "<html><body><h1>Deployed by Terraform</h1></body></html>" > /usr/share/nginx/html/index.html
  EOF
}

resource "aws_eip" "ip" {
  instance = aws_instance.web.id
  depends_on = [aws_instance.web]
}
