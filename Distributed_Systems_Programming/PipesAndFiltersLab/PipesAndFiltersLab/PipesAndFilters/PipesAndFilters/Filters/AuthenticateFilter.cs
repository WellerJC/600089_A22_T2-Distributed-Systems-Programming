using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipesAndFilters.Messages;

namespace PipesAndFilters.Filters
{
    public class AuthenticateFilter : IFilter
    {
        public IMessage Run(IMessage message)
        {
            if (message.Headers.ContainsKey("User"))
            {
                int userId;
                if (int.TryParse(message.Headers["User"], out userId))
                {
                    ServerEnvironment.SetCurrentUser(userId);
                }
            }

            return message;
        }
    }
}
