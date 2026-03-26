using Base.DataAccess.Repositories.Base.Abstract;
using byLiGG.Domain.Entities;

namespace byLiGG.Repositories.Abstractions.Modules
{
	public interface ILoginLogRepository : IAsyncMutationRepositoryBase<t_login_log>, IMutationRepositoryBase<t_login_log>
	{
	}
}
