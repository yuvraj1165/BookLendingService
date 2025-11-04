provider "aws" {
  region = "eu-west-2"
  profile = "default"
}

module "networking" {
  source = "./modules/networking"
}

module "iam" {
  source = "./modules/iam"
}

module "alb" {
  source = "./modules/alb"
  vpc_id = module.networking.vpc_id
  subnet_ids = module.networking.subnet_ids
  security_group_id = module.networking.security_group_id
}

module "ecs" {
  source = "./modules/ecs"
  image_url = "ghcr.io/yuvraj1165/booklendingservice-api:latest"
  vpc_id = module.networking.vpc_id
  subnet_ids = module.networking.subnet_ids
  security_group_id  = module.networking.security_group_id
  execution_role_arn = module.iam.execution_role_arn
  target_group_arn   = module.alb.target_group_arn
}
