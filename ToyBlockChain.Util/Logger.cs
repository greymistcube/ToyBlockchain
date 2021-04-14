﻿using System;

namespace ToyBlockChain.Util
{
    public static class Logger
    {
        private static int _logLevel = 0;

        public static int LogLevel
        {
            get
            {
                return _logLevel;
            }
            set
            {
                _logLevel = value;
            }
        }

        /// <summary>
        /// Simple helper method to log output.
        /// </summary>
        public static void Log(
            string text,
            int textLevel = 1,
            System.ConsoleColor color = ConsoleColor.White)
        {
            if(textLevel <= _logLevel)
            {
                    Console.ForegroundColor = color;
                    Console.WriteLine(text);
                    Console.ResetColor();
            }
        }
    }
}
