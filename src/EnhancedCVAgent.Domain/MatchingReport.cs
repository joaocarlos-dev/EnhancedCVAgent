using EnhancedCVAgent.Domain.ValueObjects;
using EnhancedCVAgent.Domain.ValueObjects.CandidateProfile;
using EnhancedCVAgent.Domain.ValueObjects.Matching;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain
{
    public sealed class MatchingReport
    {
        public MatchScore? Score { get; private set; }
        public MatchingReport(MatchScore score)
        {
            Score = score;
        }

        private readonly List<Skill> _missingRequiredSkills = [];
        private readonly List<Skill> _missingPreferredSkills = [];
        private readonly List<SkillMatch> _skillMatches = [];
        private readonly List<string> _reasons = [];

        public IReadOnlyCollection<string> Reasons => _reasons.AsReadOnly();

        public IReadOnlyCollection<Skill> MissingRequiredSkills => _missingRequiredSkills.AsReadOnly();
        public IReadOnlyCollection<Skill> MissingPreferredSkills => _missingPreferredSkills.AsReadOnly();
        public IReadOnlyCollection<SkillMatch> SkillMatches => _skillMatches.AsReadOnly();

        public void AddMissingRequiredSkill(Skill requiredSkill)
        {
            _missingRequiredSkills.Add(requiredSkill);
            _reasons.Add($"Missing required skill '{requiredSkill}'");
        }

        public void AddMissingPreferredSkill(Skill preferredSkill)
        {
            _missingPreferredSkills.Add(preferredSkill);
            _reasons.Add($"Missing preferred skill '{preferredSkill}'");
        }

        public void AddSkillMatch(SkillMatch skillMatch)
        {
            _skillMatches.Add(skillMatch);
            _reasons.Add($"Matched skill '{skillMatch}'");
        }

        public void SetScore(MatchScore score)
        {
            Score = score;
        }
    }
}
