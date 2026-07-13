using EnhancedCVAgent.Domain.ValueObjects.JobOpportunity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.DTOs
{
    public class JobOpportunityInput
    {
        public JobOpportunityInput(List<JobSkillRequirement> jobSkillRequirements, string url, string title, string companyName, string description)
        {
            JobSkillRequirements = jobSkillRequirements;
            Url = url;
            Title = title;
            CompanyName = companyName;
            Description = description;
        }
        public string Url { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public List<JobSkillRequirement> JobSkillRequirements { get; set; }
    }
}
