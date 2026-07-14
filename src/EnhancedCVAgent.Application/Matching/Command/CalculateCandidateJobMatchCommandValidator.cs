using EnhancedCVAgent.Application.Matching.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Application.Matching.Command
{
    public class CalculateCandidateJobMatchCommandValidator: AbstractValidator<CalculateCandidateJobMatchCommand>
    {
        public CalculateCandidateJobMatchCommandValidator() 
        {
            RuleFor(x => x.Candidate)
                .NotNull().WithMessage("Candidate is required.");

            RuleFor(x => x.JobOpportunity)
                .NotNull().WithMessage("JobOpportunity is required.");

            When(x => x.Candidate != null, () =>
            {
                RuleFor(x => x.Candidate.FullName)
                    .NotEmpty().WithMessage("Candidate fullname cannot be empty.");

                RuleFor(x => x.Candidate.ProfessionalSummary)
                    .NotEmpty().WithMessage("Professional summary cannot be empty.");

                RuleFor(x => x.Candidate.Skills)
                    .Must(skills => skills != null && skills.Count > 0)
                    .WithMessage("Skills cannot be null or less than 1");

                RuleForEach(x => x.Candidate.Skills)
                    .ChildRules(s => s.RuleFor(i => i.Name).NotEmpty());
            });

            When(x => x.JobOpportunity != null, () =>
            {
                RuleFor(x => x.JobOpportunity.CompanyName)
                    .NotEmpty().WithMessage("Company name cannot be null or empty.");

                RuleFor(x => x.JobOpportunity.Title)
                    .NotEmpty().WithMessage("Job title cannot be null or empty.");

                RuleFor(x => x.JobOpportunity.Description)
                    .NotEmpty().WithMessage("Job description cannot be null or empty.");

                RuleFor(x => x.JobOpportunity.Url)
                    .NotEmpty()
                    .Must(BeAValidHttpUrl).WithMessage("A URL da vaga é inválida.");

                RuleFor(x => x.JobOpportunity.JobSkillRequirements)
                    .Must(JobSkillRequirements => JobSkillRequirements != null && JobSkillRequirements.Count > 0)
                    .WithMessage("Job skill requirements cannot be null or less than 1.");

                RuleForEach(x => x.JobOpportunity.JobSkillRequirements)
                    .ChildRules(r => r.RuleFor(i => i.Skill).NotEmpty());
            });
        }

        private static bool BeAValidHttpUrl(string url) =>
            Uri.TryCreate(url, UriKind.Absolute, out var uri) 
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}
