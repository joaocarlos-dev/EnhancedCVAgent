using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.Company
{
    public sealed record CompanyValue
    {
        public CompanyValue(string value, string? description) 
        {
            if (string.IsNullOrEmpty(value)) throw new DomainValidationException("Company value cannot be empty.");
            Value = value;
            Description = description;
        }
        public string Value { get; }
        public string? Description { get; }
    }
}
