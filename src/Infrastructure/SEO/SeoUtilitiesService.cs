using DocumentFormat.OpenXml.Wordprocessing;
using FSH.WebApi.Application.Common.SEO;
using Meziantou.Framework;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Infrastructure.SEO;
public class SeoUtilitiesService : ISeoUtilitiesService
{
    private readonly SEOSettings _seoSettings;
    private readonly ILogger _logger;
    public SeoUtilitiesService(IConfiguration config, ILogger logger)
    {
        _seoSettings = config.GetSection(nameof(SEOSettings)).Get<SEOSettings>() ?? new SEOSettings();
        _logger = logger;

        // _logger.Information("SEO: News Slug Max Length {newsSlugMaxLength}", seoSettings.NewsSlugMaxLength);
        _logger.Information("SEO: News Title Max Length {newsTitleMaxLength}", _seoSettings.NewsTitleMaxLength);
        _logger.Information("SEO: SEO Title Max Length {seoTitleMaxLength}", _seoSettings.SEOTitleMaxLength);
        _logger.Information("SEO: Social Title Max Length {socialTitleMaxLength}", _seoSettings.SocialTitleMaxLength);

        // var options = new SlugOptions()
        // {
        //    MaximumLength = 220,
        //    Separator = "-",
        //    CanEndWithSeparator = false,
        //    CasingTransformation = CasingTransformation.ToLowerCase,
        //    Culture=new System.Globalization.CultureInfo("ar-EG")
        // };

        // options.AllowedRanges.Add(UnicodeRange.Create('ا','ي'));
        // options.AllowedRanges.Add(UnicodeRange.Create('٠', '۹'));
        //  options.AllowedRanges.Add(UnicodeRange.Create('À', 'ü'));

        const string toSlugTitle = " الرئيسية  أفريقيا\r\nحصاد ٢٠٢٣| هل يشهد العام الجديد وداع «أفريقيا الفرنسية»؟";

        // const string toSlugTitle = @"Où trouver Campus France près de chez vous ?";

        // var slugged = StringHelper.UrlFriendly(toSlugTitle, _seoSettings.NewsSlugMaxLength);
        // string slugged = Regex.Replace(toSlugTitle, @"/[^a-z0-9_ءاأإآؤئبتثجحخدذرزسشصضطظعغفقكلمنهويةى\s-]/u", "-");
        string slugged = Slugify(toSlugTitle,false);
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        _logger.Information("Title: {myTitle}", toSlugTitle);
        _logger.Information("Slugyfied: {slug}", slugged);
    }

    public string Slugify(string text, bool isLtr = false)
    {
        string _tmp = string.Empty;
        IEnumerable<string> lines = isLtr ? text.Split("\r\n").Reverse() : text.Split("\r\n");
        foreach (string item in lines)
        {
            _tmp += item;
        }

        foreach (string item in _seoSettings.TrimExpression.Split(','))
        {
            _tmp = _tmp.Replace(item, string.Empty);
        }

        _tmp = Slug.Create(_tmp, _seoSettings.SlugOptions);

        return _tmp;
    }
}