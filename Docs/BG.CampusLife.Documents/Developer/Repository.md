# Repositories
In **infrastructure layer** we implement the features or entities in below structure.

```
Task<Result<Entity>> Method(input);

public async Task<Result<Entity>> Method(input)
{
    var result = new Result<Entity>
    {
        Succeeded = true,
        StatusCode = ResultStatusCodes.Successful,
    }
    
    // Do some action here
    // result.Entity = ...
    // result.Entities = ...
    
    return result
```

more complete example in:

[University Repository](../../../Src/BG.CampusLife.Infrastructure/Repositories/UniversityRepository.cs)