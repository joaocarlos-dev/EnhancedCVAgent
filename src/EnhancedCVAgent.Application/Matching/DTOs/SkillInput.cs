using EnhancedCVAgent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.DTOs
{
    public class SkillInput
    {
        public SkillInput(string name, SkillLevel level)
        {
            Name = name;
            Level = level;
        }
        public string Name { get; set; }
        public SkillLevel Level { get; set; }
    }
}
