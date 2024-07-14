namespace clientHasidicStories.Classes
{
    public class clsStoryText
    {
        public string id { get; set; } = "";
        public bool display { get; set; } = false;
        public string text { get; set; } = "";

    }
    public class clsEditionStories
    {
        public string name { get; set; } = "";
        public bool display { get; set; } = false;
        public int iEdition { get; set; }
        public List<clsStoryText> stories { get; set; } = new List<clsStoryText>();
    }
    public class clsDisplayStoryTexts
    {
        public List<clsEditionStories> editions { get; set; } = new List<clsEditionStories>();
        public string counts
        {
            get
            {
                // a) Total number of stories in editions
                int totalStories = editions.SelectMany(edition => edition.stories).Count();

                // b) Total number of stories where display == true in editions
                int displayedStories = editions.SelectMany(edition => edition.stories)
                                                .Count(story => story.display);

                return $"{displayedStories}/{totalStories}";
            }
        }
        public void reset()
        {
            foreach (clsEditionStories e in editions)
            {
                e.display = false;
                foreach (clsStoryText s in e.stories) s.display = false;
            }
        }
    }

}
