using Newtonsoft.Json;

namespace Web.Api.ViewModels
{
    public class ErrorResponseDetailsViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
