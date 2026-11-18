namespace PropBuild.Domain.Authorization;

public interface IOrganizationOwnedResource
{
    Guid OrganizationId { get; }
}
