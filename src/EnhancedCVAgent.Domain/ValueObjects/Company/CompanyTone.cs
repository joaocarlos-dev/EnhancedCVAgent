using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.Company
{
    public sealed record CompanyTone
    {
        public CompanyTone(string tone, string? description)
        {
            if (string.IsNullOrEmpty(tone))
                throw new DomainValidationException("Company tone name cannot be empty.");

            Tone = tone;
            Description = description;
        }
        public string Tone { get; }
        public string? Description { get; }
    }
}
