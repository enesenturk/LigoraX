using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_private_league_member : IMutationEntity
{
	public Guid t_private_league_id { get; set; }

	public Guid t_user_id { get; set; }

	public Guid t_system_property_private_league_role_id { get; set; }

	public int total_points { get; set; }

	public int? rank { get; set; }

	public virtual t_private_league t_private_league { get; set; }

	public virtual t_system_property t_system_property_private_league_role { get; set; }

	public virtual t_user t_user { get; set; }
}
