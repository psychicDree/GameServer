﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace GameServer.Servers
{
    internal class Message
    {
        private byte[] data = new byte[];
        private int startIndex = 0;

        public byte[] Data
        {
            get { return data; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }

        public int RemainSize
        {
            get
            {
                return data.Length - startIndex;
            }
        }

        public void ReadMessage(int newDataAmount, Action<RequestCode, ActionCode, string> OnProcessDataCallback)
        {
            startIndex += newDataAmount;
            if(startIndex<=4)return;
            int count = BitConverter.ToInt32(data, 0);
            if (startIndex - 4 >= count)
            {
                RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                string str = Encoding.UTF8.GetString(data, 12, count - 4);
                OnProcessDataCallback(requestCode, actionCode, str);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex = count + 4;
            }
            else
                return;
        }

        public static byte[] PackData(RequestCode requestCode, string data)
        {
            byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int newDataAmount = requestCodeBytes.Length + dataBytes.Length;
            byte[] newDataAmountBytes = BitConverter.GetBytes(newDataAmount);
            return newDataAmountBytes.Concat(requestCodeBytes).ToArray().Concat(dataBytes).ToArray();

        }
    }
}
