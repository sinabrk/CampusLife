# System Design
## Table of contents

* [Identity Server](#identity-server)
* [Custom Exception Handler](#custom-exception-handler)
* [AutoMapper Interface](#automapper-interface)
* [Database Seeds](#database-seeds)
* [Notification Service](#notification-service)
* [CampusContext Interface](#campus-context-interface)
* [Current User Service](#current-user-service)
---
## Identity Server
we use identity server by Microsoft as our main and base user management system. it has all the operations and needed objects for user managements such as Registration/Login process, assign roles to users and etc.
we split the functionalities into two separate files named as **UserManager** and **SignInManager**.

As it is clear from **SignInManager** it only handles _Login_ process which user receives [Access Token] and [Refresh Token].

In **UserManager** contains all the operations such as:
* creating user
* confirm user email
* deactivate account
* reset password
* change password
* suspend account by admin

Each user in this system has a unique id, to connect identity system user to our database we only store the [user id] in our main user model.

[UserManager](../../Src/BG.CampusLife.Application/Interfaces/Services/IUserManager.cs)

[SignInManager](../../Src/BG.CampusLife.Application/Interfaces/Services/IUserManager.cs)

[UserModel](../../Src/BG.CampusLife.Domain/Entities/User.cs)

---
## AutoMapper Interface
there is an interface called **IMapFrom** in **Application Layer**, that handles the mapping profiles and mapping entity to dto.
so we don't need to create and register mapping profiles for every dto, it will registered automatically.

[IMapFrom](../../Src/BG.CampusLife.Application/Common/Mappings/IMapFrom.cs)

---
## Custom Exception Handler
in out presentation layer we have a middleware called **CustomExceptionHandler**. if anything happens that is going to throw an exception, all goes here.
In here we can customize everything on the exception side.

[CustomExceptionHandlerMiddleware](../../Src/BG.CampusLife.Presentation/Middlewares/CustomExceptionHandlerMiddleware.cs)

---
## Database Seeds
For our testing purpose we have two files named as **CampusSeed** and **IdentitySeed**.
In Identity Seed we create two predefined user in _IdentityServer_ for our login purposes. On the other side, Campus Seed will seed the entities we needed for our system like categories, locations and etc.
all of the data are fake and is just for testing.

[IdentitySeed](../../Src/BG.CampusLife.Infrastructure/Identity/IdentitySeed.cs)

[CampusSeed](../../Src/BG.CampusLife.Persistence/CampusSeed.cs)

---
## Notification Service
For pushing notifications to client, we use **SignalR** as main component for _websocket_. Each client has a unique connection id, we use that for sending specific notification to our clients.


[NotificationService](../../Src/BG.CampusLife.Application/Interfaces/Services/INotificationService.cs)
[NotificationHub](../../Src/BG.CampusLife.Infrastructure/Notifications/NotificationHub.cs)

---
## Campus Context Interface
For the sake of Clean Architecture and multi-layered design, we needed a interface for our db context.

[ICampusContext](../../Src/BG.CampusLife.Application/Interfaces/ICampusContext.cs)
[CampusContext](../../Src/BG.CampusLife.Persistence/CampusContext.cs)

---
## Current User Service
A user must be authorized to do some action, therefor he/ she must have token in http header, this service only decodes the token into some properties to use in other parts of project.

[CurrentUserService](../../Src/BG.CampusLife.Application/Interfaces/Services/ICurrentUserService.cs)
