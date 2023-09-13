# BlogManager: Implementing Hexagonal Architecture

## Overview

### Technologies and Patterns Used
- Hexagonal Architecture (Ports and Adapters)
- PostgreSQL Database
- Code-First Auto-Migration
- CQRS Pattern with Mediator
- In-Memory Database for DbContext Testing
- Separate Test Projects for Loosely Coupled Adapters and Ports
- NUnit and Fluent Assertions
- Support for Both JSON and XML Requests and Responses
- Dockerization
- Logging via Serilog
- Exception Handling Middleware with Logging
- Separate DTOs for Requests and Responses

### Areas for Further Improvement
- Integrate event sourcing along with CQRS for separate read and write databases
- Extract domain rules from domain entities to dedicated Domain (Business) Services for more robust business rule management
- Consider the use of application services as opposed to business logic directly within the application

## Architecture

The project is built on a Hexagonal (Ports and Adapters) Architecture and is organized into two main folders:

### Source Folder (`src`)
- BlogManager.Core
- BlogManager.Adapter.Api
- BlogManager.Adapter.Application
- BlogManager.Adapter.PostgreSQL
- BlogManager.Adapter.Logger

### Test Folder (`test`)
- BlogManager.Core.Test
- BlogManager.Adapter.Api.Test
- BlogManager.Adapter.PostgreSQL.Test
- BlogManager.Adapter.Logger.Test

The API project serves as the driver side of the application, while the PostgreSQL and Logger projects function as the driven sides.

#### Key Features:
- In line with Hexagonal Architecture, the Core project is framework-agnostic and fully independent.
- Domain entities such as Author and Blog encapsulate business logic and validation rules.
- Command and Query Handlers are located in the Core project, which also serves as the access point for Repositories.
- Application project serves as the entry point and thus has dependencies on all other projects to eliminate the API project's direct dependency on PostgreSQL repositories.

![](/Users/enginkisalar/Desktop/BlogManagerHexagonalArchitecture.png)

## Test Coverage

The application boasts over 90% test code coverage. Tests for repositories and the core project use an in-memory database, eliminating the need for actual database dependencies.

## System Requirements

- Docker
- Docker Compose

## Installation and Usage

First, navigate to your terminal and execute the following Docker commands:

```bash
docker-compose down
docker-compose build --no-cache
docker-compose up
```

To clone the repository, run:

```bash
git clone https://github.com/ekisalar/BlogManagerHexagonalArchitecture
cd BlogManagerHexagonalArchitecture
```
