using Microsoft.AspNetCore.Http.Headers;

namespace ContactAppApi.Contract
{
    
        public interface IResponse
        {
            Dictionary<string,string> Header { get; set; }

            public bool HasBody();
        
        
    }
}
