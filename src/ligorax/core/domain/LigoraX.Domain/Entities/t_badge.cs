using Base.Entity;

namespace LigoraX.Domain.Entities;

public partial class t_badge : IMutationEntity
{
	public string language_key { get; set; }

	public string description_key { get; set; }

	public string icon_url { get; set; }

	public Guid t_system_property_badge_trigger_id { get; set; }

	public int? trigger_value { get; set; }

	public bool is_active { get; set; }

	public virtual t_system_property t_system_property_badge_trigger { get; set; }

	public virtual ICollection<t_user_badge> t_user_badges { get; set; } = new List<t_user_badge>();
}
