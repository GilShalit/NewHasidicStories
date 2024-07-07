namespace clientHasidicStories
{
    public static class Utils
    {
        public static bool displayStories (GlobalService globalService)
        {
            return globalService.EditionFiles.hasSelected || globalService.Persons.hasSelected || globalService.Themes.hasSelected;
        }
    }
}
