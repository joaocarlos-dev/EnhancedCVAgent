using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.JobOpportunity
{
    public sealed record Qualification
    {
        public Qualification(string qualificationName)
        {
            if (string.IsNullOrEmpty(qualificationName))
            {
                throw new DomainValidationException("Qualification cannot be empty.");
            }

            QualificationName = qualificationName;
        }
        public string QualificationName { get; }
    }
}
