using System;
using FSH.WebApi.Shared.Common;

namespace FSH.WebApi.Domain.Common.Enumerators;
public abstract class EnumImageSizes : Enumeration<EnumImageSizes>
{
    public static readonly EnumImageSizes XLarge = new XLargeImageSize();
    public static readonly EnumImageSizes Large = new LargeImageSize();
    public static readonly EnumImageSizes Medium = new MediumImageSize();
    public static readonly EnumImageSizes Small = new XSmallImageSize();
    public static readonly EnumImageSizes XSmall = new XSmallImageSize();
    private EnumImageSizes(int value, string name)
        : base(value, name)
    {
    }

    public abstract int Width { get; }
    public abstract int Height { get; }
    public abstract bool Enabled { get; }
    public abstract string Usage { get; }

    // public static EnumImageSizes? FromUsage(string usage)
    // {
    //     return Enumerations
    //            .Values
    //            .SingleOrDefault(e => e.Usage == usage);
    // }
    private sealed class XLargeImageSize : EnumImageSizes
    {
        public XLargeImageSize()
            : base(1, "XLarge")
        {
        }

        public override int Width => 1920;

        public override int Height => 1080;
        public override string Usage => "Background";
        public override bool Enabled => true;
    }

    private sealed class LargeImageSize : EnumImageSizes
    {
        public LargeImageSize()
            : base(2, "Large")
        {
        }

        public override int Width => 1280;

        public override int Height => 720;
        public override string Usage => "Hero";
        public override bool Enabled => true;
    }

    private sealed class MediumImageSize : EnumImageSizes
    {
        public MediumImageSize()
            : base(3, "Medium")
        {
        }

        public override int Width => 1200;

        public override int Height => 630;
        public override string Usage => "Blog";

        public override bool Enabled => true;
    }

    private sealed class SmallImageSize : EnumImageSizes
    {
        public SmallImageSize()
            : base(2, "Small")
        {
        }

        public override int Width => 250;

        public override int Height => 250;
        public override string Usage => "Banner";
        public override bool Enabled => true;
    }

    private sealed class XSmallImageSize : EnumImageSizes
    {
        public XSmallImageSize()
            : base(2, "XSmall")
        {
        }

        public override int Width => 150;

        public override int Height => 150;
        public override string Usage => "Thumbnail";
        public override bool Enabled => true;
    }

}
