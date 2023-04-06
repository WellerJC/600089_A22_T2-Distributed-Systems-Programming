using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipesAndFilters.Messages;

namespace PipesAndFilters.Filters
{
    public class TimeStampFilter : IFilter
    {
        public IMessage Run(IMessage message)
        {
            message.Headers.Add("Timestamp", DateTime.Now.ToString());
            return message;
        }
    }
}
