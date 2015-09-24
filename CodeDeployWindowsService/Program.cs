using System;
using System.Collections.Generic;
using System.Timers;
using Topshelf;

namespace CodeDeployWindowsService
{
    public class TopshelfService
    {
        readonly Timer _timer;
        public TopshelfService()
        {
            _timer = new Timer(5000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Execute();
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }

        public void Execute()
        {
            Console.WriteLine("CodeDeploy Windows Service Example");
        }
    }


    class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<TopshelfService>(s =>                        
                {
                    s.ConstructUsing(name => new TopshelfService());    
                    s.WhenStarted(tc => tc.Start());              
                    s.WhenStopped(tc => tc.Stop());               
                });
                x.RunAsLocalSystem();

                x.SetDescription("CodeDeploy Windows Service Example");
                x.SetDisplayName("CodeDeployWindowsService");
                x.SetServiceName("CodeDeployWindowsService");
            });         
        }

    }
}