variable "region" {
  description = "AWS region to deploy into"
  type        = string
  default     = "us-east-1"
}

variable "instance_type" {
  description = "EC2 instance type"
  type        = string
  default     = "t2.micro"
}

variable "ami" {
  description = "AMI to use (optional). If empty, uses latest Amazon Linux 2"
  type        = string
  default     = ""
}

variable "key_name" {
  description = "Name of an existing EC2 KeyPair to enable SSH access (optional). Leave empty to skip."
  type        = string
  default     = ""
}
