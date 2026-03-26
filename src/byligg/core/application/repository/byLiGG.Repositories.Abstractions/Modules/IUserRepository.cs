using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface IUserRepository : IAsyncMutationRepositoryBase<t_user>, IMutationRepositoryBase<t_user>
	{
	}
}
