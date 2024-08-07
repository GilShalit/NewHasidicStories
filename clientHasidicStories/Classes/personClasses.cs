namespace clientHasidicStories.Classes
{
    public class clsPerson : IEquatable<clsPerson>
    {
        public string name { get; set; }
        public bool selected { get; set; }
        public string xmlref { get; set; }
        public string link { get; set; }
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

        //All stories in Any selected people
        public List<string> StoryIdsInAnySelectedPerson
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
        //All stories in ALL selected people
        public List<string> StoryIdsInALLSelectedPeople
        {
            get
            {
                var selectedStoryIds = new List<string>();

                if (this.hasSelected)
                {
                    selectedStoryIds = this.Where(person => person.selected)
                        .Select(person => person.stories)
                        .Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
                }

                return selectedStoryIds;
            }
        }
        public List<clsPerson> storyPeople(string storyId)
        {
            List<clsPerson> people = new List<clsPerson>();
            foreach (clsPerson person in this)
                if (person.stories.Contains(storyId))
                    people.Add(person);
            return people;
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
