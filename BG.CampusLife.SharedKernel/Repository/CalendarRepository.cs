using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Application.Interfaces.Repositories;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;
using BG.CampusLife.Domain.Exceptions;
using BG.CampusLife.Infrastructure;
using BG.CampusLife.Infrastructure.Persistence;
using BG.CampusLife.SharedKernel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BG.CampusLife.SharedKernel.Repository
{
    public class CalendarRepository : BaseRepository<CampusContext, CalendarRepository>, ICalendarRepository
    {
        public CalendarRepository(CampusContext context, ILogger<CalendarRepository> logger, IMapper mapper) : base(
            context, logger, mapper)
        {
        }

        public async Task<Result<Calendar>> GetById(Guid calendarId, User user)
        {
            var result = new Result<Calendar>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entity = await Context.Calendars.FirstOrDefaultAsync(c =>
                    c.Id == calendarId && c.UserId == user.Id),
            };

            if (result.Entity is not null) return result;

            result.Message = "Calendar not found";
            result.Succeeded = false;
            result.StatusCode = ResultStatusCodes.NotFound;

            return result;
        }

        public async Task<Result<Calendar>> GetList(User user)
        {
            var entities = await Context.Calendars
                .Where(c => c.UserId == user.Id).Select(c =>
                    new Calendar()
                    {
                        Id = c.Id,
                        Date = c.Date,
                        EntityId = c.EntityId,
                        EntityType = c.EntityType,
                        Notes = c.Notes,
                        UserId = c.UserId,
                    }).ToListAsync();

            return new Result<Calendar>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        public async Task<Result<Calendar>> GetList(User user, DateTime start, DateTime end)
        {
            var entities = await Context.Calendars.Where(
                c => c.UserId == user.Id &&
                     c.Date >= start &&
                     c.Date < end
            ).Select(c => new Calendar()
            {
                Id = c.Id,
                Date = c.Date,
                EntityId = c.EntityId,
                EntityType = c.EntityType,
                Notes = c.Notes,
                UserId = c.UserId,
            }).ToListAsync();

            return new Result<Calendar>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        public async Task<Result<Calendar>> Upsert(Calendar calendar)
        {
            var result = new Result<Calendar>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Created,
            };

            var entity = await Context.Calendars.FirstOrDefaultAsync(c => c.Id == calendar.Id);
            if (entity is null)
            {
                await Context.Calendars.AddAsync(calendar);
            }
            else
            {
                Context.Entry(calendar).State = EntityState.Modified;
                Context.Update(calendar);
            }

            await Context.SaveChangesAsync();
            result.Entity = calendar;

            return result;
        }

        public async Task<Result<int>> Delete(Guid id, User user)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.NoContent,
            };

            var entity = await Context.Calendars.FirstOrDefaultAsync(c => c.Id == id && c.UserId == user.Id);
            if (entity is null)
            {
                result.Message = $"Calendar not found with {id}.";
                result.StatusCode = ResultStatusCodes.NotFound;
                result.Succeeded = false;
            }
            else
            {
                Context.Calendars.Remove(entity);
                await Context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result<int>> ShareCalendar(User user, User targetUser)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful
            };

            if (await Context.SharedCalendars.AnyAsync(s =>
                s.UserId == user.Id &&
                s.SharedUserId == targetUser.Id))
            {
                result.Message = $"Calendar already shared to this user.";
                result.StatusCode = ResultStatusCodes.BadRequest;
                result.Succeeded = false;
            }
            else
            {
                await Context.SharedCalendars.AddAsync(new SharedCalendar()
                {
                    UserId = user.Id,
                    SharedUserId = targetUser.Id,
                });
                await Context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result<int>> UnShareCalendar(User user, User targetUser)
        {
            var result = new Result<int>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful
            };

            var entity =
                await Context.SharedCalendars.FirstOrDefaultAsync(c =>
                    c.UserId == user.Id &&
                    c.SharedUserId == targetUser.Id);
            if (entity is null)
            {
                result.Message = $"Calendar is not shared to this user.";
                result.StatusCode = ResultStatusCodes.BadRequest;
                result.Succeeded = false;
            }
            else
            {
                Context.SharedCalendars.Remove(entity);
                await Context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result<SharedCalendar>> GetSharedUsers(User user)
        {
            var entities = await Context.SharedCalendars
                .Where(s => s.UserId == user.Id).Select(s =>
                    new SharedCalendar()
                    {
                        UserId = s.UserId,
                        SharedUserId = s.SharedUserId,
                        Created = s.Created,
                    }).ToListAsync();

            return new Result<SharedCalendar>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
                Entities = entities,
                Total = entities.Count,
            };
        }

        public async Task<Result<Calendar>> GetSharedCalendar(Guid sharedToUserId)
        {
            var result = new Result<Calendar>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };
            var entity = await Context.SharedCalendars.FirstOrDefaultAsync(c => c.SharedUserId == sharedToUserId);
            if (entity is null)
            {
                result.Message = "No Calendar is shared to this user !";
                result.StatusCode = ResultStatusCodes.BadRequest;
                result.Succeeded = false;
                return result;
            }

            result.Entities = await Context.Calendars.Where(c => c.UserId == entity.UserId).Select(c => new Calendar()
            {
                Id = c.Id,
                Date = c.Date,
                EntityId = c.EntityId,
                EntityType = c.EntityType,
                Notes = c.Notes,
                UserId = c.UserId,
            }).ToListAsync();

            result.Total = result.Entities.Count;

            return result;
        }

        public async Task<Result<Calendar>> GetSharedCalendar(Guid sharedToUserId,
            DateTime start, DateTime end)
        {
            var result = new Result<Calendar>()
            {
                Succeeded = true,
                StatusCode = ResultStatusCodes.Successful,
            };

            var entity = await Context.SharedCalendars.FirstOrDefaultAsync(c => c.SharedUserId == sharedToUserId);
            if (entity is null)
            {
                result.Message = "No Calendar is shared to this user !";
                result.StatusCode = ResultStatusCodes.BadRequest;
                result.Succeeded = false;
                return result;
            }

            result.Entities = await Context.Calendars.Where(
                c => c.UserId == entity.UserId &&
                     c.Date >= start &&
                     c.Date < end
            ).Select(c => new Calendar()
            {
                Id = c.Id,
                Date = c.Date,
                EntityId = c.EntityId,
                EntityType = c.EntityType,
                Notes = c.Notes,
                UserId = c.UserId,
            }).ToListAsync();

            result.Total = result.Entities.Count;

            return result;
        }
    }
}