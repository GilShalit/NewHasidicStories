namespace clientHasidicStories.Classes
{
    public class clsDataJson
    {
        public string all { get; set; }
    }

    public class clsTheme //: IEquatable<clsTheme>
    {
        public string name { get; set; }
        public bool selected { get; set; }
        public bool hasSelected
        {
            get
            {
                if (selected) return true;
                return children.Where(c => c.selected).Any();
            }
        }
        public List<clsTheme> children { get; set; }
        public List<string> stories { get; set; }
        public clsTheme()
        {
            children = new List<clsTheme>();
            stories = new List<string>();
            selected=true;
        }
        //public bool Equals(clsTheme? other)
        //{
        //    // Check if the other object is null
        //    if (other == null) return false;

        //    // Check if the Titles are equal
        //    return this.name == other.name;
        //}
    }
    public class clsThemes : List<clsTheme>
    {
        public new void Add(clsTheme theme)
        {
            throw new Exception("Add method not supported");
        }

        //All stories that belong to any selected themes
        public List<string> StoryIdsInAnySelectedTheme
        {
            get
            {
                List<string> selectedStoryIds = new List<string>();
                foreach (clsTheme theme in this)
                    foreach (clsTheme child in theme.children)//only look at second level
                        if (child.selected)
                            selectedStoryIds.AddRange(child.stories);
                return selectedStoryIds.Distinct().ToList();
            }
        }
        //All stories that belong to ALL selected themes
        public List<string> StoryIdsInALLSelectedThemes
        {
            get
            {
                var selectedStoryIds = new List<string>();
                var selectedThemes = this.SelectMany(theme => theme.children)//Only looking at second level
                                         .Where(child => child.selected)
                                         .ToList();

                if (selectedThemes.Any())
                {
                    selectedStoryIds = selectedThemes
                        .Select(child => child.stories)
                        .Aggregate((previousList, nextList) => previousList.Intersect(nextList).ToList());
                }

                return selectedStoryIds;
            }
        }

        //themes a story belongs to
        public List<clsTheme> storyThemes(string storyId)
        {
            List<clsTheme> result = new List<clsTheme>();

            foreach (var topTheme in this)
            {
                List<clsTheme> matchingChildren = topTheme.children
                    .Where(child => child.stories.Contains(storyId))
                    .ToList();

                if (topTheme.stories.Contains(storyId) || matchingChildren.Any())
                {
                    clsTheme themeWithMatchingChildren = new clsTheme
                    {
                        name = topTheme.name,
                        selected = true,
                        stories = topTheme.stories,
                        children = matchingChildren
                    };
                    result.Add(themeWithMatchingChildren);
                }
            }

            return result;
        }

        // Expose all elements as a read-only list
        public IReadOnlyList<clsTheme> Elements => this;

        public void newTheme(string themeNames, string story)
        {
            string[] themeParts = themeNames.Split(":");
            for (int i = 0; i < themeParts.Length; i++)
            {
                themeParts[i] = themeParts[i].Trim();
            }
            clsTheme currentTopTheme = Find(t => t.name == themeParts[0]);
            clsTheme bottomTheme;
            if (currentTopTheme == null)//new top theme
            {
                currentTopTheme = new clsTheme { name = themeParts[0] };
                base.Add(currentTopTheme);
                if (themeParts.Length > 1)
                {
                    bottomTheme = new clsTheme { name = themeParts[1] };
                    bottomTheme.stories.Add(story);
                    currentTopTheme.children.Add(bottomTheme);
                }
                else //only add story to topTheme if for this story it has no children
                    currentTopTheme.stories.Add(story); //leave breakpoint to make sure no single level theme exists

            }
            else//topTheme exists
            {
                if (themeParts.Length > 1)
                {
                    bottomTheme = currentTopTheme.children.Find(t => t.name == themeParts[1]);
                    if (bottomTheme == null)
                    {
                        bottomTheme = new clsTheme { name = themeParts[1] };
                        bottomTheme.stories.Add(story);
                        currentTopTheme.children.Add(bottomTheme);
                    }
                    else
                    {
                        bottomTheme.stories.Add(story);
                    }
                }
                else //only add story to topTheme if for this story it has no children
                    currentTopTheme.stories.Add(story);
            }
        }
        public bool hasSelected => this.Where(e => e.hasSelected).Any();
    }
}
