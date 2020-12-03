using ERodScheduler.FishBowlServerObjects;
using System;

namespace ERodScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello ERod!");

            try
            {
                FishBowlServer fishBowlServer = new FishBowlServer();
                string loginResponse = fishBowlServer.Connect("222", "C Sharp Sample", "ERod", "rodapp", "algoeoe");
                string key = fishBowlServer.GetTicket(loginResponse);
                string response = fishBowlServer.ExecuteQuery(key, "<GetSOListRq></GetSOListRq>");
            }
            catch (Exception ex)
            {

            }
            Console.ReadLine();
        }
    }
}
