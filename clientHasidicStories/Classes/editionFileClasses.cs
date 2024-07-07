namespace clientHasidicStories.Classes
{
    public class clsEditionFile : IEquatable<clsEditionFile>
    {
        string _file;
        string _title;
        public string name { get { return Path.GetFileNameWithoutExtension(_file); } }
        public bool selected { get; set; }
        public string title { get { return _title; } }
        public clsEditionFile(string file, string title)
        {
            _file = file;
            _title = title;
            children = new List<clsEditionFile>();
        }
        //public List<string> stories { get; set; }
        public List<clsEditionFile> children { get; set; }

        public bool Equals(clsEditionFile? other)
        {
            // Check if the other object is null
            if (other == null) return false;

            // Check if the Titles are equal
            return this.title == other.title;
        }
        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    clsEditionFile objAsMyClass = obj as clsEditionFile;
        //    if (objAsMyClass == null) return false;
        //    else return Equals(objAsMyClass);
        //}
        //public override int GetHashCode()
        //{
        //    // Use the hash code of the Title string as the hash code for MyClass instances
        //    return title?.GetHashCode() ?? 0;
        //}
    }
    public class clsEditionFiles : List<clsEditionFile>
    {
        public new void Add(clsEditionFile edition)
        {
            base.Add(edition);
        }

        // Expose all elements as a read-only list
        public IReadOnlyList<clsEditionFile> Elements => this;
        public bool hasSelected => this.Where(e => e.selected).Any();
    }
}
