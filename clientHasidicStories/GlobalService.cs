using clientHasidicStories.Classes;

namespace clientHasidicStories
{
    public class GlobalService
    {
        private clsThemes _themes;
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

        public event Action OnGlobalThemesChanged;

        private clsEditions _editions;
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
        public event Action OnGlobalEditionsChanged;
    }
}
