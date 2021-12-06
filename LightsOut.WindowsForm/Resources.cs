using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut.WindowsForm
{
    public class Resources
    {
        public const string SettingsRoute = "/v1/BoardSetting";
        public const string InitialStateRoute = "/v1/InitialState";

        public const string ErrorWhenGettingInitialStateList = "Error when getting the initial states of the board. Please control the API";
        public const string ErrorWhenGettingBoardSetting = "Error when getting the board settings. Please control the API";
        public const string BoardSettingIsNull = "Board settings is null. Please insert settings to db.";
        public const string InitialStateListIsNull = "Initial state is null. Please insert settings to db.";
        public const string WrongInitialState = "One or more inital state value is higher than board size. Please fix initial states";
        public const string WrongInitialStateColor = "Wrong colors. Colors definition must be HEX and starts with \"#\".";
    }
}
