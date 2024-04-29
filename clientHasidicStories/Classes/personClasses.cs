namespace clientHasidicStories.Classes
{
    public class clsPerson
    {
        public string name { get; set; }
        public string xmlref { get; set; }
        public List<clsPerson> children { get; set; }
        public List<string> stories { get; set; }
        public clsPerson(string xmlref)
        {
            this.xmlref = xmlref;
            children = new List<clsPerson>();
            stories = new List<string>();
        }
    }
    public class clsPersons : List<clsPerson>
    {
        public bool hasNames { get; set; }
        public new void Add(clsPerson person)
        {
            throw new Exception("Add method not supported");
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
    }
}
