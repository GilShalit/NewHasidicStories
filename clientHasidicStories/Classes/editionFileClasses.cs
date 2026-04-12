using System.Text.RegularExpressions;

namespace clientHasidicStories.Classes
{
    public class clsEditionFile : IEquatable<clsEditionFile>
    {
        const string TitlePattern = @"^(?<en>.+?)\s+(?<year>\d{4})\s+(?<he>.+)$";
        string _file;
        string _title;
        string _titleEnglish;
        string _titleHebrew;

        public string name { get { return Path.GetFileNameWithoutExtension(_file); } }
        public bool selected { get; set; }
        public string title { get { return _title; } }
        public string titleEnglish { get { return _titleEnglish; } }
        public string titleHebrew { get { return _titleHebrew; } }

        public clsEditionFile(string file, string title)
        {
            _file = file;
            _title = title;
            children = new List<clsEditionFile>();
            selected = true;
            ParseTitleParts();
        }
        //public List<string> stories { get; set; }
        public List<clsEditionFile> children { get; set; }//just so can be used in treeview

        public bool Equals(clsEditionFile? other)
        {
            // Check if the other object is null
            if (other == null) return false;

            // Check if the Titles are equal
            return this.title == other.title;
        }

        void ParseTitleParts()
        {
            Match match = Regex.Match(_title ?? string.Empty, TitlePattern);
            if (match.Success)
            {
                string year = match.Groups["year"].Value;
                _titleEnglish = $"{match.Groups["en"].Value} {year}";
                _titleHebrew = $"{match.Groups["he"].Value} {year}";
            }
            else
            {
                _titleEnglish = _title;
                _titleHebrew = _title;
            }
        }
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
        public string getFileName(string title)
        {
            return this.Where(e => e.title == title).FirstOrDefault().name;
        }
    }
}
