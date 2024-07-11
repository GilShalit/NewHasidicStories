using clientHasidicStories.Classes;
namespace clientHasidicStories
{
    public static class Utils
    {
        public static bool displayStories(GlobalService globalService)
        {
            return globalService.EditionFiles.hasSelected || globalService.Persons.hasSelected || globalService.Themes.hasSelected;
        }

        internal static void changeDisplayStories(bool value, GlobalService gs)
        {
            //turn on selected editions
            foreach (clsEditionFile edition in gs.EditionFiles.Where(e => e.selected))
            {
                clsEditionStories editionStories = gs.DisplayStoryTexts.editions.Where(e => e.name == edition.title).First();
                editionStories.display = value;
                //find if stories include selected themes 
                foreach (clsStoryText storyText in editionStories.stories)
                    if (gs.Themes.selectedStoryIds.Contains(storyText.id)) storyText.display = true;
                //find if stories include selected persons
                //find if stories include selected places
                //if selected edition has no selected stories, turn it off
            }
        }
    }
}
