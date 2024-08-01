using clientHasidicStories.Classes;
using clientHasidicStories.Components;
using Microsoft.JSInterop;

namespace clientHasidicStories
{
    public static class Utils
    {
        public static bool displayStories(GlobalService globalService)
        {
            return globalService.EditionFiles.hasSelected || globalService.Persons.hasSelected || globalService.Themes.hasSelected;
        }

        internal static void changeDisplayStories(GlobalService gs, string placeId = "")
        {
            //turn on selected editions
            gs.DisplayStoryTexts.reset();
            List<string> selectedStoryIds = gs.Themes.selectedStoryIds;
            foreach (clsEditionFile edition in gs.EditionFiles.Where(e => e.selected))
            {
                clsEditionStories editionStories = gs.DisplayStoryTexts.editions.Where(e => e.name == edition.title).First();
                editionStories.display = true;

                foreach (clsStoryText storyText in editionStories.stories)
                {
                    if (placeId == "")
                    {
                        //find if stories include selected themes and selected persons
                        if (selectedStoryIds.Contains(storyText.id) && gs.Persons.selectedStoryIds.Contains(storyText.id))
                            storyText.display = true;
                    }
                    else
                    {
                        //find if stories include selected themes and selected persons AND selected place
                        if (selectedStoryIds.Contains(storyText.id) && gs.Persons.selectedStoryIds.Contains(storyText.id) 
                            && gs.Places.selectedStoryIds(placeId).Contains(storyText.id))
                            storyText.display = true;
                    }
                }
                //find if stories include selected places
                //if selected edition has no selected stories, turn it off
            }
        }
        internal static void changeDisplayPlaces(GlobalService gs)
        {
            List<string> selectedStoryIds = gs.DisplayStoryTexts.selectedStoryIds;
            foreach (clsPlace place in gs.Places)
            {
                foreach (string storyId in place.stories)
                {
                    //find if place stories are shown with selected themes and selected persons
                    if (selectedStoryIds.Contains(storyId))
                        gs.Points.data.changeFeatureSelection(place.xmlref, true);
                    else
                        gs.Points.data.changeFeatureSelection(place.xmlref, false);

                }
            }
        }
    }
    public enum LogicalOperator
    {
        And,
        Or
    }
}
