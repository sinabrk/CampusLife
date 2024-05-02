using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BG.CampusLife.Application.Common;
using BG.CampusLife.Domain.Entities;

namespace BG.CampusLife.Application.Interfaces.Repositories
{
    public interface IEventRepository
    {
        /// <summary>
        /// Get List of Events
        /// </summary>
        /// <returns></returns>
        Task<Result<Event>> GetList();

        /// <summary>
        /// Get Event By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<Event>> GetById(Guid id);

        /// <summary>
        /// Insert Update Event
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        Task<Result<Event>> Upsert(Event ev);

        /// <summary>
        /// Delete Event Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<int>> Delete(Guid id);
        
        /// <summary>
        /// Get User Events
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result<Event>> GetUserEvents(Guid userId);

    }
}