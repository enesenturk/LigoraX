using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_prediction : IMutationEntity
{
	public Guid t_user_id { get; set; }

	public Guid t_match_id { get; set; }

	public short predicted_home { get; set; }

	public short predicted_away { get; set; }

	public Guid? t_system_property_prediction_result_id { get; set; }

	public short base_points { get; set; }

	public decimal multiplier { get; set; }

	public short final_points { get; set; }

	public DateTime? calculated_at { get; set; }

	public virtual t_match t_match { get; set; }

	public virtual t_system_property t_system_property_prediction_result { get; set; }

	public virtual t_user t_user { get; set; }
}
