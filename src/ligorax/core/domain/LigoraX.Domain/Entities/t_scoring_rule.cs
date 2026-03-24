using Base.Entity;

namespace LigoraX.Domain.Entities;

public partial class t_scoring_rule : IMutationEntity
{
	public Guid t_system_property_prediction_result_id { get; set; }

	public short points { get; set; }

	public string description_language_key { get; set; }

	public virtual t_system_property t_system_property_prediction_result { get; set; }
}
