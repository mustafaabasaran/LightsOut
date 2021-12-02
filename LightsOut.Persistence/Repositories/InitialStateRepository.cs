using LightsOut.Application.Persistence;
using LightsOut.Domain.Models;
using LightsOut.Persistence.Context;

namespace LightsOut.Persistence.Repositories
{
    public class InitialStateRepository : GenericRepository<InitialState>, IInitialStateRepository
    {
        public InitialStateRepository(LightsOutContext context) : base(context)
        {
        }
    }
}