using EnhancedCVAgent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.DTOs
{
    public class SkillInput
    {
        public SkillInput(string name, SkillLevel skillLevel)
        {
            Name = name;
            SkillLevel = skillLevel;
        }
        public string Name { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }
}
