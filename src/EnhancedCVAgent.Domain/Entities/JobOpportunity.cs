using EnhancedCVAgent.Domain.Exceptions;
using EnhancedCVAgent.Domain.ValueObjects.JobOpportunity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EnhancedCVAgent.Domain.Entities
{
    public class JobOpportunity
    {
        public JobOpportunity(
            string url,
            string title,
            string companyName,
            string description) 
        {
            if (string.IsNullOrEmpty(url)) 
                throw new DomainValidationException("url cannot be empty.");

            if (string.IsNullOrEmpty(title))
                throw new DomainValidationException("title cannot be empty.");

            if (string.IsNullOrEmpty(companyName))
                throw new DomainValidationException("companyName cannot be empty.");

            if (string.IsNullOrEmpty(description))
                throw new DomainValidationException("description cannot be empty.");

            Url = url;
            Title = title;
            CompanyName = companyName;
            Description = description;
        }

        public string Url { get; }
        public string Title { get; }
        public string CompanyName { get; }
        public string Description { get; }
        public WorkMode WorkMode { get; }
        public EmploymentType EmploymentType { get; }
        private readonly List<JobSkillRequirement> _jobSkillRequirements = [];
        private readonly List<Responsability> _responsibilities = [];
        public SeniorityLevel SeniorityLevel { get; }
        private readonly List<Qualification> _qualifications = [];
        public DateOnly ExtractedAt { get; }

        public IReadOnlyCollection<JobSkillRequirement> JobSkillRequirements => 
            _jobSkillRequirements.AsReadOnly();

        public IReadOnlyCollection<Responsability> Responsabilities 
            => _responsibilities.AsReadOnly();

        public IReadOnlyCollection<Qualification> Qualifications 
            => _qualifications.AsReadOnly();

        public void AddJobSkillRequirement(JobSkillRequirement jobSkillRequirement)
        {
            EnsureNotNull(jobSkillRequirement, nameof(jobSkillRequirement));
            _jobSkillRequirements.Add(jobSkillRequirement);
        }

        public void AddResposability(Responsability responsability)
        {
            EnsureNotNull(responsability, nameof(responsability));
            _responsibilities.Add(responsability);
        }

        public void AddQualifications(Qualification qualification)
        {
            EnsureNotNull(qualification, nameof(qualification));
            _qualifications.Add(qualification);
        }

        private static void EnsureNotNull<T>(T value, string fieldName)
        {
            if (value is null)
                throw new DomainValidationException($"{fieldName} cannot be null.");
        }
    }
}
