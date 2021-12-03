namespace LightsOut.Application.Resources
{
    public static class ExceptionMessages
    {
        public const string EmptyBoardSettingError = "Board settings is empty. Please insert settings to database.";
        public const string MoreThanOneBoardSettingError = "There are more than one settings in the database. Please lower it down to one";
        public const string EmptyInitialStateError = "Can not find any initial state. Please insert initial states to database.";
    }
}