namespace Base.Entity;

public class IMutationEntity : IReadEntity
{

	public int id { get; set; }

	public int create_user { get; set; }
	public DateTime create_date { get; set; }

	public int? update_user { get; set; }
	public DateTime? update_date { get; set; }

}