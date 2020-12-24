using System.Threading;
using System.ServiceProcess;
using System;

namespace ERodScheduler
{
    class Program
    {
		private static readonly ManualResetEvent ResetEvent = new ManualResetEvent(false);

		static void Main(string[] args)
		{
            if (!Environment.UserInteractive)
            {
                WaitEnterButtonClickAsync();
                var service = new ErodDataService();
                service.ServiceStart(args);
                ResetEvent.WaitOne();
                service.Stop();
            }
            else
            {
                Thread.CurrentThread.Name = "ServiceThread";
                var servicesToRun = new ServiceBase[] { new ErodDataService() };
                ServiceBase.Run(servicesToRun);
            }
        }

		private static void WaitEnterButtonClickAsync()
		{
			ThreadPool.QueueUserWorkItem(parameters =>
			{
				Console.Read();
				ResetEvent.Set();
			});
		}

	}
}
