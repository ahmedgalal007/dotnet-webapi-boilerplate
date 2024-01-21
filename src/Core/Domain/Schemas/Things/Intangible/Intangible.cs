using FSH.WebApi.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things;
public abstract class Intangible : Thing
{
    public override string TypeName { get; protected set; } = nameof(Intangible);
}

// TODO ActionAccessSpecification
// TODO AlignmentObject
// TODO Audience
// TODO BedDetails
// TODO Brand
// TODO BroadcastChannel
// TODO BroadcastFrequencySpecification
// TODO Class
// TODO ComputerLanguage
// TODO ConstraintNode
// TODO DataFeedItem
// TODO DefinedTerm
// TODO Demand
// TODO DigitalDocumentPermission
// TODO EducationalOccupationalProgram
// TODO EnergyConsumptionDetails
// TODO EntryPoint
// TODO Enumeration
// TODO FloorPlan
// TODO GameServer
// TODO GeospatialGeometry
// TODO Grant
// TODO HealthInsurancePlan
// TODO HealthPlanCostSharingSpecification
// TODO HealthPlanFormulary
// TODO HealthPlanNetwork
// TODO Invoice
// TODO ItemList
// TODO JobPosting
// TODO Language
// TODO ListItem
// TODO MediaSubscription
// TODO MenuItem
// TODO MerchantReturnPolicy
// TODO MerchantReturnPolicySeasonalOverride
// TODO Observation
// TODO Occupation
// TODO OccupationalExperienceRequirements
// TODO Offer
// TODO Order
// TODO OrderItem
// TODO ParcelDelivery
// TODO Permit
// TODO ProgramMembership
// TODO Property
// TODO PropertyValueSpecification
// TODO Quantity
// TODO Rating
// TODO Reservation
// TODO Role
// TODO Schedule
// TODO Seat
// TODO Series
// TODO Service
// TODO ServiceChannel
// TODO SpeakableSpecification
// TODO StatisticalPopulation
// TODO StructuredValue
// TODO Ticket
// TODO Trip
// TODO VirtualLocation