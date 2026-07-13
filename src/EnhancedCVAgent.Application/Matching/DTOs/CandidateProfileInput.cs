using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.DTOs
{
    public class CandidateProfileInput
    {
        public CandidateProfileInput(string professionalSummary, string fullName, List<SkillInput> skills)
        {
            ProfessionalSummary = professionalSummary;
            FullName = fullName;
            Skills = skills;
        }
        public List<SkillInput> Skills { get; set; }
        public string FullName { get; set; }
        public string ProfessionalSummary { get; set; }
    }
}
