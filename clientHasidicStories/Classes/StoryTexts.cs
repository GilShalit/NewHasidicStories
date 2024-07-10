namespace clientHasidicStories.Classes
{
    public class clsDisplayStoryTexts
    {
        public List<clsEditionStories> editions { get; set; } = new List<clsEditionStories>();
        public void reset()
        {
            foreach (clsEditionStories e in editions)
            {
                e.display = false;
                foreach (clsStoryText s in e.stories) s.display = false;
            }
        }
    }
    public class clsEditionStories
    {
        public string name { get; set; } = "";
        public bool display { get; set; } = false;
        public List<clsStoryText> stories { get; set; } = new List<clsStoryText>();
    }
    public class clsStoryText
    {
        public string id { get; set; } = "";
        public bool display { get; set; } = false;
        public string text { get; set; } = "";

    }

}
