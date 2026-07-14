using EnhancedCVAgent.Domain.Entities;
using EnhancedCVAgent.Domain.Enums;
using EnhancedCVAgent.Domain.Exceptions;
using EnhancedCVAgent.Domain.ValueObjects;
using EnhancedCVAgent.Domain.ValueObjects.JobOpportunity;
using EnhancedCVAgent.Domain.ValueObjects.Matching;

namespace EnhancedCVAgent.Domain.Services.MatchService
{
    public sealed class MatchingService : IMatchingService
    {
        private const int RequiredWeight = 2;
        private const int PreferredWeight = 1;

        public MatchingReport Match(CandidateProfile candidate, JobOpportunity jobOpportunity)
        {
            if (candidate is null)
                throw new DomainValidationException("Candidate profile cannot be null or empty.");

            if (jobOpportunity is null)
                throw new DomainValidationException("JobOpportunity cannot be null or empty.");

            var report = new MatchingReport(MatchScore.Create(0, 0, [], ConfidenceLevel.Low));

            var candidateSkillsByName = candidate.Skills.ToDictionary(skill => skill.Name);

            var earnedPoints = 0;
            var maxPoints = 0;
            var requiredCount = 0;
            var missingRequiredCount = 0;

            foreach (var requirement in jobOpportunity.JobSkillRequirements)
            {
                var isRequired = requirement.SkillRequirementType == RequirementType.Required;
                var weight = isRequired ? RequiredWeight : PreferredWeight;

                maxPoints += weight * (int)SkillLevel.Expert;

                if (isRequired)
                    requiredCount++;

                if (candidateSkillsByName.TryGetValue(requirement.Skill, out var candidateSkill))
                {
                    var points = weight * (int)candidateSkill.Level;
                    earnedPoints += points;
                    report.AddSkillMatch(new SkillMatch(candidateSkill, ToSkill(requirement), points));
                    continue;
                }

                var missingSkill = ToSkill(requirement);

                if (isRequired)
                {
                    missingRequiredCount++;
                    report.AddMissingRequiredSkill(missingSkill);
                }
                else
                {
                    report.AddMissingPreferredSkill(missingSkill);
                }
            }

            var score = maxPoints == 0
                ? 0
                : (int)Math.Round((double)earnedPoints / maxPoints * 100);

            var confidence = maxPoints == 0
                ? ConfidenceLevel.Low
                : DetermineConfidence(requiredCount, missingRequiredCount);

            var missingSkills = report.MissingRequiredSkills
                .Concat(report.MissingPreferredSkills)
                .ToList();

            report.SetScore(MatchScore.Create(score, score, missingSkills, confidence));

            return report;
        }

        private static Skill ToSkill(JobSkillRequirement requirement) =>
            new(requirement.Skill, SkillLevel.NoKnowledge);

        private static ConfidenceLevel DetermineConfidence(int requiredCount, int missingRequiredCount)
        {
            if (missingRequiredCount == 0)
                return ConfidenceLevel.High;

            if (missingRequiredCount * 2 <= requiredCount)
                return ConfidenceLevel.Medium;

            return ConfidenceLevel.Low;
        }
    }
}
