// General Usings
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using System.Net;
global using System;
global using BG.CampusLife.Infrastructure.Identity;
global using BG.CampusLife.Persistence;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Hosting;
global using System.IO;
global using System.Reflection;
global using System.Text.Json.Serialization;
global using BG.CampusLife.Application;
global using BG.CampusLife.Application.Interfaces;
global using BG.CampusLife.Application.Interfaces.Services;
global using BG.CampusLife.Infrastructure;
global using BG.CampusLife.Presentation.Middlewares;
global using BG.CampusLife.Infrastructure.Notifications;
global using BG.CampusLife.Presentation.Services;
global using FluentValidation.AspNetCore;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.OpenApi.Models;
global using Newtonsoft.Json;
global using System.Security.Claims;
global using BG.CampusLife.Application.Common.Exceptions;
global using Microsoft.Extensions.Logging;

// Calendar
global using BG.CampusLife.Application.Calendars.Commands.DeleteCalendar;
global using BG.CampusLife.Application.Calendars.Commands.ShareCalendar;
global using BG.CampusLife.Application.Calendars.Commands.UnShareCalendar;
global using BG.CampusLife.Application.Calendars.Commands.UpsertCalendar;
global using BG.CampusLife.Application.Calendars.DTOs;
global using BG.CampusLife.Application.Calendars.Queries.GetList;
global using BG.CampusLife.Application.Calendars.Queries.GetSharedCalendar;
global using BG.CampusLife.Application.Calendars.Queries.GetSharedUsers;

// Category
global using BG.CampusLife.Application.Categories.Commands.DeleteCategory;
global using BG.CampusLife.Application.Categories.Commands.UpsertCategory;
global using BG.CampusLife.Application.Categories.DTOs;
global using BG.CampusLife.Application.Categories.Queries.GetByCode;
global using BG.CampusLife.Application.Categories.Queries.GetById;
global using BG.CampusLife.Application.Categories.Queries.GetBySlug;
global using BG.CampusLife.Application.Categories.Queries.GetList;

// Document
global using BG.CampusLife.Application.Documents.Commands.DeleteDocument;
global using BG.CampusLife.Application.Documents.Commands.UploadDocument;

// Friends

// Identity
global using BG.CampusLife.Application.Identity.Commands.ConfirmEmailToken;
global using BG.CampusLife.Application.Identity.Commands.Deactivate;
global using BG.CampusLife.Application.Identity.Commands.ForgetPassword;
global using BG.CampusLife.Application.Identity.Commands.Login;
global using BG.CampusLife.Application.Identity.Commands.RefreshToken;
global using BG.CampusLife.Application.Identity.Commands.Register;
global using BG.CampusLife.Application.Identity.Commands.ResetPassword;
global using BG.CampusLife.Application.Identity.DTOs;

// Location
global using BG.CampusLife.Application.Locations.Commands.DeleteLocation;
global using BG.CampusLife.Application.Locations.Commands.UpsertLocation;
global using BG.CampusLife.Application.Locations.DTOs;
global using BG.CampusLife.Application.Locations.Queries.GetById;
global using BG.CampusLife.Application.Locations.Queries.GetByLongLat;
global using BG.CampusLife.Application.Locations.Queries.GetList;

// Market
global using BG.CampusLife.Application.MarketPlace.Commands.DeleteMarketItem;
global using BG.CampusLife.Application.MarketPlace.Commands.UpsertMarketItem;
global using BG.CampusLife.Application.MarketPlace.DTOs;
global using BG.CampusLife.Application.MarketPlace.Queries.GetDetail;
global using BG.CampusLife.Application.MarketPlace.Queries.GetList;
global using BG.CampusLife.Application.MarketPlace.Queries.GetMarketProperties;
global using BG.CampusLife.Application.MarketPlace.Queries.GetMarketTags;
global using BG.CampusLife.Application.MarketPlace.Queries.GetUserMarketItems;

// Notification
global using BG.CampusLife.Application.Notifications.Commands.SendNotification;
global using BG.CampusLife.Application.Notifications.Commands.SetNotificationVisited;
global using BG.CampusLife.Application.Notifications.Queries.GetNotificationDetail;
global using BG.CampusLife.Application.Notifications.Queries.GetUserNotifications;

// Post
global using BG.CampusLife.Application.Posts.Commands.DeletePost;
global using BG.CampusLife.Application.Posts.Commands.UpsertPost;
global using BG.CampusLife.Application.Posts.DTOs;
global using BG.CampusLife.Application.Posts.Queries.GetById;
global using BG.CampusLife.Application.Posts.Queries.GetList;

// Property
global using BG.CampusLife.Application.Properties.Commands.DeleteProperty;
global using BG.CampusLife.Application.Properties.Commands.UpsertProperty;
global using BG.CampusLife.Application.Properties.Queries.GetPropertiesList;
global using BG.CampusLife.Application.Properties.Queries.GetPropertyById;

// Tag
global using BG.CampusLife.Application.Tags.Commands.DeleteTag;
global using BG.CampusLife.Application.Tags.Commands.UpsertTag;
global using BG.CampusLife.Application.Tags.Queries.GetTagById;
global using BG.CampusLife.Application.Tags.Queries.GetTagsList;

// University
global using BG.CampusLife.Application.Universities.Commands.DeleteUniversity;
global using BG.CampusLife.Application.Universities.Commands.UpsertUniversity;
global using BG.CampusLife.Application.Universities.DTOs;
global using BG.CampusLife.Application.Universities.Queries.GetById;
global using BG.CampusLife.Application.Universities.Queries.GetList;

// User
global using BG.CampusLife.Application.Users.Commands.UpdateProfile;
global using BG.CampusLife.Application.Users.Queries.GetUserProfile;