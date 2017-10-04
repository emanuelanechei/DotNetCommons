﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCommons.Logger
{
    public class LogChain : Stack<ILogMethod>
    {
        public string Name { get; }

        public LogChain(string name)
        {
            Name = name;
        }

        public LogChain(LogChain chain)
            : this(chain.Name)
        {
            foreach(var link in chain.Reverse())
                Push(link);
        }

        public void Process(List<LogEntry> entries, bool flush)
        {
            foreach (var link in this)
                entries = link.Handle(entries, flush);
        }
    }
}
