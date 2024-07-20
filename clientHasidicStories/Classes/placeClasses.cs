namespace clientHasidicStories.Classes
{
    public class clsPlace:IEquatable<clsPlace>
    {
        public string name { get; set; }
        public bool selected { get; set; }
        public string xmlref { get; set; }
        public List<clsPlace> children { get; set; } // only for display in tree
        public List<string> stories { get; set; }
        public clsPlace(string xmlref)
        {
            this.xmlref = xmlref;
            children = new List<clsPlace>();
            stories = new List<string>();
            selected = true;
        }
        public bool Equals(clsPlace? other)
        {
            // Check if the other object is null
            if (other == null) return false;

            // Check if the cmlRefs are equal
            return this.xmlref == other.xmlref;
        }
    }
    public class clsPlaces : List<clsPlace>
    {
        //public bool hasNames { get; set; }
        public new void Add(clsPlace place)
        {
            throw new Exception("Add method not supported");
        }

        //story ids for selected place
        public List<string> selectedStoryIds(string placeId)
        {
            return this.Where(p => p.xmlref == placeId).First().stories.ToList();
        }

        // Expose all elements as a read-only list
        public IReadOnlyList<clsPlace> Elements => this;
        public void newPlace(string xmlref, string story)
        {
            clsPlace place = base.Find(p => p.xmlref == xmlref);
            if(place == null)
            {
                place = new clsPlace( xmlref);
                place.stories.Add(story);
                base.Add(place);
            }
            else
            {
                place.stories.Add(story);
            }
        }
    }
}
