using Microsoft.AspNetCore.Components;

namespace clientHasidicStories.Pages
{
    public partial class Index : ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            var task1 = CallApi1();
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
        }

        private async Task<clsThemesJson> CallApi1()
        {
            try
            {
                // Call your first API and return the result
                return new clsThemesJson();
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
