using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_competition : IMutationEntity
{
	public int feed_reference_id { get; set; }

	public string name { get; set; }

	public string code { get; set; }

	public Guid? t_country_id { get; set; }

	public string logo_url { get; set; }

	public bool is_active { get; set; }

	public string current_season { get; set; }

	public virtual t_country t_country { get; set; }

	public virtual ICollection<t_leaderboard_snapshot> t_leaderboard_snapshots { get; set; } = new List<t_leaderboard_snapshot>();

	public virtual ICollection<t_match> t_matches { get; set; } = new List<t_match>();

	public virtual ICollection<t_private_league> t_private_leagues { get; set; } = new List<t_private_league>();
}
