using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_system_property_type : IMutationEntity
{
	public string language_key { get; set; }

	public short sort_order { get; set; }

	public virtual ICollection<t_system_property> t_system_properties { get; set; } = new List<t_system_property>();
}
