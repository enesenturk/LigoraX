using Base.Entity;

namespace LigoraX.Domain.Entities;

public partial class t_system_property : IMutationEntity
{
	public Guid t_system_property_type_id { get; set; }

	public string language_key { get; set; }

	public short sort_order { get; set; }

	public virtual ICollection<t_badge> t_badges { get; set; } = new List<t_badge>();

	public virtual ICollection<t_match> t_matches { get; set; } = new List<t_match>();

	public virtual ICollection<t_prediction> t_predictions { get; set; } = new List<t_prediction>();

	public virtual ICollection<t_private_league_member> t_private_league_members { get; set; } = new List<t_private_league_member>();

	public virtual t_scoring_rule t_scoring_rule { get; set; }

	public virtual t_system_property_type t_system_property_type { get; set; }
}
