using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.DTOs
{
    public class JobSkillRequirementInput
    {
        public JobSkillRequirementInput(string skill, string? skillExperienceTime, RequirementType requirementType)
        {
            Skill = skill;
            SkillExperienceTime = skillExperienceTime;
            RequirementType = requirementType;
        }
        public string Skill {  get; set; }
        public string? SkillExperienceTime { get; set; }
        public RequirementType RequirementType { get; set; }
    }
}
