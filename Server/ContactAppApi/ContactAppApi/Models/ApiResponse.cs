using Microsoft.AspNetCore.Http.Headers;

namespace ContactAppApi.Models
{
    public class ApiResponse<TBody>  
    {
        public TBody? Body { get; set; }
        public string Status { get;set; }
        public int StatusCode { get; set; }
        public bool HasBody()
        {
            return Body != null;
        }
        public ApiResponse(TBody body,string status, int satatuscode)
        {
            this.Body = body;
            this.Status = status;
            this.StatusCode = satatuscode;
            
        }

    }
    public class ApiResponseBuilder<TBody>
    {
        private TBody body;
        private string status="";
        private int statusCode=0;



        public ApiResponseBuilder<TBody> AddBody(TBody body)
        {
            this.body = body;
            return this;
        }
        public ApiResponseBuilder<TBody> AddStatusCode(int statusCode)
        {
            this.statusCode = statusCode;
            return this;
        }
        public ApiResponseBuilder<TBody?> AddStatus(string status)
        {
            this.status = status;
            return this;
        }
        public ApiResponse<TBody> Build() => new ApiResponse<TBody>(body, status, statusCode);


    }
}
