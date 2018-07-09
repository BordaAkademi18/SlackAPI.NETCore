using System;
using System.Collections.Generic;
using System.Text;

namespace SlackAPI.RTM_API.Middleware_Architecture
{
    public class TimerPipeline
    {
        public Stack<ITimerMiddleware> _pipelineElemets = new Stack<ITimerMiddleware>();

        public TimerPipeline()
        {
        }

        public void Add(ITimerMiddleware middleware)
        {
            _pipelineElemets.Push(middleware);
        }

        public void Run(SlackClient slackClient)
        {            
            foreach (var item in _pipelineElemets)
                 item.Process(slackClient);            
        }
    }
}
