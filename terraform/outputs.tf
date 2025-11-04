output "ecs_service_name" {
  value = module.ecs.ecs_service_name
}

output "load_balancer_dns" {
  value = module.alb.load_balancer_dns
}