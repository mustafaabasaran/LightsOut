using LightsOut.Application.Persistence;
using LightsOut.Domain.Models;
using LightsOut.Persistence.Context;

namespace LightsOut.Persistence.Repositories
{
    public class BoardSettingsRepository : GenericRepository<BoardSetting>, IBoardSettingRepository
    {
        public BoardSettingsRepository(LightsOutContext context) : base(context)
        {
        }
    }
}