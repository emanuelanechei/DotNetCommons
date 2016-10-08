﻿using System;
using System.Threading;

namespace CommonNetTools
{
    public static class CtrlBreak
    {
        private static readonly ManualResetEvent Event = new ManualResetEvent(false);
        private static Action _hook;

        private static void CancelKeypress(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            Event.Set();
            _hook?.Invoke();
        }

        public static void Hook(Action action)
        {
            _hook = action;
            Console.CancelKeyPress += CancelKeypress;
        }

        public static void Release()
        {
            Console.CancelKeyPress -= CancelKeypress;
            _hook = null;
        }

        public static void WaitFor()
        {
            Event.Reset();
            Event.WaitOne();
        }
    }
}