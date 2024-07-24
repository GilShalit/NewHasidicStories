using Blazorise.TreeView;
using clientHasidicStories.Classes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace clientHasidicStories.Components
{
    public partial class Themes
    {
        [Inject] GlobalService globalService { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Parameter] public string caption { get; set; }
        [Parameter] public string clear { get; set; }
        [Parameter] public string all { get; set; }
        [Parameter] public string close { get; set; }

        IList<clsTheme> selectedNodes = new List<clsTheme>();
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
            {
                if (node.selected) selectedNodes.Add(node);
                foreach (clsTheme child in node.children)
                {
                    if (child.selected) selectedNodes.Add(child);
                }
            }
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
        private void OnSelectedNodesChanged(IList<clsTheme> nodes)
        {
            bool topNodeSelectedBefore;
            bool topNodeSelectedNow;
            bool topNodeUnSelectedNow;
            foreach (clsTheme node in globalService.Themes)
            {
                topNodeSelectedBefore = node.selected;

                //add top node to selectedNodes if in nodes
                if (nodes.Contains(node)) selectNode(node);
                else unSelectNode(node);

                topNodeSelectedNow = node.selected && !topNodeSelectedBefore;
                topNodeUnSelectedNow = !node.selected && topNodeSelectedBefore;

                //add children to selectedNodes if in nodes
                foreach (clsTheme child in node.children)
                {
                    if (nodes.Contains(child)) selectNode(child);
                    else unSelectNode(child);
                }

                //select children and expand if top node changed to selected now
                if (topNodeSelectedNow)
                {
                    expandedNodes.Add(node);
                    foreach (clsTheme child in node.children) selectNode(child);
                }

                //unselect children if top node changed to unselected now
                if (topNodeUnSelectedNow)
                    foreach (clsTheme child in node.children) unSelectNode(child);

                _treeView.Reload();
            }

            globalService.updateStoriesAndPoints();
        }

        void selectNode(clsTheme node)
        {
            node.selected = true;
            if (!selectedNodes.Contains(node)) selectedNodes.Add(node);
        }
        void unSelectNode(clsTheme node)
        {
            node.selected = false;
            if (selectedNodes.Contains(node)) selectedNodes.Remove(node);
        }

        Task OnClearAll()
        {
            foreach (clsTheme node in globalService.Themes)
            {
                node.selected = false;
                foreach (clsTheme child in node.children)
                {
                    child.selected = false;
                }
            }
            selectedNodes.Clear();
            globalService.updateStoriesAndPoints();
            return Task.CompletedTask;
        }
        Task OnSelectAll()
        {
            selectedNodes.Clear();
            foreach (clsTheme node in globalService.Themes)
            {
                selectNode(node);
                foreach (clsTheme child in node.children)
                {
                    selectNode(child);
                }
            }
            globalService.updateStoriesAndPoints();
            return Task.CompletedTask;
        }
        Task OnClose()
        {
            expandedNodes.Clear();
            _treeView.Reload();
            return Task.CompletedTask;
        }


    }
}
