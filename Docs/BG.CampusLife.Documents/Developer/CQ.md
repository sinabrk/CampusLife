# Command and Queries
In **application layer** we implement the features or entities in below structure.

```
EntityName (in plural)
|- Commands
|----- DeleteEntity
|--------- DeleteEntityCommand.cs
|--------- DeleteEntityHandler.cs
|----- UpsertEntity
|--------- UpsertEntityCommand.cs
|--------- UpsertEntityHandler.cs
|--------- UpsertEntityValidator.cs
|- DTOs
|--------- EntityDto.cs
|- Queries
|----- GetList
|--------- GetListEntityQuery.cs
|--------- GetListEntityHandler.cs
|--------- GetListEntityValidator.cs
|----- GetById
|--------- GetEntityByIdQuery.cs
|--------- GetEntityByIdHandler.cs
|--------- GetEntityByIdValidator.cs
```