using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_match : IMutationEntity
{
	public int feed_reference_id { get; set; }

	public Guid t_competition_id { get; set; }

	public Guid t_home_team_id { get; set; }

	public Guid t_away_team_id { get; set; }

	public int? matchday { get; set; }

	public Guid t_system_property_match_status_id { get; set; }

	public DateTime starts_at { get; set; }

	public short? home_score { get; set; }

	public short? away_score { get; set; }

	public decimal multiplier { get; set; }

	public DateTime? prediction_deadline { get; set; }

	public DateTime? synced_at { get; set; }

	public virtual t_team t_away_team { get; set; }

	public virtual t_competition t_competition { get; set; }

	public virtual t_team t_home_team { get; set; }

	public virtual ICollection<t_prediction> t_predictions { get; set; } = new List<t_prediction>();

	public virtual t_system_property t_system_property_match_status { get; set; }
}
