using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Schemas.Things.Organization;
public abstract class Organization : Thing
{
    public override string TypeName { get; protected set; } = nameof(Organization);
}

// Airline
// Consortium
// Corporation
// EducationalOrganization
// FundingScheme
// GovernmentOrganization
// LibrarySystem
// LocalBusiness
// MedicalOrganization
// NGO
// NewsMediaOrganization
// OnlineBusiness
// PerformingGroup
// PoliticalParty
// Project
// ResearchOrganization
// SearchRescueOrganization
// SportsOrganization
// WorkersUnion
