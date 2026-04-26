using EnhancedCVAgent.Domain.Enums;
using EnhancedCVAgent.Domain.Exceptions;

namespace EnhancedCVAgent.Domain.ValueObjects;

public class Skill
{
    public string Name { get; private set; }
    public SkillLevel Level { get; private set; }

    public Skill(string name, SkillLevel level)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Skill can not be empty.");
        
        Name = name.Trim().ToLowerInvariant();
        Level = level;
    }
}