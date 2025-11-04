resource "aws_ecs_cluster" "main" {
  name = "booklending-cluster"
}

resource "aws_ecs_task_definition" "api" {
  family                   = "booklending-api"
  requires_compatibilities = ["FARGATE"]
  network_mode            = "awsvpc"
  cpu                     = "256"
  memory                  = "512"
  execution_role_arn      = var.execution_role_arn

  container_definitions = jsonencode([
    {
      name      = "booklending-api"
      image     = var.image_url
      essential = true
      portMappings = [
        {
          containerPort = 8080
          hostPort      = 8080
        }
      ]
      repositoryCredentials = {
        credentialsParameter = "arn:aws:secretsmanager:eu-west-2:786284304523:secret:ghcr-creds"
      }
    }
  ])
}

resource "aws_ecs_service" "api" {
  name            = "booklending-api-service"
  cluster         = aws_ecs_cluster.main.id
  task_definition = aws_ecs_task_definition.api.arn
  desired_count   = 1
  launch_type     = "FARGATE"
  network_configuration {
    subnets         = var.subnet_ids
    security_groups = [var.security_group_id]
    assign_public_ip = true
  }
  load_balancer {
    target_group_arn = var.target_group_arn
    container_name   = "booklending-api"
    container_port   = 8080
  }
  
  force_new_deployment = true
}

output "ecs_service_name" {
  value = aws_ecs_service.api.name
}