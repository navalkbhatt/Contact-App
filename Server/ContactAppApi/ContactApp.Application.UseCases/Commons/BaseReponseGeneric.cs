using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Application.UseCases.Commons
{
    public interface IResponse
    {
        ResponseHeader Header { get; set; }

        bool HasBody();
    }
    public interface IResponse<out TBody> : IResponse
    {
        TBody Body { get; }
    }
    

}
