using EnhancedCVAgent.Domain.Common;
using EnhancedCVAgent.Domain.Exceptions;
using EnhancedCVAgent.Domain.ValueObjects;
using EnhancedCVAgent.Domain.ValueObjects.Company;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.Entities
{
    public class Company : Entity
    {
        public Company(CompanyProfile companyProfile)
        {
            CompanyProfile = companyProfile;
        }

        public IReadOnlyCollection<CompanyCultureTrait> CompanyCultureTraits => _companyCultureTraits;
        public IReadOnlyCollection<CompanyValue> CompanyValues => _companyValues;
        public IReadOnlyCollection<CompanyTone> CompanyTones => _companyTones;
        private readonly List<CompanyCultureTrait> _companyCultureTraits = [];
        private readonly List<CompanyValue> _companyValues = [];
        public CompanyProfile CompanyProfile;
        private readonly List<CompanyTone> _companyTones = [];

        public void AddCompanyValue(CompanyValue companyValue) 
        {
            if (string.IsNullOrEmpty(companyValue.Value))
                throw new DomainValidationException("Company value cannot be empty.");
            _companyValues.Add(companyValue);
        }

        public void AddCompanyTone(CompanyTone companyTone)
        {
            if (string.IsNullOrEmpty(companyTone.Tone))
                throw new DomainValidationException("Company tone cannot be empty.");

            _companyTones.Add(companyTone);
        }

        public void AddCompanyCultureTrait(CompanyCultureTrait companyCultureTrait)
        {
            if (string.IsNullOrWhiteSpace(companyCultureTrait.Source))
                throw new DomainValidationException("Culture trait source must be provided.");
            _companyCultureTraits.Add(companyCultureTrait);
        }
    }
}
