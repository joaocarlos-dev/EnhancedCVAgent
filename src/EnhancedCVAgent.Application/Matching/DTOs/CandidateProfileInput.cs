using EnhancedCVAgent.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.DTOs
{
    public class CandidateProfileInput
    {
        public CandidateProfileInput(string professionalSummary, string fullName, List<Skill> skills)
        {
            ProfessionalSummary = professionalSummary;
            FullName = fullName;
            Skills = skills;
        }
        public List<Skill> Skills { get; set; }
        public string FullName { get; set; }
        public string ProfessionalSummary { get; set; }
    }
}
