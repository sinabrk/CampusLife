using BG.CampusLife.Application.Common;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Identity.DTOs
{
    public static class IdentityResultExtensions
    {
        public static IdentityResultHandler ToResult(this IdentityResult result)
        {
            return result.Succeeded
                ? IdentityResultHandler.Success()
                : IdentityResultHandler.Failure(result.Errors.Select(e => e.Description));
        }
    }
}