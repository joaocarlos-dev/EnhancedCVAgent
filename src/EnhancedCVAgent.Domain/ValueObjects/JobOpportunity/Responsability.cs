using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.JobOpportunity
{
    public sealed record Responsability
    {
        public Responsability(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new DomainValidationException("Responsability description cannot be empty");
            }
        }
        public string Description { get; }
    }
}
