using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_derby : IMutationEntity
{
	public Guid t_team_a_id { get; set; }

	public Guid t_team_b_id { get; set; }

	public decimal default_multiplier { get; set; }

	public virtual t_team t_team_a { get; set; }

	public virtual t_team t_team_b { get; set; }
}
