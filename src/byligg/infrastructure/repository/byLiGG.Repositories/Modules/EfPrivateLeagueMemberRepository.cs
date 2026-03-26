using Base.DataAccess.Repositories.Base.Concrete;
using byLiGG.Domain.Entities;
using byLiGG.Persistence.Contexts;
using byLiGG.Repositories.Abstractions.Modules;

namespace byLiGG.Repositories.Modules
{
	public class EfPrivateLeagueMemberRepository : EfMutationRepositoryBase<t_private_league_member, byliggContext>, IPrivateLeagueMemberRepository
	{
	}
}
