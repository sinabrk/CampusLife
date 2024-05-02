using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface ICalendarRepository
    {
        /// <summary>
        /// Get Calendar Entity
        /// </summary>
        /// <param name="calendarId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<Calendar>> GetById(Guid calendarId, User user);
        
        /// <summary>
        /// Get All Calendar Entities of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<Calendar>> GetList(User user);

        /// <summary>
        /// Get Calendar Entities between a time
        /// </summary>
        /// <param name="user"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<Result<Calendar>> GetList(User user, DateTime start, DateTime end);

        /// <summary>
        /// Insert / Update Calendar entity
        /// </summary>
        /// <param name="calendar"></param>
        /// <returns></returns>
        Task<Result<Calendar>> Upsert(Calendar calendar);
        
        /// <summary>
        /// Remove Calendar entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<int>> Delete(Guid id, User user);

        /// <summary>
        /// Share a Calendar to a specific user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="targetUser"></param>
        /// <returns></returns>
        Task<Result<int>> ShareCalendar(User user, User targetUser);
        
        /// <summary>
        /// Revoke Shared Calendar
        /// </summary>
        /// <param name="user"></param>
        /// <param name="targetUser"></param>
        /// <returns></returns>
        Task<Result<int>> UnShareCalendar(User user, User targetUser);

        /// <summary>
        /// Get shared Users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Result<SharedCalendar>> GetSharedUsers(User user);

        /// <summary>
        /// Get Shared Calendars
        /// </summary>
        /// <param name="sharedToUserId"></param>
        /// <returns></returns>
        Task<Result<Calendar>> GetSharedCalendar(Guid sharedToUserId);

        /// <summary>
        /// Get Shared Calendars between a time
        /// </summary>
        /// <param name="sharedToUserId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        Task<Result<Calendar>> GetSharedCalendar(Guid sharedToUserId, DateTime start, DateTime end);
    }
}