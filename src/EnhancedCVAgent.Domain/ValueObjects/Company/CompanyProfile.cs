using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects.Company
{
    public sealed record CompanyProfile
    {
        public CompanyProfile(
            string name,
            Uri? websiteUrl,
            string? mission,
            string? vision,
            string? description,
            string? industry,
            string? headquarters,
            string? size,
            DateTime? foundedAt
            ) 
        {
            if (string.IsNullOrEmpty(name)) 
                throw new DomainValidationException("Company name cannot be empty.");

            if (!Uri.IsWellFormedUriString(websiteUrl?.AbsoluteUri, UriKind.Absolute)) 
                throw new DomainValidationException("Company URL format is wrong.");

            if (foundedAt > DateTime.Today) 
                throw new DomainValidationException("Company Found date cannot be later than today.");
            Name = name;
            WebsiteUrl = websiteUrl;
            Mission = mission;
            Vision = vision;
            Description = description;
            Industry = industry;
            Headquarters = headquarters;
            Size = size;
            FoundedAt = foundedAt;
        }
        public string Name { get; }
        public Uri? WebsiteUrl { get; }
        public string? Mission {  get; }
        public string? Vision { get; }
        public string? Description { get; }
        public string? Industry { get; }
        public string? Headquarters { get; }
        public string? Size { get; }

        public DateTime? FoundedAt { get; }
    }
}
