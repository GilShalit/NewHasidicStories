using Microsoft.AspNetCore.Components;
using static clientHasidicStories.Components.Editions;
using System.Net.Http.Json;
using clientHasidicStories.Classes;

namespace clientHasidicStories.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] HttpClient http { get; set; }
        [Inject] GlobalService globalService { get; set; }
        bool isLoading = true;
        protected override async Task OnInitializedAsync()
        {
            var task1 = GetDocThemes();
            await task1.ContinueWith(t =>
            {
                if (t.IsFaulted || t.Result == null)
                    HandleError(t.Exception);
                else
                    InvokeAsync(async () => await ProcessThemes(t.Result));
            });

            var task2 = GetEditions();
            await task2.ContinueWith(t =>
            {
                if (t.IsFaulted)
                    HandleError(t.Exception);
                else
                    InvokeAsync(async () => await ProcessEditions(t.Result));
            });

            await Task.WhenAll(task1, task2);
            isLoading = false;
        }

        private async Task<clsThemesJson> GetDocThemes()
        {
            try
            {
                HttpResponseMessage response = await http.GetAsync("api/get-ana");
                if (response.IsSuccessStatusCode)
                {
                    clsThemesJson themesJson = await response.Content.ReadFromJsonAsync<clsThemesJson>();
                    return themesJson;
                }
                else
                {
                    HandleError(new Exception($"Failed to get themes. Status code: {response.StatusCode}"));
                    return null;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return null;
            }
        }

        private async Task ProcessThemes(clsThemesJson result)
        {
            try
            {
                string story = "";
                string[] themeNames = [];
                clsThemes localThemes = new clsThemes();
                clsTheme theme;
                for (int i = 0; i < result.all.Length; i += 2)
                {
                    story = result.all[i];
                    themeNames = result.all[i + 1].Split(";");
                    for (int j = 0; j < themeNames.Length; j++)
                    {
                        localThemes.newTheme(themeNames[j],story);
                        theme = localThemes.Find(t => t.name == themeNames[j].Split(":")[0].Trim());
                    }
                }
                globalService.Themes = localThemes;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private async Task<clsFileList> GetEditions()
        {
            try
            {
                clsFileList files;
                HttpResponseMessage response = await http.GetAsync("api/filelist");

                if (response.IsSuccessStatusCode)
                {
                    files = await response.Content.ReadFromJsonAsync<clsFileList>();
                    return files;
                }
                else
                {
                    HandleError(new Exception($"Failed to get editions. Status code: {response.StatusCode}"));
                    return null;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return null;
            }
        }

        private async Task ProcessEditions(clsFileList result)
        {
            try
            {
                clsEditions editions = new clsEditions();
                foreach (Li lItem in result.li)
                {
                    editions.Add(new clsEdition(lItem.pbrestricted.pbajax.url, lItem.header.div.div[0].a.h5.text));
                }
                globalService.Editions = editions;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void HandleError(Exception ex)
        {
            // Handle the error here
        }


    }
}
