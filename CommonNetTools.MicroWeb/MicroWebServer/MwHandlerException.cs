﻿using System;
using System.Net;

namespace CommonNetTools.Scripting.MicroWebServer
{
    public class MwHandlerException : Exception
    {
        public MwResponse Response { get; }

        public MwHandlerException(MwResponse error)
        {
            Response = error;
        }

        public MwHandlerException(HttpStatusCode statusCode, string message = null)
        {
            Response = MwResponse.Error(statusCode, message);
        }
    }
}
