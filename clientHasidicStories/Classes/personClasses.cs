namespace clientHasidicStories.Classes
{
    public class clsPerson
    {
        public string name { get; set; }
        public List<clsPerson> children { get; set; }
        public List<string> stories { get; set; }
        public clsPerson(string name)
        {
            this.name = name;
            children = new List<clsPerson>();
            stories = new List<string>();
        }
    }
    public class clsPersons : List<clsPerson>
    {
        public new void Add(clsPerson person)
        {
            base.Add(person);
        }

        // Expose all elements as a read-only list
        public IReadOnlyList<clsPerson> Elements => this;
        public void newPerson(string personName, string story)
        {
            clsPerson person = base.Find(p => p.name == personName);
            if(person == null)
            {
                person = new clsPerson(personName);
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
