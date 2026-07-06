using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.Matching
{
    public sealed record SkillMatch
        (
        Skill CandidadeSkill, 
        Skill JobSkill, 
        int Points
        );
}
