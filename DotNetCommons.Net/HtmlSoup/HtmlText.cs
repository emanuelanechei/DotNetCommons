﻿using System;

namespace DotNetCommons.Net.HtmlSoup
{
    public class HtmlText : HtmlElement
    {
        public string Text { get; set; }

        public HtmlText()
        {
        }

        public HtmlText(string text)
        {
            Text = (text ?? "").Trim();
        }
    }
}