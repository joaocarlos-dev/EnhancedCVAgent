using EnhancedCVAgent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.Services.MatchService
{
    public interface IMatchingService
    {
        MatchingReport Match(CandidateProfile candidate, JobOpportunity jobOpportunity);
    }
}
