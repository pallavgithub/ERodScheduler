﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ERodScheduler.FishBowlServerObjects
{
    class ConnectionObject {
        private TcpClient tc;
        private NetworkStream tcS;

        private EndianBinaryWriter bw;
        private EndianBinaryReader br;

        private Socket sock;
        private IPEndPoint ipep;
        private IPAddress ipAddr;

        /**
         * Create the server connection
         */
        public ConnectionObject() {
            tc = new TcpClient("166.78.20.116", 28192);
            tcS = tc.GetStream();

            bw = new EndianBinaryWriter(new BigEndianBitConverter(), tcS);
            br = new EndianBinaryReader(new BigEndianBitConverter(), tcS);
        }

        /**
         * Send the XML request string
         */
        public String sendCommand(string command) {
            try
            {
                bw = new EndianBinaryWriter(new BigEndianBitConverter(), tcS);
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] bytes = encoding.GetBytes(command);
                bw.Write(bytes.Length);
                bw.Write(bytes);
                bw.Flush();
                Thread.Sleep(10000);
                br = new EndianBinaryReader(new BigEndianBitConverter(), tcS);
                int i = br.ReadInt32();
                byte[] bytess = new byte[i];
                br.Read(bytess, 0, i);
                String response = encoding.GetString(bytess, 0, i);

                return response;
            }
            catch (Exception)
            {
                return "";
            }
        }

        
    }
}
