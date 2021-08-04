using AltV.Net;
using AltV.Net.ColoredConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace altV_FakeMasterlist.Utilities
{
    public enum ConsoleLogType
    {
        SUCCESS,
        ERROR
    }

    public class Logger
    {
        public void Console(ConsoleLogType type, string Category, string Log)
        {
            string message = "[";
            message += type == ConsoleLogType.SUCCESS ? "~g~" : "~r~";
            message += type == ConsoleLogType.SUCCESS ? "+" : "x";
            message += "~w~";
            message += "] ";

            message += $"{Category} >> {Log}";

            ColoredMessage cMessage = new ColoredMessage();
            cMessage += message;

            Alt.LogColored(cMessage);
        }
    }
}
