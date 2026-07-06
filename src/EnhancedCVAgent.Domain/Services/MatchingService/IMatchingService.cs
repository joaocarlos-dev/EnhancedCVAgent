using EnhancedCVAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.Services.MatchingService.MatchingService
{
    public interface IMatchingService
    {
        MatchingReport Match(CandidateProfile candidate, JobOpportunity jobOpportunity);
    }
}
