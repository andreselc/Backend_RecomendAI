using System.Net;

namespace IARecommendAPI.Modelos
{
    public class RespuestasAPI
    {
        public RespuestasAPI()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
