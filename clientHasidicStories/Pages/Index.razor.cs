using Microsoft.AspNetCore.Components;
using static clientHasidicStories.Components.Editions;
using System.Net.Http.Json;
using clientHasidicStories.Classes;
using Microsoft.JSInterop;
using System.IO;
using System.Xml.Serialization;

using clientHasidicStories.Components;
using System.Data.SqlTypes;

namespace clientHasidicStories.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] HttpClient http { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] GlobalService globalService { get; set; }
        bool isLoading = true;
        protected override async Task OnInitializedAsync()
        {
            if (!globalService.DataLoaded)
            {
                isLoading = true;

                var task1 = GetFullData().ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        InvokeAsync(async () => await ProcessData(t.Result));
                    }
                    else
                    {
                        HandleError(t.Exception);
                    }
                });

                var task2 = GetEditions().ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        InvokeAsync(async () => await ProcessEditions(t.Result));
                    }
                    else
                    {
                        HandleError(t.Exception);
                    }
                });

                await Task.WhenAll(task1, task2);
                globalService.DataLoaded = true;

                isLoading = false;
            }
        }

        private async Task<clsEditionsData> GetFullData()
        {
            try
            {
                Console.WriteLine("Start GetDocThemes");
                //await Task.Delay(2000); // Delay for 2 seconds
                //Console.WriteLine("End delay in GetFullData");
                clsEditionsData editions;
                HttpResponseMessage response = await http.GetAsync("api/get-fulldata");
                if (response.IsSuccessStatusCode)
                {
                    clsDataJson dataJson = await response.Content.ReadFromJsonAsync<clsDataJson>();
                    XmlSerializer serializer = new XmlSerializer(typeof(clsEditionsData));
                    using (StringReader reader = new StringReader(dataJson.all))
                    {
                        editions = (clsEditionsData)serializer.Deserialize(reader);
                    }
                    Console.WriteLine("End GetDocThemes");
                    return editions;
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

        private async Task ProcessData(clsEditionsData data)
        {
            try
            {
                Console.WriteLine("Start ProcessData");
                string story = "";
                globalService.EditionsData = data;

                //building persons in separate loops so UI updates as we go
                clsPersons localPersons = new clsPersons();
                for (int e = 0; e < data.editions.Length; e++)
                {
                    for (int s = 0; s < data.editions[e].stories.Length; s++)
                    {
                        story = data.editions[e].stories[s].Id;
                        for (int j = 0; j < data.editions[e].stories[s].persons.Length; j++)
                        {
                            localPersons.newPerson(data.editions[e].stories[s].persons[j], story);
                        }
                    }
                }
                globalService.Persons = localPersons;

                //building themes in separate loops so UI updates as we go
                string[] themeNames = [];
                clsThemes localThemes = new clsThemes();
                for (int e = 0; e < data.editions.Length; e++)
                {
                    for (int s = 0; s < data.editions[e].stories.Length; s++)
                    {
                        story = data.editions[e].stories[s].Id;
                        themeNames = data.editions[e].stories[s].ana.Split(";");
                        for (int j = 0; j < themeNames.Length; j++)
                        {
                            localThemes.newTheme(themeNames[j], story);
                        }
                    }
                }
                globalService.Themes = localThemes;
                Console.WriteLine("End ProcessData");
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
                Console.WriteLine("Start GetEditions");
                clsFileList files;
                HttpResponseMessage response = await http.GetAsync("api/filelist");

                if (response.IsSuccessStatusCode)
                {
                    files = await response.Content.ReadFromJsonAsync<clsFileList>();
                    Console.WriteLine("End GetEditions");
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
                Console.WriteLine("Start ProcessEditions");
                //await Task.Delay(2000); // Delay for 2 seconds
                //Console.WriteLine("End delay in ProcessEditions");
                clsEditionFiles editions = new clsEditionFiles();
                foreach (Li lItem in result.li)
                {
                    editions.Add(new clsEditionFile(lItem.pbrestricted.pbajax.url, lItem.header.div.div[0].a.h5.text));
                }
                globalService.Editions = editions;
                Console.WriteLine("End ProcessEditions");
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
