namespace clientHasidicStories.Classes
{
    public class clsEdition
    {
        string _file;
        string _title;
        public string name { get { return Path.GetFileNameWithoutExtension(_file); }  }
        public string title { get { return _title; } }
        public clsEdition(string file, string title)
        {
            _file = file;
            _title = title;
            children = new List<clsEdition>();
        }
        //public List<string> stories { get; set; }
        public List<clsEdition> children { get; set; }
    }
    public class clsEditions : List<clsEdition>
    {
        public new void Add(clsEdition edition)
        {
            base.Add(edition);
        }

        // Expose all elements as a read-only list
        public IReadOnlyList<clsEdition> Elements => this;
    }
}
