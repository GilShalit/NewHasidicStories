using clientHasidicStories.Classes;
using clientHasidicStories.Components;
namespace clientHasidicStories
{
    public static class Utils
    {
        public static bool displayStories(GlobalService globalService)
        {
            return globalService.EditionFiles.hasSelected || globalService.Persons.hasSelected || globalService.Themes.hasSelected;
        }

        internal static void changeDisplayStories(GlobalService gs)
        {
            //turn on selected editions
            gs.DisplayStoryTexts.reset();
            foreach (clsEditionFile edition in gs.EditionFiles.Where(e => e.selected))
            {
                clsEditionStories editionStories = gs.DisplayStoryTexts.editions.Where(e => e.name == edition.title).First();
                editionStories.display = true;

                foreach (clsStoryText storyText in editionStories.stories)
                {
                    //find if stories include selected themes and selected persons
                    if (gs.Themes.selectedStoryIds.Contains(storyText.id) && gs.Persons.selectedStoryIds.Contains(storyText.id))
                        storyText.display = true;

                }
                //find if stories include selected places
                //if selected edition has no selected stories, turn it off
            }
        }
        internal static void changeDisplayPlaces(GlobalService gs)
        {
            foreach (clsPlace place in gs.Places)
            {
                foreach (string storyId in place.stories)
                {
                    //find if place stories are shown with selected themes and selected persons
                    if (gs.DisplayStoryTexts.selectedStoryIds.Contains(storyId) )
                        gs.Points.data.changeFeatureSelection(place.xmlref,true);
                    else
                        gs.Points.data.changeFeatureSelection(place.xmlref,false);

                }
            }
        }
    }
}
