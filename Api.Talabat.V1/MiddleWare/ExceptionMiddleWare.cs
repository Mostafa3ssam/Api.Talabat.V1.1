using Api.Talabat.V1.Error;
using System.Net;
using System.Text.Json;

namespace Api.Talabat.V1.MiddleWare
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleWare(RequestDelegate Next ,ILogger<ExceptionMiddleWare> logger , IHostEnvironment environment)
        {
            _next = Next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync( HttpContext context)
        {
            try
            {


                await _next.Invoke(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //    if (_environment.IsDevelopment())
                //    {
                //        var Response = new ApiExcptionResponse((int) HttpStatusCode.InternalServerError , ex.Message ,ex.StackTrace.ToString());
                //    }
                //    else
                //    {
                //        var Response = new ApiExcptionResponse((int)HttpStatusCode.InternalServerError);
                //    }
                //var JsonResponse = JsonSerializer.Serialize(Response)

                var Response = _environment.IsDevelopment() ? new ApiExcptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new ApiExcptionResponse((int)HttpStatusCode.InternalServerError);

                var JsonResponse = JsonSerializer.Serialize(Response);
                context.Response.WriteAsync(JsonResponse);  


            }


        }
    }
}
