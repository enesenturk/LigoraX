using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_user_stat : IMutationEntity
{
	public Guid t_user_id { get; set; }

	public int total_points { get; set; }

	public int total_predictions { get; set; }

	public int correct_predictions { get; set; }

	public int exact_scores { get; set; }

	public int current_streak { get; set; }

	public int best_streak { get; set; }

	public virtual t_user t_user { get; set; }
}
