using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class Pipeline
    {
        public Stack<IMiddleware> _pipelineElemets = new Stack<IMiddleware>();
        public HelpMiddleware helpMiddleware = new HelpMiddleware();

        public Pipeline()
        {
            _pipelineElemets.Push(helpMiddleware);
            _pipelineElemets.Push(new StatsMiddleware());
            _pipelineElemets.Push(new DisconnectBotMiddleware());
        }

        public void Add(IMiddleware middleware)
        {
            _pipelineElemets.Push(middleware);
        }

        public void Run(Dictionary<string, object> parameters)
        {
            bool jobCompleted = false;

            while(!jobCompleted)
            {
                jobCompleted = true;
                int i = 0;
                foreach (var item in _pipelineElemets)
                {
                    if (i == _pipelineElemets.Count - 1)
                        helpMiddleware.HelpBuilder(this);
                    item.Process(parameters);
                    if (item.IsComplete == true)
                        break;
                    i++;
                }
            }
        }
    }
}
