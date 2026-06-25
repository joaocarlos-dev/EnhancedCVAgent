using EnhancedCVAgent.Domain.Enums;
using EnhancedCVAgent.Domain.Exceptions;

namespace EnhancedCVAgent.Domain.ValueObjects;

public class CompanyCultureTrait
{
    public CompanyCultureTrait(CultureTraitType type, TraitIntensity intensity, ConfidenceLevel confidence, string source)
    {
        Type = type;
        Intensity = intensity;
        Confidence = confidence;
        Source = source;
    }

    public CultureTraitType Type { get; }
    public TraitIntensity Intensity { get;  }
    public ConfidenceLevel Confidence { get;  }
    public string Source { get; }

    public static CompanyCultureTrait Create(
        CultureTraitType type,
        TraitIntensity intensity,
        ConfidenceLevel confidence,
        string source
    )
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            throw new DomainValidationException("Culture trait source must be provided.");
        }

        return new CompanyCultureTrait(type, intensity, confidence, source);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not CompanyCultureTrait other)
            return false;
        return Type == other.Type &&
               Intensity == other.Intensity &&
               Confidence == other.Confidence &&
               Source.Equals(other.Source, StringComparison.OrdinalIgnoreCase);
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(
            Type,
            Intensity,
            Confidence,
            Source.ToLowerInvariant());
    }
}