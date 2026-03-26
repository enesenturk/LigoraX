using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_private_league : IMutationEntity
{
	public string name { get; set; }

	public string description { get; set; }

	public string invite_code { get; set; }

	public Guid t_user_id { get; set; }

	public Guid? t_competition_id { get; set; }

	public bool is_active { get; set; }

	public short max_members { get; set; }

	public virtual t_competition t_competition { get; set; }

	public virtual ICollection<t_private_league_member> t_private_league_members { get; set; } = new List<t_private_league_member>();

	public virtual t_user t_user { get; set; }
}
