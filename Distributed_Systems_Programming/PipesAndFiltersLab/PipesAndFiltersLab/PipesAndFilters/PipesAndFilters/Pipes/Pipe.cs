using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipesAndFilters.Filters;
using PipesAndFilters.Messages;

namespace PipesAndFilters.Pipes
{
    class Pipe : IPipe
    {
        private List<IFilter> Filters { get; }

        public Pipe()
        {
            Filters = new List<IFilter>();
        }

        public void RegisterFilter(IFilter filter)
        {
            Filters.Add(filter);
        }

        public IMessage ProcessMessage(IMessage message)
        {
            IMessage result = message;

            foreach (IFilter filter in Filters)
            {
                result = filter.Run(result);
            }

            return result;
        }
    }
}
