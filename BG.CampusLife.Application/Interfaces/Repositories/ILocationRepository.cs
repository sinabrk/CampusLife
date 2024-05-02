using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface ILocationRepository
    {
        Task<Result<Location>> Create(Location location);

        Task<Result<Location>> Update(Location location);

        Task<Result<int>> Delete(Guid id);

        Task<Result<Location>> GetById(Guid id);

        Task<Result<Location>> GetLocationByLongLat(double lng, double lat);

        Task<Result<Location>> GetLocationByCity(string city);

        Task<Result<Location>> GetLocationByCountry(string country);

        Task<Result<Location>> GetAll();
    }
}
