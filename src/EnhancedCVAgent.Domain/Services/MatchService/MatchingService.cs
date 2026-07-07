using EnhancedCVAgent.Domain.Entities;
using EnhancedCVAgent.Domain.Exceptions;
using EnhancedCVAgent.Domain.Services.MatchService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.Services.MatchingService
{
    public sealed class MatchingService: IMatchingService
    {
        public MatchingReport Match(CandidateProfile candidate, JobOpportunity jobOpportunity)
        {
            if(candidate is null)
            {
                throw new DomainValidationException("Candidate profile cannot be null or empty.");
            }

            if(jobOpportunity is null)
            {
                throw new DomainValidationException("JobOpportunity cannot be null or empty.");
            }

            throw new NotImplementedException();
        }
    }
}
