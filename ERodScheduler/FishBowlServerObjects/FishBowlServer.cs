using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;

namespace ERodScheduler.FishBowlServerObjects
{
    public class FishBowlServer
    {
        static ConnectionObject connectionObject;
        public string loginResponse;

        public FishBowlServer()
        {
        }

        public string Connect(string iAID, string iAName, string iADescription, string userName, string userPassword)
        {
            loginResponse = ConnectFishBowlServer(iAID, iAName, iADescription, userName, userPassword);
            return loginResponse;
        }

        public string ConnectFishBowlServer(string iAID, string iAName, string iADescription, string userName, string userPassword)
        {
            String loginCommand = createLoginXml(iAID, iAName,iADescription,userName,userPassword);
            connectionObject = new ConnectionObject();
            string response = connectionObject.sendCommand(loginCommand);
            return response;
            //key = pullKey(connectionObject.sendCommand(loginCommand));
        }

        private static String createLoginXml(string iAID, string iAName, string iADescription, string userName, string userPassword)
        {
            System.Text.StringBuilder buffer = new System.Text.StringBuilder("<FbiXml><Ticket/><FbiMsgsRq><LoginRq><IAID>");
            buffer.Append(iAID);
            buffer.Append("</IAID><IAName>");
            buffer.Append(iAName);
            buffer.Append("</IAName><IADescription>");
            buffer.Append(iADescription);
            buffer.Append("</IADescription><UserName>");
            buffer.Append(userName);
            buffer.Append("</UserName><UserPassword>");

            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] encoded = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(userPassword));
            string encrypted = Convert.ToBase64String(encoded, 0, 16);
            buffer.Append(encrypted);
            buffer.Append("</UserPassword></LoginRq></FbiMsgsRq></FbiXml>");

            return buffer.ToString();
        }

        public string GetTicket(string loginResponse)
        {
            return PullKey(loginResponse);
        }

        private static String PullKey(String connection)
        {
            String key = "";
            using (XmlReader reader = XmlReader.Create(new StringReader(connection)))
            {
                while (reader.Read())
                {
                    //if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("Key"))
                    if (reader.Name.Equals("Key") && reader.Read())
                    {
                        return reader.Value.ToString();
                    }
                }
            }
            return key;
        }

        public string ExecuteQuery(string key, string Query)
        {
            string query = "<FbiXml><Ticket><Key>" + key + "</Key></Ticket><FbiMsgsRq>"+ Query  + "</FbiMsgsRq></FbiXml>";
            String response = connectionObject.sendCommand(query);
            return response;
        }
    }
}
