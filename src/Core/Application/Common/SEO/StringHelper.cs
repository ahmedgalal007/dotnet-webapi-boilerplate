﻿using Meziantou.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FSH.WebApi.Application.Common.SEO;
public static class StringHelper
{
    /// <summary>
    /// Creates a URL And SEO friendly slug
    /// </summary>
    /// <param name="text">Text to slugify</param>
    /// <param name="maxLength">Max length of slug</param>
    /// <returns>URL and SEO friendly string</returns>
    public static string UrlFriendly(string text, int maxLength = 0)
    {
        // Return empty value if text is null
        if (text == null) return "";

        var normalizedString = text
            // Make lowercase
            .ToLowerInvariant();
            // Normalize the text
            //.Normalize(NormalizationForm.FormD);

        var stringBuilder = new StringBuilder();
        var stringLength = normalizedString.Length;
        var prevdash = false;
        var trueLength = 0;


        // normalizedString = new Regex(@"/[^a-z0-9_\s-ءاأإآؤئبتثجحخدذرزسشصضطظعغفقكلمنهويةى]/u", RegexOptions.Compiled).Replace(normalizedString, string.Empty);
        char c;

        for (int i = 0; i < stringLength; i++)
        {
            c = normalizedString[i];

            switch (CharUnicodeInfo.GetUnicodeCategory(c))
            {
                // Check if the character is a letter or a digit if the character is a
                // international character remap it to an ascii valid character
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (c < 128)
                        stringBuilder.Append(c);
                    else
                        stringBuilder.Append(ConstHelper.RemapInternationalCharToAscii(c));

                    prevdash = false;
                    trueLength = stringBuilder.Length;
                    break;

                // Check if the character is to be replaced by a hyphen but only if the last character wasn't
                case UnicodeCategory.SpaceSeparator:
                case UnicodeCategory.ConnectorPunctuation:
                case UnicodeCategory.DashPunctuation:
                case UnicodeCategory.OtherPunctuation:
                case UnicodeCategory.MathSymbol:
                    if (!prevdash)
                    {
                        stringBuilder.Append('-');
                        prevdash = true;
                        trueLength = stringBuilder.Length;
                    }
                    break;
            }

            // If we are at max length, stop parsing
            if (maxLength > 0 && trueLength >= maxLength)
                break;
        }

        // Trim excess hyphens
        var result = stringBuilder.ToString().Trim('-');

        // Remove any excess character to meet maxlength criteria
        return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
    }

}
