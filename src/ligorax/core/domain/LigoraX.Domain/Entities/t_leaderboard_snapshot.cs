using Base.Entity;

namespace LigoraX.Domain.Entities;

public partial class t_leaderboard_snapshot : IMutationEntity
{
	public Guid t_user_id { get; set; }

	public Guid? t_competition_id { get; set; }

	public int? matchday { get; set; }

	public int total_points { get; set; }

	public int? rank { get; set; }

	public virtual t_competition t_competition { get; set; }

	public virtual t_user t_user { get; set; }
}
