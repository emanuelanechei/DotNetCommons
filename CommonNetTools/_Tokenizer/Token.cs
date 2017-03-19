﻿using System;

namespace CommonNetTools
{
    public class Token
    {
        public TokenDefinition Definition { get; }
        public TokenList Section { get; } = new TokenList();
        public string Text { get; set; }
        public int Value => Definition.Value;

        public Token(TokenDefinition definition, string text = null)
        {
            Definition = definition;
            Text = text;
        }
    }
}
