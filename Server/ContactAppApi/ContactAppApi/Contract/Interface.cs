using ContactAppApi.Models;

namespace ContactAppApi.Contract
{
    public interface IResponse<out TBody> : IResponse
    {
        TBody Body { get; }
    }
}
