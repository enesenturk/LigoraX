using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface ISystemPropertyTypeRepository : IAsyncMutationRepositoryBase<t_system_property_type>, IMutationRepositoryBase<t_system_property_type>
	{
	}
}
