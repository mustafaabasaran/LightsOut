using LightsOut.Domain.Models;
using LightsOut.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.Api.IntegrationTests
{
    public  static class Utilities
    {
        public static void InitializeDbForTests(LightsOutContext db)
        {
            db.BoardSettings.Add(GetSeedingSetting());
            db.InitialStates.AddRange(GetInitialStateList());
            db.SaveChanges();
        }

        public static BoardSetting GetSeedingSetting()
        {
            return new BoardSetting()
            {
                Id = 1,
                OnColor = "#0300ff",
                OffColor = "#000000",
                Size = 5
            };
        }

        public static List<InitialState> GetInitialStateList()
        {
            return new List<InitialState>()
            {
                new InitialState(){ Row = 0, Column = 0, State = 1},
                new InitialState(){ Row = 0, Column = 2, State = 1},
                new InitialState(){ Row = 1, Column = 4, State = 1},
                new InitialState(){ Row = 2, Column = 4, State = 1},
                new InitialState(){ Row = 3, Column = 1, State = 1},
            };
        }

    }
}
