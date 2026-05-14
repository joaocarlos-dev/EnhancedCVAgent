using EnhancedCVAgent.Domain.Enums;
using EnhancedCVAgent.Domain.Exceptions;

namespace EnhancedCVAgent.Domain.ValueObjects;

public class MatchScore
{
    public MatchScore(int technicalScore, int totalScore, IEnumerable<Skill> missingSkills, ConfidenceLevel confidence)
    {
        TechnicalScore = technicalScore;
        TotalScore = totalScore;
        MissingSkills = missingSkills.ToList().AsReadOnly();
        Confidence = confidence;
    }

    public int TotalScore { get; }
    public int TechnicalScore { get; }
    public IReadOnlyCollection<Skill> MissingSkills { get;  }
    public ConfidenceLevel Confidence { get; }

    public static MatchScore Create(
        int totalScore,
        int technicalScore,
        IEnumerable<Skill>? missingSkills,
        ConfidenceLevel confidence
    )
    {
        ValidateScore(totalScore, nameof(totalScore));
        ValidateScore(technicalScore, nameof(technicalScore));

        if (missingSkills is null)
        {
            throw new DomainValidationException("Missing Skills cannot be null.");
        }

        
        return new MatchScore(
            technicalScore,
            totalScore,
            missingSkills,
            confidence);
    }

    private static void ValidateScore(int value, string field)
    {
        if (value is < 0 or > 100)
        {
            throw new DomainValidationException($"{field} must be between 0 and 100.");
        } 
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not MatchScore other)
            return false;

        return TotalScore == other.TotalScore &&
               TechnicalScore == other.TechnicalScore &&
               Confidence == other.Confidence &&
               MissingSkills.SequenceEqual(other.MissingSkills);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            TotalScore,
            TechnicalScore,
            Confidence);
    }
    
}