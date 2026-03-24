namespace Base.Entity;

public class IMutationEntity : IReadEntity
{

	public Guid id { get; set; }

	public Guid created_by { get; set; }
	public DateTime created_at { get; set; }

	public Guid? updated_by { get; set; }
	public DateTime? updated_at { get; set; }

}