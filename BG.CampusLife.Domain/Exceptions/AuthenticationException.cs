using System;

namespace BG.CampusLife.Domain.Exceptions
{
    public class AuthenticationException : Exception
    {
        public string Error { get; set; }

        public AuthenticationException() : base("Register/Login Failed!")
        {
        }

        public AuthenticationException(string error)
        {
            this.Error = error;
        }
    }
}