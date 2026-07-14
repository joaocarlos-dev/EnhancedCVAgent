using EnhancedCVAgent.Application.Matching.DTOs;
using EnhancedCVAgent.Domain.Entities;
using EnhancedCVAgent.Domain.Services.MatchService;
using EnhancedCVAgent.Domain.ValueObjects;
using EnhancedCVAgent.Domain.ValueObjects.JobOpportunity;
using MediatR;

namespace EnhancedCVAgent.Application.Matching.Commands
{
    public sealed class CalculateCandidateJobMatchCommandHandler
        : IRequestHandler<CalculateCandidateJobMatchCommand, MatchResultDto>
    {
        private readonly IMatchingService _matchingService;

        public CalculateCandidateJobMatchCommandHandler(IMatchingService matchingService)
        {
            _matchingService = matchingService;
        }

        public Task<MatchResultDto> Handle(
            CalculateCandidateJobMatchCommand request,
            CancellationToken cancellationToken)
        {
            var candidate = BuildCandidate(request.Candidate);
            var jobOpportunity = BuildJobOpportunity(request.JobOpportunity);

            var report = _matchingService.Match(candidate, jobOpportunity);

            var result = new MatchResultDto
            {
                TotalScore = report.Score?.TotalScore ?? 0,
                TechnicalScore = report.Score?.TechnicalScore ?? 0,
                Confidence = report.Score?.Confidence ?? default,
                MissingRequiredSkills = report.MissingRequiredSkills.Select(skill => skill.Name).ToList(),
                MissingPreferredSkills = report.MissingPreferredSkills.Select(skill => skill.Name).ToList(),
                Reasons = report.Reasons.ToList()
            };

            return Task.FromResult(result);
        }

        private static CandidateProfile BuildCandidate(CandidateProfileInput input)
        {
            var candidate = new CandidateProfile(input.FullName, input.ProfessionalSummary);

            foreach (var skill in input.Skills)
            {
                candidate.AddSkill(new Skill(skill.Name, skill.Level));
            }

            return candidate;
        }

        private static JobOpportunity BuildJobOpportunity(JobOpportunityInput input)
        {
            var jobOpportunity = new JobOpportunity(
                input.Url,
                input.Title,
                input.CompanyName,
                input.Description);

            foreach (var requirement in input.JobSkillRequirements)
            {
                jobOpportunity.AddJobSkillRequirement(
                    new JobSkillRequirement(
                        requirement.Skill,
                        requirement.SkillExperienceTime,
                        requirement.RequirementType));
            }

            return jobOpportunity;
        }
    }
}
