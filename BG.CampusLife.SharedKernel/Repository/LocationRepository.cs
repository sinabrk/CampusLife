using AutoMapper;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Exceptions;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class LocationRepository : BaseRepository<CampusContext, LocationRepository>, ILocationRepository
    {
        public LocationRepository(CampusContext context, ILogger<LocationRepository> logger, IMapper mapper) : base(
            context, logger, mapper)
        {
        }

        #region Commands

        public async Task<Result<Location>> Create(Location location)
        {
            var result = new Result<Location>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };

            if (location == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.BadRequest;
                result.Message = "Location is null";
                return result;
            }

            result.Entity = location;

            await Context.Locations.AddAsync(location);
            await Context.SaveChangesAsync();

            return result;
        }

        public async Task<Result<Location>> Update(Location location)
        {
            var result = new Result<Location>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };

            var updateLocation = Mapper.Map<Location>(location);

            if (await Context.Locations.FindAsync(updateLocation.Id) == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Message = $"Location not found with {updateLocation.Id}";
                return result;
            }

            result.Entity = updateLocation;

            Context.Entry(updateLocation).State = EntityState.Modified;
            Context.Update(updateLocation);
            await Context.SaveChangesAsync();

            return result;
        }

        public async Task<Result<int>> Delete(Guid id)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };

            var location = await Context.Locations.FirstOrDefaultAsync(l => l.Id == id);

            if (location is null)
            {
                result.Succeeded = false;
                result.Message = $"Location not found with {id}";
                result.StatusCode = ResultStatusCodes.NotFound;
            }

            Context.Locations.Remove(location);
            await Context.SaveChangesAsync();
            return result;
        }

        #endregion

        #region Quries

        public async Task<Result<Location>> GetById(Guid id)
        {
            var result = new Result<Location>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };

            var entity = await Context.Locations
                .Include(location => location.Universities)
                .AsSplitQuery()
                .Include(location => location.Posts)
                .AsSplitQuery()
                .Include(location => location.Users)
                .AsSplitQuery()
                .Where(x => x.Id == id)
                .Select(x => new Location()
                {
                    Id = x.Id,
                    Country = x.Country,
                    City = x.City,
                    State = x.State,
                    Street = x.Street,
                    DoorNumber = x.DoorNumber,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    PostalCode = x.PostalCode,
                    Users = x.Users,
                    Universities = x.Universities,
                    Posts = x.Posts
                })
                .FirstOrDefaultAsync();

            if (entity is null)
            {
                result.Succeeded = false;
                result.Message = $"Location not found with {id}";
                result.StatusCode = ResultStatusCodes.NotFound;
            }
            else
            {
                result.Entity = entity;
            }

            return result;
        }

        public async Task<Result<Location>> GetLocationByCity(string city) => new Result<Location>()
        {
            Succeeded = false,
            StatusCode = ResultStatusCodes.NotImplemented,
            Message = "Not Implemented",
        };

        public async Task<Result<Location>> GetLocationByCountry(string country) => new Result<Location>()
        {
            Succeeded = false,
            StatusCode = ResultStatusCodes.NotImplemented,
            Message = "Not Implemented",
        };

        public async Task<Result<Location>> GetLocationByLongLat(double lng, double lat) =>
            new Result<Location>()
            {
                Succeeded = false,
                StatusCode = ResultStatusCodes.NotImplemented,
                Message = "Not Implemented",
            };

        public async Task<Result<Location>> GetAll()
        {
            var entities = await Context.Locations
                .Include(location => location.Users).AsSplitQuery()
                .Include(location => location.Posts).AsSplitQuery()
                .Include(location => location.Universities).AsSplitQuery()
                .Select(x => new Location()
                {
                    Id = x.Id,
                    Country = x.Country,
                    City = x.City,
                    State = x.State,
                    Street = x.Street,
                    DoorNumber = x.DoorNumber,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    PostalCode = x.PostalCode,
                    Users = x.Users,
                    Universities = x.Universities,
                    Posts = x.Posts
                })
                .ToListAsync();
            return new Result<Location>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        #endregion
    }
}