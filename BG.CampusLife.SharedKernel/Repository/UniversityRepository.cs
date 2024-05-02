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
    public class UniversityRepository : BaseRepository<CampusContext, UniversityRepository>, IUniversityRepository
    {
        public UniversityRepository(CampusContext context, ILogger<UniversityRepository> logger, IMapper mapper) : base(
            context, logger, mapper)
        {
        }

        #region Commands

        public async Task<Result<University>> Create(University university)
        {
            await Context.Universities.AddAsync(university);
            await Context.SaveChangesAsync();

            Logger.LogInformation($"University with ID {university.Id} has been created.");
            return new Result<University>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
                Entity = university,
            };
        }

        public async Task<Result<int>> Delete(Guid id)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };
            var uni = await Context.Universities.FindAsync(id);

            if (uni is null)
            {
                result.Message = $"University not found with {id}";
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
            }
            else
            {
                Context.Universities.Remove(uni);
                await Context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result<University>> Update(University university)
        {
            var result = new Result<University>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };

            var uni = await Context.Universities.FindAsync(university.Id);

            if (uni is null)
            {
                result.Message = $"University not found with {university.Id}";
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
            }

            else
            {
                Context.Entry(university).State = EntityState.Modified;
                Context.Universities.Update(university);

                await Context.SaveChangesAsync();

                Logger.LogInformation($"University with Id {university.Id} has been updated.");

                result.Entity = university;
            }

            return result;
        }

        // TODO: Check with Omid if he sends you the id of location
        //public Guid CheckLocation(Location location)
        //{
        //    var result = Context.Locations.Find(location.Id);

        //    return result.Id;
        //}

        #endregion

        #region Quries

        public async Task<Result<University>> GetUniversityByLocation(string country, string city)
        {
            var entities = await Context.Universities
                .Include(x => x.Location)
                .AsSplitQuery()
                .Where(x => x.Location.Country == country && x.Location.City == city)
                .ToListAsync();

            return new Result<University>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        public async Task<Result<University>> GetUniversityByName(string name)
        {
            var result = new Result<University>
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Universities.Where(x => x.Name.Contains(name))
                    .Include(x => x.Users)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync()
            };

            if (result.Entity is not null) return result;

            result.Message = $"University not found with {name}";
            result.StatusCode = ResultStatusCodes.NotFound;
            result.Succeeded = false;

            return result;
        }

        public async Task<Result<University>> GetAll()
        {
            var entities = await Context.Universities
                .Select(x => new University { Name = x.Name, Location = x.Location, Users = x.Users })
                .ToListAsync();

            return new Result<University>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }


        public async Task<Result<University>> GetUniversityById(Guid id)
        {
            var result = new Result<University>
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Universities
                    .Where(x => x.Id == id)
                    .Include(x => x.Users)
                    .AsSplitQuery()
                    .Select(x => new University { Name = x.Name, Location = x.Location, Users = x.Users })
                    .FirstOrDefaultAsync(),
            };

            if (result.Entity is not null) return result;

            result.Message = $"University not found with {id}";
            result.StatusCode = ResultStatusCodes.NotFound;
            result.Succeeded = false;

            return result;
        }

        #endregion
    }
}