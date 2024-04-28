namespace clientHasidicStories.Classes
{
    public class clsEditionFile
    {
        string _file;
        string _title;
        public string name { get { return Path.GetFileNameWithoutExtension(_file); }  }
        public string title { get { return _title; } }
        public clsEditionFile(string file, string title)
        {
            _file = file;
            _title = title;
            children = new List<clsEditionFile>();
        }
        //public List<string> stories { get; set; }
        public List<clsEditionFile> children { get; set; }
    }
    public class clsEditionFiles : List<clsEditionFile>
    {
        public new void Add(clsEditionFile edition)
        {
            base.Add(edition);
        }

        // Expose all elements as a read-only list
        public IReadOnlyList<clsEditionFile> Elements => this;
    }
}
