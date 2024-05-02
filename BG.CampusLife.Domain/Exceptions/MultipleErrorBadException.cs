using System;
using System.Collections.Generic;

namespace BG.CampusLife.Domain.Exceptions
{
    public class MultipleErrorBadException : Exception
    {
        public Dictionary<string, string> Errors { get; set; }

        public MultipleErrorBadException(string message, Dictionary<string, string> errors)
            : base(message)
        {
            Errors = errors;
        }

    }
}