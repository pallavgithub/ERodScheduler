using ERodScheduler.FishBowlServerObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ERodScheduler
{
    partial class ErodDataService : ServiceBase
    {
        private static Timer Timer;
        private static IConfiguration Configuration;
        
        public ErodDataService()
        {
            InitializeComponent();          
        }

        private static TimeSpan NextStartInterval
        {
            get
            {
                var now = DateTime.Now;
                var startTime = DateTime.Today.Add(TimeSpan.Parse(Configuration[Constants.StartTime],CultureInfo.InvariantCulture));
                return startTime >= now ? startTime.Subtract(now) : now.AddMinutes(int.Parse(Configuration[Constants.ContinueAfterMinutes])).Subtract(now);
            }
        }

        public void ServiceStart(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            ConfigurationInitialized();
            Log("Service has been started..!"+Environment.NewLine);
            Log("Service start time" + Configuration[Constants.StartTime] + Environment.NewLine);
            try
            {
                var nextStartInterval = NextStartInterval;
                bool scheduardisable = bool.Parse(Configuration[Constants.SchedularDisable]);

                if (scheduardisable)
                    // DataCallbackhandler(null);
                    Testhndler(null);
                else
                    // Timer = new Timer(DataCallbackhandler, null, nextStartInterval, TimeSpan.FromMilliseconds(-1));
                    Timer = new Timer(Testhndler, null, nextStartInterval, TimeSpan.FromMilliseconds(-1));               
            }
            catch (Exception ex)
            {
                Log(string.Format("Error something wen wrong in Service Logging {0}",ex.Message));
            }
                
        }
        
        private void Log(string logMessage)
        {
            File.AppendAllTextAsync(Path.Combine(Configuration[Constants.logFileLocation], Configuration[Constants.logFileName] + DateTime.Now.ToString("dd-MM-yy")+".txt"), DateTime.UtcNow.ToString() + " : " + logMessage + Environment.NewLine);
        }

        public void Testhndler(object state)
        {
            Log("Service Logging..!");
           
            var nextStartInterval = NextStartInterval;
            if (Timer != null)
                Timer.Change(nextStartInterval, TimeSpan.FromMilliseconds(-1));
           
            Log("Service Logging Done");
            Log(string.Format("Service will run after {0} at {1}"+ Environment.NewLine + Environment.NewLine, NextStartInterval, DateTime.Now.Add(NextStartInterval)));
        }

        public void DataCallbackhandler(object state)
        {
            try
            {
                FishBowlServer fishBowlServer = new FishBowlServer();
                Console.WriteLine("1");
                string loginResponse = fishBowlServer.Connect("222", "C Sharp Sample", "ERod", "rodapp", "algoeoe");
                Console.WriteLine("2");
                string key = fishBowlServer.GetTicket(loginResponse);
                Console.WriteLine("3");
                string response = fishBowlServer.ExecuteQuery(key, "<GetSOListRq></GetSOListRq>");
                Console.WriteLine("4");

                using var reader = XmlReader.Create(new MemoryStream(Encoding.ASCII.GetBytes(response)));
                var result = Savefile(GetJsonString(reader));

                Log("Service Logging Done");
                Log(string.Format("Service will run after {0} at {1}", NextStartInterval, DateTime.Now.Add(NextStartInterval)));

            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStop()
        {
            try
            {
                if (Timer != null)
                {
                    Timer.Change(Timeout.Infinite, Timeout.Infinite);
                }

                Log("Service stopped");
            }
            catch (Exception ex)
            {
                Log("An Error occured while stopping of the service."+Environment.NewLine + ex.Message);
            }
        }

        public static string GetJsonString(XmlReader reader)
        {
            try
            {
                reader.MoveToContent();
                reader.ReadToDescendant("Ticket");
                reader.SkipAsync();
                reader.MoveToContent();
                reader.ReadToFollowing("GetSOListRs");
                XmlSerializer Serializer = new XmlSerializer(typeof(SalesOrdersList));
                var OrderItems = (SalesOrdersList)Serializer.Deserialize(reader);
                return JsonSerializer.Serialize(OrderItems);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static bool Savefile(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data))
                {
                    File.WriteAllTextAsync(Path.Combine(Configuration[Constants.DataFilePath], Configuration[Constants.DataFileName]+".txt"), data);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void ConfigurationInitialized()
        {
            Configuration = new ConfigurationBuilder()
                                       .AddJsonFile("ErodServiceConfig.json", optional: true, reloadOnChange: true)
                                       .Build();
        }

    }
}
