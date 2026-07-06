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

        private readonly List<MissingSkill> _missingRequiredSkills = [];
        private readonly List<MissingSkill> _missingPreferredSkills = [];
        private readonly List<SkillMatch> _skillMatches = [];
        private readonly List<string> _reasons = [];

        public IReadOnlyCollection<string> Reasons => _reasons.AsReadOnly();

        public IReadOnlyCollection<MissingSkill> MissingRequiredSkills => _missingRequiredSkills.AsReadOnly();
        public IReadOnlyCollection<MissingSkill> MissingPreferredSkills => _missingPreferredSkills.AsReadOnly();
        public IReadOnlyCollection<SkillMatch> SkillMatches => _skillMatches.AsReadOnly();

        public void AddMissingRequiredSkill(MissingSkill requiredSkill)
        {
            _missingRequiredSkills.Add(requiredSkill);
            _reasons.Add($"Missing required skill '{requiredSkill}'");
        }

        public void AddMissingPreferredSkill(MissingSkill preferredSkill)
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
