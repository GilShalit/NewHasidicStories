namespace clientHasidicStories
{
    public class clsThemesJson
    {
        public string[] all { get; set; }
    }

    public class clsTheme
    {
        public string name { get; set; }
        public List<clsTheme> children { get; set; }
        public List<string> stories { get; set; }
    }
    public class clsThemes
    {
        public List<clsTheme> themes { get; set; }
    }
}
