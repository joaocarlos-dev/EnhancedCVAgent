using EnhancedCVAgent.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.DTOs
{
    public class MatchResultDto
    {
        public int TotalScore { get; set; }
        public int TechnicalScore { get; set; }
        public List<string> MissingRequiredSkills { get; set; }
        public List<string> MissingPreferredSkills {  get; set; }
        public List<string> Reasons { get; set; }
        public ConfidenceLevel Confidence { get; set; }
    }
}
