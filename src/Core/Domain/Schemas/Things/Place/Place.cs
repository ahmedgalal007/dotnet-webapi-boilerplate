
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things;
public abstract class Place : Thing
{
    public override string TypeName { get; protected set; } = nameof(Place);
}

// TODO Accommodation
// TODO AdministrativeArea
// TODO CivicStructure
// TODO Landform
// TODO LandmarksOrHistoricalBuildings
// TODO LocalBusiness
// TODO Residence
// TODO TouristAttraction
// TODO TouristDestination