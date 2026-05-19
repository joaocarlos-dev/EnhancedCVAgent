using EnhancedCVAgent.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnhancedCVAgent.Domain.ValueObjects
{
    public class Language
    {
        public Language(string languageName, string languageLevel)
        {
            if (string.IsNullOrWhiteSpace(languageName))
            {
                throw new DomainValidationException("Language name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(languageLevel))
            {
                throw new DomainValidationException("Language level cannot be empty.");
            }

            LanguageName = languageName;
            LanguageLevel = languageLevel;
        }
        public string LanguageName {  get;}
        public string LanguageLevel { get;}
    }
}
