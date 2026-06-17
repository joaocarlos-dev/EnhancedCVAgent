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
        public string WorkMode { get; }
        public string EmploymentType { get; }
        private readonly List<string> _jobSkillRequirement = [];
        private readonly List<string> Responsibilities = [];
        public string SeniorityLevel { get; }
        public string Qualifications {  get; }
        public DateOnly ExtractedAt { get; }
    }
}
