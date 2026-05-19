using EnhancedCVAgent.Domain.Common;
using EnhancedCVAgent.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.Entities
{
    public class CandidateProfile : Entity
    {
        public CandidateProfile(string fullName, 
            string professionalSummary, 
            List<Skill> skills, 
            List<Experience> experiences, 
            List<Education> educations, 
            List<Language> languages, 
            List<string> certifications)
        {
            FullName = fullName;
            ProfessionalSummary = professionalSummary;
            Skills = skills;
            Experiences = experiences;
            Educations = educations;
            Languages = languages;
            Certifications = certifications;
        }
        public string FullName { get; protected set;  }
        public string ProfessionalSummary { get; protected set; }
        public List<Skill> Skills { get; protected set; }
        public List<Experience> Experiences { get; protected set;  }
        public List<Education> Educations {  get; protected set; }
        public List<Language> Languages { get; protected set;  }
        public List<string> Certifications { get; protected set; }
    }
}
