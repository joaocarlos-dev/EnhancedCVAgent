using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.CandidateProfile
{
    public sealed record Experience
    {
        public Experience(string companyName, 
            string role, 
            DateOnly startDate, 
            DateOnly? endDate, 
            string description, 
            IReadOnlyCollection<Skill> skillsUsed)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new DomainValidationException("Company name is required.");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new DomainValidationException("Role is required.");
            }

            if (endDate.HasValue && endDate.Value < startDate)
            {
                throw new DomainValidationException("Experience end date cannot be before start date.");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new DomainValidationException("Experience description is required.");
            }

            CompanyName = companyName.Trim();
            Role = role.Trim();
            StartDate = startDate;
            EndDate = endDate;
            Description = description.Trim();
            SkillsUsed = skillsUsed.ToList().AsReadOnly() 
                ?? throw new DomainValidationException("Skills used cannot be null.");
        }
        public string CompanyName { get; }
        public string Role { get; }
        public DateOnly StartDate { get; }
        public DateOnly? EndDate { get; }
        public string Description { get; }
        public IReadOnlyCollection<Skill> SkillsUsed {  get; }
        public bool IsCurrent => EndDate is null;

    }
}
