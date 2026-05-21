using EnhancedCVAgent.Domain.Common;
using EnhancedCVAgent.Domain.Exceptions;
using EnhancedCVAgent.Domain.ValueObjects.CandidateProfile;

namespace EnhancedCVAgent.Domain.Entities;

public class CandidateProfile : Entity
{
    private readonly List<Skill> _skills = [];
    private readonly List<Experience> _experiences = [];
    private readonly List<Education> _educations = [];
    private readonly List<Language> _languages = [];
    private readonly List<Certification> _certifications = [];

    public CandidateProfile(string fullName, string professionalSummary)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainValidationException("Full name is required.");

        if (string.IsNullOrWhiteSpace(professionalSummary))
            throw new DomainValidationException("Professional summary is required.");

        FullName = fullName.Trim();
        ProfessionalSummary = professionalSummary.Trim();
    }

    public string FullName { get; private set; } = string.Empty;
    public string ProfessionalSummary { get; private set; } = string.Empty;

    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();
    public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
    public IReadOnlyCollection<Certification> Certifications => _certifications.AsReadOnly();

    public void UpdateProfessionalSummary(string professionalSummary)
    {
        if (string.IsNullOrWhiteSpace(professionalSummary))
            throw new DomainValidationException("Professional summary is required.");

        ProfessionalSummary = professionalSummary.Trim();
    }

    public void AddSkill(Skill skill)
    {
        EnsureNotNull(skill, nameof(skill));

        if (_skills.Any(existing => existing.Name == skill.Name))
            throw new DomainValidationException("Skill already exists in candidate profile.");

        _skills.Add(skill);
    }

    public void AddExperience(Experience experience)
    {
        EnsureNotNull(experience, nameof(experience));

        _experiences.Add(experience);
    }

    public void AddEducation(Education education)
    {
        EnsureNotNull(education, nameof(education));

        _educations.Add(education);
    }

    public void AddLanguage(Language language)
    {
        EnsureNotNull(language, nameof(language));

        _languages.Add(language);
    }

    public void AddCertification(Certification certification)
    {
        EnsureNotNull(certification, nameof(certification));

        _certifications.Add(certification);
    }

    private static void EnsureNotNull<T>(T value, string fieldName)
    {
        if (value is null)
            throw new DomainValidationException($"{fieldName} cannot be null.");
    }
}