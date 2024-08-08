using Blazorise.TreeView;
using clientHasidicStories.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace clientHasidicStories.Components
{
    public partial class StoryThemes
    {
        [Inject] GlobalService globalService { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Parameter] public string caption { get; set; }
        [Parameter] public string clear { get; set; }
        [Parameter] public string all { get; set; }
        [Parameter] public string close { get; set; }

        IList<clsTheme> expandedNodes = new List<clsTheme>();
        private TreeView<clsTheme> _treeView;

        protected override void OnInitialized()
        {
            globalService.OnGlobalThemesChanged += HandleGlobalThemesChange;
        }
        public void Dispose()
        {
            globalService.OnGlobalThemesChanged -= HandleGlobalThemesChange;
        }
        private void HandleGlobalThemesChange()
        {
            foreach (clsTheme node in globalService.Themes)
                expandedNodes.Add(node);
            StateHasChanged();
            FixAlignment();
        }
        private string fixName(string name)
        {
            string n = name.Replace("_", " ").Replace("-", " ");
            n = char.ToUpper(n[0]) + n.Substring(1);
            return n;
        }
        async Task OnExpandedNodeChanged(IList<clsTheme> nodes)
        {
            await FixAlignment();
        }

        async Task FixAlignment()
        {
            if (CultureInfo.CurrentCulture.Name == "he-IL")
            {
                await Task.Delay(5);
                await JS.InvokeVoidAsync("changeClassStyle", "b-tree-view-node", "margin", "0 1.25rem 0 0");
                await JS.InvokeVoidAsync("changeClassStyle", "b-tree-view-node-title", "margin", "0 1.25rem 0 0");
                await JS.InvokeVoidAsync("changeClassStyle", "b-tree-view-node-icon", "float", "right");
                await JS.InvokeVoidAsync("changeClass", "fa-chevron-right", "fa-chevron-left");
            }
        }
    }
}
