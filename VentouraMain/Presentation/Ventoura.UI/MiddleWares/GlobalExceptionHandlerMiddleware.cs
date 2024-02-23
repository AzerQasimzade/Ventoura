namespace Ventoura.UI.MiddleWares
{
    public class GlobalExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                string errorpage = Path.Combine("/error", $"ErrorPage?error={e}");
                string errorMessage = e.Message;
                errorpage = Path.Combine("/error", $"ErrorPage?error={Uri.EscapeDataString(errorMessage)}");
                context.Response.Redirect(errorpage);

            }
        }
    }
}
