/*
Deprecated / Holded Till better analysis and testing
for localization Impelementation
*/
using System;
// using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Shared.Localizations
{

    //[ComplexType]
    //    [Owned]
    public class LocalizedString
    {
        public string Arabic { get; set; } = string.Empty;
        public string French { get; set; } = string.Empty;
        public string English { get; set; } = string.Empty;
        public string German { get; set; } = string.Empty;
        public override string ToString()
        {
            //switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant())
            //{
            //    case "FR":
            //        return French;
            //    case "EN":
            //        return English;
            //    case "AR":
            //        return Arabic;
            //    case "DE":
            //        return German;
            //    default:
            //        return Arabic;
            //}
            return Arabic;
        }

        [NotMapped]
        public string Current
        {
            get
            {
                switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant())
                {
                    case "FR":
                        return French;
                    case "EN":
                        return English;
                    case "AR":
                        return Arabic;
                    case "DE":
                        return German;
                }
                return ToString();
            }

            set
            {
                switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToUpperInvariant())
                {
                    case "FR":
                        French = value;
                        break;
                    case "EN":
                        English = value;
                        break;
                    case "AR":
                        Arabic = value;
                        break;
                    case "DE":
                        German = value;
                        break;
                }

            }
        }
    }
}
