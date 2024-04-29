using clientHasidicStories.Classes;

namespace clientHasidicStories
{
    public class GlobalService
    {
        private clsThemes _themes;
        private clsEditionFiles _editions;
        private clsPersons _persons;
        private bool _TEILoadedOnce;
        public clsEditionsData EditionsData { get; set;}
        public TEI Authorities{ get; set; }
        public clsThemes Themes
        {
            get => _themes;
            set
            {
                if (_themes!= value)
                {
                    _themes = value;
                    OnGlobalThemesChanged?.Invoke();
                }
            }
        }
        public clsEditionFiles Editions
        {
            get => _editions;
            set
            {
                if (_editions != value)
                {
                    _editions = value;
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
        public bool TEILoadedOnce { get { bool loaded = _TEILoadedOnce;_TEILoadedOnce = true; return loaded; } init { _TEILoadedOnce = false; } }
        public bool DataLoaded { get; set; }

        public event Action OnGlobalEditionsChanged;
        public event Action OnGlobalThemesChanged;
        public event Action OnGlobalPersonsChanged;
    }
}
