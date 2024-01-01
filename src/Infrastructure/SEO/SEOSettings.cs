using FSH.WebApi.Domain.Common.Localizations;
using Meziantou.Framework;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Text.Unicode;

namespace FSH.WebApi.Infrastructure.SEO;
public class SEOSettings
{
    public int? NewsSlugMaxLength { get; set; } = 60;
    public int? NewsTitleMaxLength { get; set; } = 60;
    public int? NewsSubTitleMaxLength { get; set; } = 60;
    public int? SEOTitleMaxLength { get; set; } = 60;
    public int? SocialTitleMaxLength { get; set; } = 60;
    public SlugUnicodeRange[]? SlugUnicodeRanges { get; set; } = new SlugUnicodeRange[] { };
    public string? Separator { get; set; } = "-";
    public bool? CanEndWithSeparator { get; set; } = false;
    public bool? LoggerEnabled { get; set; } = false;
    public string? CasingTransformatione { get; set; } = "ToLowerCase"; // ToLowerCase/ToUpperCase/PreserveCase
    public string? Culture { get; set; } = "ar-EG";
    public string? TrimExpression { get; set; }
    public SlugOptions? SlugOptions
    {
        get
        {
            var options = new SlugOptions
            {
                MaximumLength = (int)NewsSlugMaxLength!,
                Separator = Separator!,
                CanEndWithSeparator = false,
                CasingTransformation = CasingTransformation.ToLowerCase,
                Culture = new System.Globalization.CultureInfo(Culture!)
            };

            switch (CasingTransformatione)
            {
                case "ToLowerCase":
                    options.CasingTransformation = CasingTransformation.ToLowerCase;
                    break;
                case "ToUpperCase":
                    options.CasingTransformation = CasingTransformation.ToUpperCase;
                    break;
                case "PreserveCase":
                    options.CasingTransformation = CasingTransformation.PreserveCase;
                    break;
                default:
                    options.CasingTransformation = CasingTransformation.ToLowerCase;
                    break;
            }

            if (options.AllowedRanges.Count == 0)
            {
                options.AllowedRanges.Add(UnicodeRange.Create('ا', 'ي'));
                options.AllowedRanges.Add(UnicodeRange.Create('٠', '۹'));
                options.AllowedRanges.Add(UnicodeRange.Create('À', 'ü'));
            }
            else
            {
                foreach (SlugUnicodeRange rng in SlugUnicodeRanges)
                {
                    options.AllowedRanges.Add(UnicodeRange.Create(rng.FirstCharacter, rng.LastCharacter));
                }
            }

            return options;
        }

        private set { }
    }
    
}

public class SlugUnicodeRange
{
    public char FirstCharacter { get; set; }
    public char LastCharacter { get; set; }
}