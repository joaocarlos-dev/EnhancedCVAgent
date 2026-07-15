using EnhancedCVAgent.Domain;
using EnhancedCVAgent.Domain.Entities;
using EnhancedCVAgent.Domain.Enums;
using EnhancedCVAgent.Domain.Exceptions;
using EnhancedCVAgent.Domain.Services.MatchService;
using EnhancedCVAgent.Domain.ValueObjects;
using EnhancedCVAgent.Domain.ValueObjects.JobOpportunity;

namespace EnhancedCVAgent.UnitTests.Services;

public class MatchingServiceTests
{
    private readonly MatchingService _sut = new();

    [Fact]
    public void Match_WhenCandidateIsNull_ThrowsDomainValidationException()
    {
        // Arrange
        var jobOpportunity = CreateJobOpportunity();

        // Act
        var exception = Record.Exception(() => _sut.Match(null!, jobOpportunity));

        // Assert
        Assert.IsType<DomainValidationException>(exception);
    }

    [Fact]
    public void Match_WhenJobOpportunityIsNull_ThrowsDomainValidationException()
    {
        // Arrange
        var candidate = CreateCandidate();

        // Act
        var exception = Record.Exception(() => _sut.Match(candidate, null!));

        // Assert
        Assert.IsType<DomainValidationException>(exception);
    }

    [Fact]
    public void Match_WhenJobHasNoSkillRequirements_ReturnsZeroScoreWithLowConfidence()
    {
        // Arrange
        var candidate = CreateCandidate();
        var jobOpportunity = CreateJobOpportunity();

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        Assert.NotNull(report.Score);
        Assert.Equal(0, report.Score!.TotalScore);
        Assert.Equal(ConfidenceLevel.Low, report.Score.Confidence);
        Assert.Empty(report.SkillMatches);
    }

    [Fact]
    public void Match_WhenCandidateMeetsRequiredSkillAtExpertLevel_ReturnsFullScoreAndHighConfidence()
    {
        // Arrange
        var candidate = CreateCandidate(("csharp", SkillLevel.Expert));
        var jobOpportunity = CreateJobOpportunity(("csharp", RequirementType.Required));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        Assert.Equal(100, report.Score!.TotalScore);
        Assert.Equal(100, report.Score.TechnicalScore);
        Assert.Equal(ConfidenceLevel.High, report.Score.Confidence);
        Assert.Empty(report.MissingRequiredSkills);
        Assert.Single(report.SkillMatches);
    }

    [Fact]
    public void Match_WhenCandidateMeetsRequiredSkillAtIntermediateLevel_ReturnsHalfScore()
    {
        // Arrange
        var candidate = CreateCandidate(("csharp", SkillLevel.Intermediate));
        var jobOpportunity = CreateJobOpportunity(("csharp", RequirementType.Required));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        Assert.Equal(50, report.Score!.TotalScore);
        Assert.Equal(ConfidenceLevel.High, report.Score.Confidence);
    }

    [Fact]
    public void Match_WhenRequiredSkillIsMissing_AddsSkillToMissingRequiredSkills()
    {
        // Arrange
        var candidate = CreateCandidate(("java", SkillLevel.Expert));
        var jobOpportunity = CreateJobOpportunity(("csharp", RequirementType.Required));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        var missing = Assert.Single(report.MissingRequiredSkills);
        Assert.Equal("csharp", missing.Name);
        Assert.Empty(report.SkillMatches);
    }

    [Fact]
    public void Match_WhenPreferredSkillIsMissing_AddsSkillToMissingPreferredSkills()
    {
        // Arrange
        var candidate = CreateCandidate(("csharp", SkillLevel.Expert));
        var jobOpportunity = CreateJobOpportunity(
            ("csharp", RequirementType.Required),
            ("docker", RequirementType.Preferred));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        var missing = Assert.Single(report.MissingPreferredSkills);
        Assert.Equal("docker", missing.Name);
        Assert.Empty(report.MissingRequiredSkills);
    }

    [Fact]
    public void Match_WhenHalfOfRequiredSkillsAreMissing_ReturnsMediumConfidence()
    {
        // Arrange
        var candidate = CreateCandidate(("csharp", SkillLevel.Expert));
        var jobOpportunity = CreateJobOpportunity(
            ("csharp", RequirementType.Required),
            ("java", RequirementType.Required));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        Assert.Equal(ConfidenceLevel.Medium, report.Score!.Confidence);
    }

    [Fact]
    public void Match_WhenAllRequiredSkillsAreMissing_ReturnsLowConfidence()
    {
        // Arrange
        var candidate = CreateCandidate(("java", SkillLevel.Expert));
        var jobOpportunity = CreateJobOpportunity(("csharp", RequirementType.Required));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        Assert.Equal(ConfidenceLevel.Low, report.Score!.Confidence);
    }

    [Fact]
    public void Match_WhenSkillMatches_RecordsMatchedPoints()
    {
        // Arrange
        var candidate = CreateCandidate(("csharp", SkillLevel.Advanced));
        var jobOpportunity = CreateJobOpportunity(("csharp", RequirementType.Required));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        var skillMatch = Assert.Single(report.SkillMatches);
        Assert.Equal(RequiredWeight * (int)SkillLevel.Advanced, skillMatch.Points);
        Assert.Equal("csharp", skillMatch.CandidadeSkill.Name);
    }

    [Fact]
    public void Match_WhenPreferredSkillMatched_UsesPreferredWeightForPoints()
    {
        // Arrange
        var candidate = CreateCandidate(("docker", SkillLevel.Expert));
        var jobOpportunity = CreateJobOpportunity(("docker", RequirementType.Preferred));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        var skillMatch = Assert.Single(report.SkillMatches);
        Assert.Equal(PreferredWeight * (int)SkillLevel.Expert, skillMatch.Points);
    }

    [Fact]
    public void Match_WhenSkillCasingDiffers_MatchesCaseInsensitively()
    {
        // Arrange
        var candidate = CreateCandidate(("CSharp", SkillLevel.Expert));
        var jobOpportunity = CreateJobOpportunity(("csharp", RequirementType.Required));

        // Act
        var report = _sut.Match(candidate, jobOpportunity);

        // Assert
        Assert.Single(report.SkillMatches);
        Assert.Empty(report.MissingRequiredSkills);
        Assert.Equal(100, report.Score!.TotalScore);
    }

    private const int RequiredWeight = 2;
    private const int PreferredWeight = 1;

    private static CandidateProfile CreateCandidate(params (string Name, SkillLevel Level)[] skills)
    {
        var candidate = new CandidateProfile("Jane Doe", "Experienced software engineer.");

        foreach (var (name, level) in skills)
            candidate.AddSkill(new Skill(name, level));

        return candidate;
    }

    private static JobOpportunity CreateJobOpportunity(
        params (string Skill, RequirementType Type)[] requirements)
    {
        var jobOpportunity = new JobOpportunity(
            "https://jobs.example.com/123",
            "Backend Developer",
            "Contoso",
            "Build and maintain backend services.");

        foreach (var (skill, type) in requirements)
            jobOpportunity.AddJobSkillRequirement(new JobSkillRequirement(skill, null, type));

        return jobOpportunity;
    }
}
