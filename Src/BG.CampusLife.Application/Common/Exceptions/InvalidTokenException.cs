﻿namespace BG.CampusLife.Application.Common.Exceptions;

public class InvalidTokenException : Exception
{
    public InvalidTokenException() : base("Invalid Token")
    {
    }
}