output "instance_id" {
  description = "ID of the EC2 instance"
  value       = aws_instance.web.id
}

output "public_ip" {
  description = "Public IP assigned to the instance (Elastic IP)"
  value       = aws_eip.ip.public_ip
}

output "public_dns" {
  description = "Public DNS of the instance"
  value       = aws_instance.web.public_dns
}
