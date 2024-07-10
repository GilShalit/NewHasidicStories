using clientHasidicStories.Classes;

namespace clientHasidicStories
{
    public class GlobalService
    {
        private clsGeoJson _points;
        private clsThemes _themes;
        private clsEditionFiles _editionfiles;
        private clsPersons _persons;
        private bool _TEILoadedOnce;
        private bool _displayStories;
        public clsStoryInfoData StoryInfoData { get; set; }
        public clsDisplayStoryTexts DisplayStoryTexts { get; set; }
        public TEI AuthoritiesData { get; set; }
        public bool diplayStories
        {
            get=>_displayStories;
            set
            {
                if (_displayStories != value)
                {
                    _displayStories = value;
                    OnDisplayStoriesChanged?.Invoke();
                }
            }
        }
        public clsGeoJson Points
        {
            get => _points;
            set
            {
                if (_points != value)
                {
                    _points = value;
                    OnGlobalPointsChanged?.Invoke();
                }
            }
        }
        public clsThemes Themes
        {
            get => _themes;
            set
            {
                if (_themes != value)
                {
                    _themes = value;
                    OnGlobalThemesChanged?.Invoke();
                }
            }
        }
        public clsEditionFiles EditionFiles
        {
            get => _editionfiles;
            set
            {
                if (_editionfiles != value)
                {
                    _editionfiles = value;
                    OnGlobalEditionsChanged?.Invoke();
                }
            }
        }
        public clsPersons Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                OnGlobalPersonsChanged?.Invoke();
            }
        }
        public bool TEILoadedOnce { get { bool loaded = _TEILoadedOnce; _TEILoadedOnce = true; return loaded; } init { _TEILoadedOnce = false; } }
        public bool DataLoaded { get; set; }

        public event Action OnGlobalEditionsChanged;
        public event Action OnGlobalThemesChanged;
        public event Action OnGlobalPersonsChanged;
        public event Action OnGlobalPointsChanged;
        public event Action OnDisplayStoriesChanged;
    }
}
