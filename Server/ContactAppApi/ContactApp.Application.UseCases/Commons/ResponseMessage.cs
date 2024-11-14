namespace ContactApp.Application.UseCases.Commons
{
    public class ResponseMessage
    {
        private int statusCode;

        public string? Code { get; private set; }

        public string? ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        public ResponseMessageType Type { get; set; }

        public static ResponseMessage CreateInfo(uint prefix, uint code, int statusCode, string shortDescription, string longDescription)
        {
            return Create(prefix, code, statusCode, ResponseMessageType.Info, shortDescription, longDescription);
        }

        public static ResponseMessage CreateWarning(uint prefix, uint code, int statusCode, string shortDescription, string longDescription)
        {
            return Create(prefix, code, statusCode, ResponseMessageType.Warning, shortDescription, longDescription);
        }

        public static ResponseMessage CreateError(uint prefix, uint code, int statusCode, string shortDescription, string longDescription)
        {
            return Create(prefix, code, statusCode, ResponseMessageType.Error, shortDescription, longDescription);
        }

        public static ResponseMessage Create(uint prefix, uint code, int statusCode, ResponseMessageType type, string shortDescription, string longDescription)
        {
            if (prefix > 999)
            {
                throw new ArgumentOutOfRangeException("prefix", "Prefix must be 3 digits or less");
            }

            if (code > 9999)
            {
                throw new ArgumentOutOfRangeException("code", "Code must be 4 digits or less");
            }

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.Type = type;
            responseMessage.SetCode(prefix.ToString("D3") + code.ToString("D4"), statusCode);
            responseMessage.ShortDescription = shortDescription;
            responseMessage.LongDescription = longDescription;
            return responseMessage;
        }

        public void SetCode(string code, int statusCode)
        {
            this.statusCode = statusCode;
            Code = $"{statusCode}.{code}";
        }

        public int GetHttpStatusCode()
        {
            return statusCode;
        }
    }
}
