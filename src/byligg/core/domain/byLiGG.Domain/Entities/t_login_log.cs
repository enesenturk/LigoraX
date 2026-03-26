using Base.Entity;

namespace byLiGG.Domain.Entities;

public partial class t_login_log : IMutationEntity
{
	public Guid? t_user_id { get; set; }

	public string ip_address { get; set; }

	public string user_agent { get; set; }

	public bool is_success { get; set; }

	public string failed_reason { get; set; }

	public virtual t_user t_user { get; set; }
}
