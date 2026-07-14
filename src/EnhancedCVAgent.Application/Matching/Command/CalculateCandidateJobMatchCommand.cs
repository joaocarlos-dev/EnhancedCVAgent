using EnhancedCVAgent.Application.Matching.DTOs;
using MediatR;

namespace EnhancedCVAgent.Application.Matching.Commands
{
    public sealed class CalculateCandidateJobMatchCommand : IRequest<MatchResultDto>
    {
        public CalculateCandidateJobMatchCommand(
            CandidateProfileInput candidate,
            JobOpportunityInput jobOpportunity)
        {
            Candidate = candidate;
            JobOpportunity = jobOpportunity;
        }

        public CandidateProfileInput Candidate { get; set; }
        public JobOpportunityInput JobOpportunity { get; set; }
    }
}
