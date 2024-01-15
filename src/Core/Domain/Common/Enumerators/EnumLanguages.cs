using System;
using FSH.WebApi.Shared.Common;

namespace FSH.WebApi.Domain.Common.Enumerators;
public abstract class EnumLanguages : Enumeration<EnumLanguages>
{
    public static readonly EnumLanguages AR = new ArabicLanguage();
    public static readonly EnumLanguages EN = new EnglishLanguage();
    public static readonly EnumLanguages FR = new FrenchLanguage();
    private EnumLanguages(int value, string name)
        : base(value, name)
    {
    }

    public abstract bool IsRTL { get; }
    public abstract string Title { get; }
    public abstract string ISOCode { get; }
    public abstract string TwoLettersCode { get; }
    private sealed class ArabicLanguage : EnumLanguages
    {
        public ArabicLanguage()
            : base(1, "ِArabic")
        {
        }

        public override bool IsRTL => true;
        public override string Title => "العربية";
        public override string ISOCode => "ar-EG";
        public override string TwoLettersCode => "ar";
    }

    private sealed class EnglishLanguage : EnumLanguages
    {
        public EnglishLanguage()
            : base(2, "English")
        {
        }

        public override bool IsRTL => false;
        public override string Title => "English";
        public override string ISOCode => "en-US";
        public override string TwoLettersCode => "en";
    }

    private sealed class FrenchLanguage : EnumLanguages
    {
        public FrenchLanguage()
            : base(3, "French")
        {
        }

        public override bool IsRTL => false;
        public override string Title => "French";
        public override string ISOCode => "fr-FR";
        public override string TwoLettersCode => "fr";

    }
}
