namespace Api.Talabat.V1.Error
{
    public class ApiExcptionResponse:ApiResponse
    {
        public string Details { get; set; }
        public ApiExcptionResponse(int SatusCode , string? Massage = null , string? details = null):base(SatusCode, Massage)
        {
            Details = details;
        }
    }
}
