namespace ContactApp.Application.UseCases.Commons
{

    public class Response<TBody> : IResponse<TBody>, IResponse
    {
        public ResponseHeader Header { get; set; } = new ResponseHeader();

        public IEnumerable<BaseError> Errors { get; set; }

        public TBody Body { get; set; }

        public bool HasBody()
        {
            return Body != null;
        }
    }
}
