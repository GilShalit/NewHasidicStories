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
using Blazorise.Modules;
using Blazorise;
using System.Globalization;

namespace clientHasidicStories.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] HttpClient http { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] GlobalService globalService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        private Blazorise.IFluentColumn myColumnSize = ColumnSize.Is3;
        string mapPadding = "";

        protected override async Task OnInitializedAsync()
        {
            if (CultureInfo.CurrentCulture.Name == "he-IL") mapPadding = "padding-left:0";
            else mapPadding = "padding-right:0";

            if (!globalService.DataLoaded)
            {
                var task1 = GetStoryInfo().ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        InvokeAsync(async () => await ProcessStoryInfo(t.Result));
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

                var task4 = GetStoryTexts().ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        InvokeAsync(async () => await ProcessStoryTexts(t.Result));
                    }
                    else
                    {
                        HandleError(t.Exception);
                    }
                });
                await task4;

                globalService.DataLoaded = true;
                
                Console.WriteLine("===Data loaded===");
                globalService.updateStoriesAndPoints();
            }
        }

        private async Task ProcessStoryTexts(clsEditionsStoryTexts StoryTexts)
        {
            try
            {
                Console.WriteLine("Start ProcessStories");
                clsDisplayStoryTexts dst = new clsDisplayStoryTexts();
                for (int i = 0; i < StoryTexts.editions.Count(); i++)
                {
                    editionStoryTexts e = StoryTexts.editions[i];
                    dst.editions.Add(new clsEditionStories() { name = e.name, iEdition = i });
                    foreach (story s in e.stories)
                    {
                        dst.editions[dst.editions.Count - 1].stories.Add(new clsStoryText() { id = s.id, text = s.text.Replace("\n", "") });
                    }
                }
                globalService.DisplayStoryTexts = dst;
                Console.WriteLine("End ProcessStories");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private async Task<clsEditionsStoryTexts> GetStoryTexts()
        {
            try
            {
                Console.WriteLine("Start GetStoryTexts");
                //await Task.Delay(2000); // Delay for 2 seconds
                //Console.WriteLine("End delay in GetFullData");
                clsEditionsStoryTexts editionsStoryTexts;
                HttpResponseMessage response = await http.GetAsync("api/get-storytexts");
                if (response.IsSuccessStatusCode)
                {
                    clsDataJson dataJson = await response.Content.ReadFromJsonAsync<clsDataJson>();
                    XmlSerializer serializer = new XmlSerializer(typeof(clsEditionsStoryTexts));
                    using (StringReader reader = new StringReader(dataJson.all))
                    {
                        editionsStoryTexts = (clsEditionsStoryTexts)serializer.Deserialize(reader);
                    }
                    Console.WriteLine("End GetStoryTexts");
                    return editionsStoryTexts;
                }
                else
                {
                    HandleError(new Exception($"Failed to get story texts. Status code: {response.StatusCode}"));
                    return null;
                }

            }
            catch (Exception ex)
            {
                HandleError(ex);
                return null;
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

                //Both options below do not work so probably need to develop an api call so that authorities can be outside the application
                //httpLocal.BaseAddress = new Uri("http://localhost:8081/exist/apps/HasidicStoriesServer/data/");
                //httpLocal.BaseAddress = new Uri("http://localhost:8081/exist/apps/");

                httpLocal.BaseAddress = new Uri(Navigation.BaseUri);
                string url = $"Authorities.xml?nocache={DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
                HttpResponseMessage response = await httpLocal.GetAsync(url);
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
                clsPersons localPersons = globalService.Persons;
                if (localPersons != null)
                {
                    //clsPerson prs = localPersons.Where(p => p.xmlref == "01.001").FirstOrDefault();
                    foreach (clsPerson person in localPersons)
                    {
                        //Console.WriteLine(person.xmlref);
                        TEITeiHeaderFileDescSourceDescListPersonPerson teiPerson = authorities.teiHeader.fileDesc.sourceDesc.listPerson[1].person.Where(p => p.xmlid == person.xmlref).FirstOrDefault();
                        person.name = teiPerson.name.Where(n=>n.lang=="en").First().Value;
                        person.link = teiPerson.idno[0].Value;
                    }
                    localPersons.hasNames = true;
                    globalService.Persons = localPersons;
                }

                //find Ids of places included in the stories
                clsGeoJson localPoints = new clsGeoJson();
                List<string> includedPlacesIds = new List<string>();
                if (globalService.Places != null)
                {
                    foreach (clsPlace place in globalService.Places)
                        if (!includedPlacesIds.Contains(place.xmlref)) includedPlacesIds.Add(place.xmlref);
                }

                foreach (TEITeiHeaderFileDescSourceDescListPlace place in authorities.teiHeader.fileDesc.sourceDesc.listPlace
                    .Where(p => includedPlacesIds.Contains(p.xmlid))
                    )
                {
                    //Console.WriteLine(place.placeName.Value);
                    Feature newPoint = new Feature();
                    if (place.location != null)
                    {
                        string[] aGeo = place.location.geo.Trim().Split(",");
                        float[] geo = new float[aGeo.Length];
                        geo[0] = float.Parse(aGeo[1], CultureInfo.InvariantCulture);
                        geo[1] = float.Parse(aGeo[0], CultureInfo.InvariantCulture);
                        newPoint.geometry.coordinates = geo;
                    }
                    newPoint.properties.name = place.placeName;
                    if (place.idno != null) newPoint.properties.link = place.idno[0].Value;//ToDo: support more then one link
                    newPoint.properties.xmlid = place.xmlid;
                    localPoints.data.Add(newPoint);
                }
                globalService.Points = localPoints;
                Console.WriteLine("End ProcessAuthorities");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private async Task<clsStoryInfoData> GetStoryInfo()
        {
            try
            {
                Console.WriteLine("Start GetStoryInfo");
                //await Task.Delay(2000); // Delay for 2 seconds
                //Console.WriteLine("End delay in GetFullData");
                clsStoryInfoData editionsInfo;
                HttpResponseMessage response = await http.GetAsync("api/get-storyinfo");
                if (response.IsSuccessStatusCode)
                {
                    clsDataJson dataJson = await response.Content.ReadFromJsonAsync<clsDataJson>();
                    XmlSerializer serializer = new XmlSerializer(typeof(clsStoryInfoData));
                    using (StringReader reader = new StringReader(dataJson.all))
                    {
                        editionsInfo = (clsStoryInfoData)serializer.Deserialize(reader);
                    }
                    Console.WriteLine("End GetStoryInfo");
                    return editionsInfo;
                }
                else
                {
                    HandleError(new Exception($"Failed to get story info. Status code: {response.StatusCode}"));
                    return null;
                }

            }
            catch (Exception ex)
            {
                HandleError(ex);
                return null;
            }
        }

        private async Task ProcessStoryInfo(clsStoryInfoData storyInfo)
        {
            try
            {
                Console.WriteLine("Start ProcessStoryInfo");
                string story = "";
                //globalService.StoryInfoData = storyInfo;

                //building persons and people
                clsPersons localPersons = new clsPersons();
                clsPlaces localPlaces = new clsPlaces();
                for (int e = 0; e < storyInfo.editions.Length; e++)
                {
                    for (int s = 0; s < storyInfo.editions[e].stories.Length; s++)
                    {
                        story = storyInfo.editions[e].stories[s].Id;
                        for (int j = 0; j < storyInfo.editions[e].stories[s].persons.Length; j++)
                        {
                            localPersons.newPerson(storyInfo.editions[e].stories[s].persons[j].Substring(1), story);
                            //localPersons.newPerson(storyInfo.editions[e].stories[s].persons[j], story);
                        }
                        for (int j = 0; j < storyInfo.editions[e].stories[s].places.Length; j++)
                        {
                            localPlaces.newPlace(storyInfo.editions[e].stories[s].places[j].Substring(1), story);
                            //localPlaces.newPlace(storyInfo.editions[e].stories[s].places[j], story);
                        }
                    }
                }
                globalService.Persons = localPersons;
                globalService.Places = localPlaces;

                //building themes in separate loops so UI updates as we go
                string[] themeNames = [];
                clsThemes localThemes = new clsThemes();
                for (int e = 0; e < storyInfo.editions.Length; e++)
                {
                    for (int s = 0; s < storyInfo.editions[e].stories.Length; s++)
                    {
                        story = storyInfo.editions[e].stories[s].Id;
                        //Console.WriteLine(story);
                        themeNames = storyInfo.editions[e].stories[s].ana.Split(";");
                        for (int j = 0; j < themeNames.Length; j++)
                        {
                            localThemes.newTheme(themeNames[j], story);
                        }
                    }
                }
                globalService.Themes = localThemes;
                Console.WriteLine("End ProcessStoryInfo");
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
                    editions.Add(new clsEditionFile(lItem.pbrestricted.pbajax.url, lItem.header.div.div[0].a.span.text));
                }
                globalService.EditionFiles = editions;
                Console.WriteLine("End ProcessEditions");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void HandleError(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }
}
