namespace Api.Talabat.V1.Error
{
    public class ApiVaildationResponse:ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiVaildationResponse():base(400)
        {
            Errors = new List<string>();
        }
    }
}
