using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things;
public abstract class Organization : Thing
{
    public override string TypeName { get; protected set; } = nameof(Organization);
}

// TODO Airline
// TODO Consortium
// TODO Corporation
// TODO EducationalOrganization
// TODO FundingScheme
// TODO GovernmentOrganization
// TODO LibrarySystem
// TODO LocalBusiness
// TODO MedicalOrganization
// TODO NGO
// TODO NewsMediaOrganization
// TODO OnlineBusiness
// TODO PerformingGroup
// TODO PoliticalParty
// TODO Project
// TODO ResearchOrganization
// TODO SearchRescueOrganization
// TODO SportsOrganization
// TODO WorkersUnion
