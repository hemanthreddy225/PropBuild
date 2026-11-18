namespace PropBuild.Domain.Entities;

public sealed class Membership
{
    public Guid OrganizationId { get; set; }
    public Guid UserId { get; set; }
    public OrganizationRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Organization Organization { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}
