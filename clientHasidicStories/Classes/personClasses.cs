namespace clientHasidicStories.Classes
{
    public class clsPerson : IEquatable<clsPerson>
    {
        public string name { get; set; }
        public bool selected { get; set; }
        public string xmlref { get; set; }
        public List<clsPerson> children { get; set; } // only for display in tree
        public List<string> stories { get; set; }
        public clsPerson(string xmlref)
        {
            this.xmlref = xmlref;
            children = new List<clsPerson>();
            stories = new List<string>();
            selected = true;
        }
        public bool Equals(clsPerson? other)
        {
            // Check if the other object is null
            if (other == null) return false;

            // Check if the cmlRefs are equal
            return this.xmlref == other.xmlref;
        }
    }
    public class clsPersons : List<clsPerson>
    {
        public bool hasNames { get; set; }
        public new void Add(clsPerson person)
        {
            throw new Exception("Add method not supported");
        }

        public List<string> selectedStoryIds
        {
            get
            {
                List<string> selectedStoryIds = new List<string>();
                foreach (clsPerson person in this)
                        if (person.selected)
                            selectedStoryIds.AddRange(person.stories);
                return selectedStoryIds.Distinct().ToList();
            }
        }
        // Expose all elements as a read-only list
        public IReadOnlyList<clsPerson> Elements => this;
        public void newPerson(string xmlref, string story)
        {
            clsPerson person = base.Find(p => p.xmlref == xmlref);
            if(person == null)
            {
                person = new clsPerson( xmlref);
                person.stories.Add(story);
                base.Add(person);
            }
            else
            {
                person.stories.Add(story);
            }
        }
        public bool hasSelected => this.Where(e => e.selected).Any();
    }
}
