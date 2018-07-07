using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public interface IMiddleware
    {
        void Process(Dictionary<string, object> parameters);

        bool IsComplete { get; }

        string Description { get; }

        string Command { get; }
    }
}
