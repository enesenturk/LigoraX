using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_team : IMutationEntity
{
	public int feed_reference_id { get; set; }

	public string name { get; set; }

	public string short_name { get; set; }

	public string tla { get; set; }

	public string logo_url { get; set; }

	public string primary_color { get; set; }

	public string secondary_color { get; set; }

	public Guid? t_country_id { get; set; }

	public virtual t_country t_country { get; set; }

	public virtual ICollection<t_derby> t_derbyt_team_as { get; set; } = new List<t_derby>();

	public virtual ICollection<t_derby> t_derbyt_team_bs { get; set; } = new List<t_derby>();

	public virtual ICollection<t_match> t_matcht_away_teams { get; set; } = new List<t_match>();

	public virtual ICollection<t_match> t_matcht_home_teams { get; set; } = new List<t_match>();

	public virtual ICollection<t_user> t_users { get; set; } = new List<t_user>();
}
