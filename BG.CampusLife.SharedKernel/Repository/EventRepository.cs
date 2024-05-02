using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class EventRepository : BaseRepository<CampusContext, EventRepository>, IEventRepository
    {
        public EventRepository(CampusContext context, ILogger<EventRepository> logger, IMapper mapper) : base(context,
            logger, mapper)
        {
        }

        public async Task<Result<Event>> GetList()
        {
            var entities = await Context.Events.Select(e => new Event()
            {
                Id = e.Id,
                Title = e.Title,
                Body = e.Body,
                Created = e.Created,
                UserId = e.UserId,
                LocationId = e.LocationId,
                Start = e.Start,
                End = e.End,
            }).ToListAsync();
            
            return new Result<Event>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };;
        }

        public async Task<Result<Event>> GetById(Guid id)
        {
            var result = new Result<Event>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Events.FirstOrDefaultAsync(e => e.Id == id),
            };
            if (result.Entity is not null) return result;
            
            result.Message = $"Event not found with id {id}";
            result.Succeeded = false;
            result.StatusCode = ResultStatusCodes.NotFound;

            return result;
        }

        public async Task<Result<Event>> Upsert(Event ev)
        {

            var result = new Result<Event>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };
            
            var entity = await Context.Events.FirstOrDefaultAsync(c => c.Id == ev.Id);
            if (entity is null)
            {
                await Context.Events.AddAsync(ev);
            }
            else
            {
                Context.Entry(ev).State = EntityState.Modified;
                Context.Update(ev);
            }
            await Context.SaveChangesAsync();

            result.Entity = ev;

            return result;
        }

        public async Task<Result<int>> Delete(Guid id)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };
            
            var entity = await Context.Events.FirstOrDefaultAsync(c => c.Id == id);
            if (entity is null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Message = $"Calendar not found with {id}";
            }
            else
            {
                Context.Events.Remove(entity);
                await Context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result<Event>> GetUserEvents(Guid userId)
        {
            var entities = await Context.Events.Where(e => e.UserId == userId).Select(e => new Event()
            {
                Id = e.Id,
                Title = e.Title,
                Body = e.Body,
                Created = e.Created,
                UserId = e.UserId,
                LocationId = e.LocationId,
                Start = e.Start,
                End = e.End,
            }).ToListAsync();

            return new Result<Event>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count
            };
        }
    }
}