using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.JobOpportunity
{
    public sealed record JobSkillRequirement
    {
        public JobSkillRequirement(string skill, string? skillExperienceTime, RequirementType skillRequirementType)
        {
            if (string.IsNullOrEmpty(skill)){
                throw new DomainValidationException("Skill cannot be empty.");
            }

            Skill = skill.Trim().ToLowerInvariant();
            SkillExperienceTime = skillExperienceTime;
            SkillRequirementType = skillRequirementType;
        }
        public string Skill { get; }
        public string? SkillExperienceTime { get; }
        public RequirementType SkillRequirementType { get; }

    }
}
