﻿namespace BG.CampusLife.Application.Common.Exceptions;

public class MultipleErrorBadException : Exception
{
    public Dictionary<string, string> Errors { get; set; }

    public MultipleErrorBadException(string message, Dictionary<string, string> errors)
        : base(message)
    {
        Errors = errors;
    }

}