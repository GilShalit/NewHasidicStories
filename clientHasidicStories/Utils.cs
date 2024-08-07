using clientHasidicStories.Classes;
using clientHasidicStories.Components;
using clientHasidicStories.Pages;
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

            List<string> selectedThemesStoryIds;//StoryIds of selected themes
            if (gs.logicalOperatorThemes == LogicalOperator.Or) selectedThemesStoryIds = gs.Themes.StoryIdsInAnySelectedTheme;
            else selectedThemesStoryIds = gs.Themes.StoryIdsInALLSelectedThemes;

            List<string> selectedPeopleStoryIds;//StoryIds of selected themes
            if (gs.logicalOperatorPeople == LogicalOperator.Or) selectedPeopleStoryIds = gs.Persons.StoryIdsInAnySelectedPerson;
            else selectedPeopleStoryIds = gs.Persons.StoryIdsInALLSelectedPeople;

            List<string> selectedPlaceStoryIds = new List<string>();
            if (placeId != "") selectedPlaceStoryIds = gs.Places.selectedStoryIds(placeId);//StoryIds of selected place, if placeId is not empty

            foreach (clsEditionFile edition in gs.EditionFiles.Where(e => e.selected))
            {
                clsEditionStories editionStories = gs.DisplayStoryTexts.editions.Where(e => e.name == edition.title).First();
                editionStories.display = true;

                foreach (clsStoryText storyText in editionStories.stories)
                {
                    if (placeId == "")
                    {
                        //find if stories include selected themes and selected persons
                        if (selectedThemesStoryIds.Contains(storyText.id) && selectedPeopleStoryIds.Contains(storyText.id))
                            storyText.display = true;

                        //if (gs.Places.storyPlaces(storyText.id).Count==0)Console.WriteLine($"No places for story {storyText.text.Substring(0,20)} in {edition.title}");
                    }
                    else
                    {
                        //find if stories include selected themes and selected persons AND selected place
                        if (selectedThemesStoryIds.Contains(storyText.id) && selectedPeopleStoryIds.Contains(storyText.id)
                            && selectedPlaceStoryIds.Contains(storyText.id))
                            storyText.display = true;
                    }
                }
            }
        }
        internal static void changeDisplayPlaces(GlobalService gs)
        {
            List<string> selectedStoryIds = gs.DisplayStoryTexts.selectedStoryIds;
            gs.Points.data.unselectAllFeatures();
            foreach (clsPlace place in gs.Places)
            {
                foreach (string storyId in place.stories)
                {
                    //select place stories are shown with selected themes and selected persons
                    if (selectedStoryIds.Contains(storyId))
                        gs.Points.data.selectFeature(place.xmlref);

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
