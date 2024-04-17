using Microsoft.AspNetCore.Components;
using static clientHasidicStories.Components.Editions;
using System.Net.Http.Json;

namespace clientHasidicStories.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] HttpClient http { get; set; }
        bool isLoading = true;
        protected override async Task OnInitializedAsync()
        {
            var task1 = GetDocThemes();
            await task1.ContinueWith(t =>
            {
                if (t.IsFaulted)
                    HandleError(t.Exception);
                else
                    InvokeAsync(async () => await ProcessResult1(t.Result));
            });

            var task2 = CallApi2();
            await task2.ContinueWith(t =>
            {
                if (t.IsFaulted)
                    HandleError(t.Exception);
                else
                    InvokeAsync(async () => await ProcessResult2(t.Result));
            });

            await Task.WhenAll(task1, task2);
            isLoading = false;
        }

        private async Task<clsThemesJson> GetDocThemes()
        {
            try
            {
                HttpResponseMessage response = await http.GetAsync($"api/get-ana");
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

        private async Task ProcessResult1(clsThemesJson result)
        {
            try
            {
                // Process the result of your first API call
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private async Task<ApiResult2> CallApi2()
        {
            try
            {
                // Call your second API and return the result
                return new ApiResult2();
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return null;
            }
        }

        private async Task ProcessResult2(ApiResult2 result)
        {
            try
            {
                // Process the result of your second API call
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

        class ApiResult2 { }

    }
}
