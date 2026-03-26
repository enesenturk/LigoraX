using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface ISystemPropertyRepository : IAsyncMutationRepositoryBase<t_system_property>, IMutationRepositoryBase<t_system_property>
	{
	}
}
