# RPG LMS Server

This is the server for the RPG Learning Management System (LMS). It uses Docker, Terraform, PostgreSQL, and Redis.

## Prerequisites

- Docker
- Terraform
- Git

## Setup

1. Clone the repository: https://github.com/yourusername/rpg-lms-server.git

2. Navigate to the project directory:

3. Copy the `.env.example` file to `.env`

4. Update the `.env` file with your own values.

5. Build and start the Docker containers from your server project directory:
    To start: docker-compose up --build -d
    To stop: docker-compose down

## Deployment

The deployment process depends on the cloud provider. Here are the general steps you would need to follow for AWS and Azure:

1. Set up an account on the cloud provider's website.

2. Install the cloud provider's CLI tool.

3. Configure the CLI tool with your account credentials.

4. Use Terraform to provision the necessary resources (like virtual machines, databases, and networking components).

5. Deploy the Docker containers to the provisioned resources.

Please refer to the official AWS and Azure documentation for specific instructions on how to deploy Docker containers to these platforms.

## Errors
Run the following command to restore the dependencies: "dotnet restore"

## Contributing

Please read `CONTRIBUTING.md` for details on our code of conduct, and the process for submitting pull requests to us.

## License

This project is licensed under the MIT License - see the `LICENSE.md` file for details