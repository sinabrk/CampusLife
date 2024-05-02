using System;

namespace BG.CampusLife.Domain.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base("Invalid Token")
        {
        }
    }
}