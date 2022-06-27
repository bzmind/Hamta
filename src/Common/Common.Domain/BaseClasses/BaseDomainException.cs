﻿namespace Common.Domain.BaseClasses;

public class BaseDomainException : Exception
{
    public BaseDomainException()
    {

    }

    public BaseDomainException(string message) : base(message)
    {

    }
}