using System.Collections.Generic;
using PipesAndFilters.Filters;
using PipesAndFilters.Messages;


namespace PipesAndFilters.Pipes
{
    public interface IPipe
    {
        void RegisterFilter(IFilter filter);
        IMessage ProcessMessage(IMessage message);
    }
}
