using Base.Entity;

namespace LigoraX.Domain.Entities;

public partial class t_user_badge : IMutationEntity
{
	public Guid t_user_id { get; set; }

	public Guid t_badge_id { get; set; }

	public virtual t_badge t_badge { get; set; }

	public virtual t_user t_user { get; set; }
}
