using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects
{
    public sealed record Certification
    {
        public Certification(string name, int totalHours)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainValidationException("Certification name cannot be empty.");
            }

            if (totalHours  < 2)
            {
                throw new DomainValidationException("Total hours cannot be less than 2.");
            }
            Name = name;
            TotalHours = totalHours;
        }

        public string Name { get; }
        public int? TotalHours { get; }
    }
}
