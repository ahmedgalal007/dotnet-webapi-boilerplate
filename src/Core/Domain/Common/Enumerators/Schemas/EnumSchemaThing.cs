using FSH.WebApi.Shared.Common;

namespace FSH.WebApi.Domain.Common.Enumerators.Schemas;
public class EnumSchemaThing : Enumeration<EnumSchemaThing>
{
    public static readonly EnumSchemaThing Action = new ActionThing();
    public static readonly EnumSchemaThing BioChemEntity = new BioChemEntityThing();
    public static readonly EnumSchemaThing CreativeWork = new CreativeWorkThing();
    public static readonly EnumSchemaThing Event = new EventThing();
    public static readonly EnumSchemaThing Intangible = new IntangibleThing();
    public static readonly EnumSchemaThing MedicalEntity = new MedicalEntityThing();
    public static readonly EnumSchemaThing Organization = new OrganizationThing();
    public static readonly EnumSchemaThing Person = new PersonThing();
    public static readonly EnumSchemaThing Place = new PlaceThing();
    public static readonly EnumSchemaThing Product = new ProductThing();
    public static readonly EnumSchemaThing Taxon = new TaxonThing();
    private EnumSchemaThing(int value, string name)
        : base(value, name)
    {
    }

    private sealed class ActionThing : EnumSchemaThing
    {
        public ActionThing()
            : base(1, "Action")
        {
        }
    }
    private sealed class BioChemEntityThing : EnumSchemaThing
    {
        public BioChemEntityThing()
            : base(2, "BioChemEntity")
        {
        }
    }
    private sealed class CreativeWorkThing : EnumSchemaThing
    {
        public CreativeWorkThing()
            : base(3, "CreativeWork")
        {
        }
    }
    private sealed class EventThing : EnumSchemaThing
    {
        public EventThing()
            : base(4, "Event")
        {
        }
    }
    private sealed class IntangibleThing : EnumSchemaThing
    {
        public IntangibleThing()
            : base(5, "Intangible")
        {
        }
    }
    private sealed class MedicalEntityThing : EnumSchemaThing
    {
        public MedicalEntityThing()
            : base(6, "MedicalEntity")
        {
        }
    }
    private sealed class OrganizationThing : EnumSchemaThing
    {
        public OrganizationThing()
            : base(7, "Organization")
        {
        }
    }
    private sealed class PersonThing : EnumSchemaThing
    {
        public PersonThing()
            : base(8, "Person")
        {
        }
    }
    private sealed class PlaceThing : EnumSchemaThing
    {
        public PlaceThing()
            : base(9, "Place")
        {
        }
    }
    private sealed class ProductThing : EnumSchemaThing
    {
        public ProductThing()
            : base(10, "Product")
        {
        }
    }
    private sealed class TaxonThing : EnumSchemaThing
    {
        public TaxonThing()
            : base(11, "Taxon")
        {
        }
    }
}
