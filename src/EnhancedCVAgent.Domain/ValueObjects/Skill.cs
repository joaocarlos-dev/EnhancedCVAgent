using EnhancedCVAgent.Domain.Enums;

namespace EnhancedCVAgent.Domain.ValueObjects;

public class Skill
{
    public string Name { get; private set; }
    public SkillLevel Level { get; private set; }

    public Skill(string name, SkillLevel level)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(name, "Skill can not be empty.");
        
        Name = name.Trim().ToLowerInvariant();
        Level = level;
    }
}