using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_country : IMutationEntity
{
	public string code { get; set; }

	public string flag_url { get; set; }

	public virtual ICollection<t_competition> t_competitions { get; set; } = new List<t_competition>();

	public virtual ICollection<t_team> t_teams { get; set; } = new List<t_team>();
}
