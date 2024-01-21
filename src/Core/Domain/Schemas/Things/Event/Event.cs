using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things;
public abstract class Event : Thing
{
    public override string TypeName { get; protected set; } = nameof(Event);
}

// TODO BusinessEvent
// TODO ChildrensEvent
// TODO ComedyEvent
// TODO CourseInstance
// TODO DanceEvent
// TODO DeliveryEvent
// TODO EducationEvent
// TODO EventSeries
// TODO ExhibitionEvent
// TODO Festival
// TODO FoodEvent
// TODO Hackathon
// TODO LiteraryEvent
// TODO MusicEvent
// TODO PublicationEvent
// TODO SaleEvent
// TODO ScreeningEvent
// TODO SocialEvent
// TODO SportsEvent
// TODO TheaterEvent
// TODO VisualArtsEvent
