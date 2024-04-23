using clientHasidicStories.Classes;

namespace clientHasidicStories
{
    public class GlobalService
    {
        private clsThemes _themes;
        private clsEditions _editions;
        private bool _TEILoadedOnce;
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
        public clsEditions Editions
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
        public bool TEILoadedOnce { get { bool loaded = _TEILoadedOnce;_TEILoadedOnce = true; return loaded; } init { _TEILoadedOnce = false; } }
        public bool DataLoaded { get; set; }

        public event Action OnGlobalEditionsChanged;
        public event Action OnGlobalThemesChanged;
    }
}
