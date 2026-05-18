using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects
{
    public class Education
    {
        public Education(string school, string graduation, string graduationLevel, DateOnly startDate, DateOnly? endDate)
        {
            if (string.IsNullOrWhiteSpace(school))
            {
                throw new DomainValidationException("School cannot be empty.");
            }
            if(string.IsNullOrWhiteSpace(graduation))
            {
                throw new DomainValidationException("Graduation cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(graduationLevel))
            {
                throw new DomainValidationException("Graduation cannot be empty.");
            }
            if (endDate.HasValue && endDate.Value < startDate)
            {
                throw new DomainValidationException("Gradution end date cannot be before start date.");
            }

            School = school.Trim();
            Graduation = graduation.Trim();
            GraduationLevel = graduationLevel.Trim();
            StartDate = startDate;
            EndDate = endDate;
        }
        public string School {  get; }
        public string Graduation { get; }
        public string GraduationLevel { get; }
        public DateOnly StartDate { get; }
        public DateOnly? EndDate { get; }
        
    }
}
