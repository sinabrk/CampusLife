# System Architecture

Project is based on 

>**Clean Architecture**
>
>**Repository Pattern**
>
>**CQRS**
>
>**Mediator**
>
> **AutoMapper**
> 
> **FluentValidation**
> 
> **IdentityServer**
> 
> **SignalR**

for now we have 5 layers:
1. [Application](#application-layer)
2. [Domain](#domain-layer) 
3. [Infrastructure](#infrastructure-layer)
4. [Persistence](#persistence-layer)
5. [Presentation](#presentation-layer)

In continue we will discuss from bottom.

---
## Presentation Layer
Web API project consists of _Controllers_, _Middlewares_ and _Services (only the ones that are may change with every request)_.

---
## Persistence Layer
Class library project that maintains are database context and configurations.
1. CampusContext
2. Configuration (Fluent API)
3. Migrations

---
## Domain Layer
All the **Entities** and **Enums** are here.

---
## Infrastructure Layer
Implementation of **Identity Server Settings**, **Services**, **Hubs** and **Repositories** are in this project.

Note: for IdentityServer settings we will explain it in [System Design Section](Design.md).

---
## Application Layer
All The Commands and Queries for each entity will be Here.
1. Entities CQs
2. All Interfaces (Repositories and Services)
3. Utilities or other things that we used as shared component across the project as Common

