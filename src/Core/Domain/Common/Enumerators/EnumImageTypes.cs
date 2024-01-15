using System;
using FSH.WebApi.Shared.Common;

namespace FSH.WebApi.Domain.Common.Enumerators;
public abstract class EnumImageTypes : Enumeration<EnumImageTypes>
{
    public static readonly EnumImageTypes JPEG = new JpegImageType();
    public static readonly EnumImageTypes PNG = new PngImageType();
    public static readonly EnumImageTypes SVG = new SvgImageType();
    public static readonly EnumImageTypes WEBP = new WebPImageType();
    private EnumImageTypes(int value, string name)
        : base(value, name)
    {
    }

    public abstract bool IsTransparent { get; }

    private sealed class JpegImageType : EnumImageTypes
    {
        public JpegImageType()
            : base(1, "JPEG")
        {
        }

        public override bool IsTransparent => false;
    }

    private sealed class PngImageType : EnumImageTypes
    {
        public PngImageType()
            : base(2, "PNG")
        {
        }

        public override bool IsTransparent => true;
    }

    private sealed class SvgImageType : EnumImageTypes
    {
        public SvgImageType()
            : base(3, "SVG")
        {
        }

        public override bool IsTransparent => true;
    }

    private sealed class WebPImageType : EnumImageTypes
    {
        public WebPImageType()
            : base(4, "WEBP")
        {
        }

        public override bool IsTransparent => true;
    }
}
