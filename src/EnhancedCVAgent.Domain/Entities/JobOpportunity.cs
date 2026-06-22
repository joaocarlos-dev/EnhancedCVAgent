using EnhancedCVAgent.Domain.ValueObjects.JobOpportunity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.Entities
{
    public class JobOpportunity
    {
        public string Url { get; }
        public string Title { get; }
        public string CompanyName { get; }
        public string Description { get; }
        public WorkMode WorkMode { get; }
        public EmploymentType EmploymentType { get; }
        private readonly List<JobSkillRequirement> _jobSkillRequirements = [];
        private readonly List<Responsability> _responsibilities = [];
        public SeniorityLevel SeniorityLevel { get; }
        private readonly List<string> _qualifications = [];
        public DateOnly ExtractedAt { get; }

    }
}
