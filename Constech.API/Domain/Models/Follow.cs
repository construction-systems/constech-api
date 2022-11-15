namespace Constech.API.Domain.Models;

public class Follow
{
    public Guid Id { get; set; }
    public Guid FollowerId { get; set; }
    public User Follower { get; set; }
    public Guid FollowingProjectId { get; set; }
    public Project FollowingProject { get; set; }
    public DateTime FollowedAt { get; set; } = DateTime.Now;
}