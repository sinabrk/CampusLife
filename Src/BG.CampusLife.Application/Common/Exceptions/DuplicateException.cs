﻿namespace BG.CampusLife.Application.Common.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException(string message) : base(message)
    {
    }
}
