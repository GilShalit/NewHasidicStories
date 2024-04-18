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
    }
}
