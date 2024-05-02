using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface IUniversityRepository
    {
        Task<Result<University>> Create(University model);

        Task<Result<int>> Delete(Guid id);

        Task<Result<University>> Update(University model);

        Task<Result<University>> GetUniversityById(Guid id);

        Task<Result<University>> GetAll();

        Task<Result<University>> GetUniversityByName(string name);
        
        Task<Result<University>> GetUniversityByLocation(string country, string city);
    }
}
