using Microsoft.AspNetCore.Components;
using static clientHasidicStories.Components.Editions;
using System.Net.Http.Json;
using clientHasidicStories.Classes;
using Microsoft.JSInterop;
using System.IO;
using System.Xml.Serialization;

using clientHasidicStories.Components;
using System.Data.SqlTypes;
using System.Text.Json;
using System.Net.WebSockets;
using System.Drawing;

namespace clientHasidicStories.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] HttpClient http { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] GlobalService globalService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
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

                var task2 = GetEditionFiles().ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        InvokeAsync(async () => await ProcessEditionFiles(t.Result));
                    }
                    else
                    {
                        HandleError(t.Exception);
                    }
                });
                await Task.WhenAll(task1, task2);

                var task3 = GetAuthorities().ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        InvokeAsync(async () => await ProcessAuthorities(t.Result));
                    }
                    else
                    {
                        HandleError(t.Exception);
                    }
                });
                await task3;

                globalService.DataLoaded = true;

                isLoading = false;
            }
        }
        private async Task<TEI> GetAuthorities()
        {
            try
            {
                Console.WriteLine("Start GetAuthorities");
                //await Task.Delay(2000); // Delay for 2 seconds
                //Console.WriteLine("End delay in GetAuthorities");
                TEI authorities;

                HttpClient httpLocal = new HttpClient();
                httpLocal.BaseAddress = new Uri(Navigation.BaseUri);

                HttpResponseMessage response = await httpLocal.GetAsync("Authorities.xml");
                if (response.IsSuccessStatusCode)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(TEI));
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        authorities = (TEI)serializer.Deserialize(stream);
                    }
                    Console.WriteLine("End GetAuthorities");
                    return authorities;
                }
                else
                {
                    HandleError(new Exception($"Failed to get authorities. Status code: {response.StatusCode}"));
                    return null;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return null;
            }
        }
        private async Task ProcessAuthorities(TEI authorities)
        {
            try
            {
                Console.WriteLine("Start ProcessAuthorities");
                //await Task.Delay(2000); // Delay for 2 seconds
                //Console.WriteLine("End delay in ProcessAuthorities");

                //get Person names
                globalService.Authorities = authorities;
                clsPersons localPersons = globalService.Persons;
                foreach (clsPerson person in localPersons)
                {
                    person.name = authorities.teiHeader.fileDesc.sourceDesc.listPerson.Where(p => p.xmlid == person.xmlref).FirstOrDefault().name;
                }
                localPersons.hasNames = true;
                globalService.Persons = localPersons;

                clsGeoJson localPoints = new clsGeoJson();
                foreach (TEITeiHeaderFileDescSourceDescPlace place in authorities.teiHeader.fileDesc.sourceDesc.listPlace)
                {
                    Feature newPoint = new Feature();
                    string[] aGeo = place.location.geo.Trim().Split(",");
                    float[] geo = new float[aGeo.Length];
                    geo[0] = float.Parse(aGeo[1]);
                    geo[1] = float.Parse(aGeo[0]);
                    newPoint.geometry.coordinates = geo;
                    newPoint.properties.name = place.placeName.Value;
                    newPoint.properties.link = place.idno.Value;
                    newPoint.properties.xmlid = place.xmlid;
                    localPoints.data.Add(newPoint);
                }
                globalService.Points=localPoints;
                Console.WriteLine("End ProcessAuthorities");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private async Task<clsEditionsData> GetFullData()
        {
            try
            {
                Console.WriteLine("Start GetFullData");
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
                    Console.WriteLine("End GetFullData");
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

                //building persons
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

        private async Task<clsFileList> GetEditionFiles()
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

        private async Task ProcessEditionFiles(clsFileList result)
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
