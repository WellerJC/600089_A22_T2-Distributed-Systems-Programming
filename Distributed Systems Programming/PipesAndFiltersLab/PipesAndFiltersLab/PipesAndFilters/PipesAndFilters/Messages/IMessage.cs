using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesAndFilters.Messages
{
    public interface IMessage
    {
        Dictionary<string,string> Headers { get; set; }
        string Body { get; set; }

    }
}
