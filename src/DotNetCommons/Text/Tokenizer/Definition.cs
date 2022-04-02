﻿using System;

// ReSharper disable UnusedMember.Global

namespace DotNetCommons.Text.Tokenizer;

public enum TokenMode
{
    None,
    Any,
    Letter,
    Digit,
    LetterOrDigit,
    Whitespace,
    EndOfLine
}

/// <summary>
/// Abstract base class for all definitions. The ID is a value - usually an enum - that defines
/// the category or type, decided entirely by the caller and how the callers wants to group or
/// classify tokens.
/// </summary>
/// <typeparam name="T">Type of the ID value.</typeparam>
public abstract class Definition<T>
{
    public bool Discard { get; }
    public T ID { get; }

    protected Definition(T id, bool discard)
    {
        Discard = discard;
        ID = id;
    }
}

/// <summary>
/// Capture a sequence of characters, e.g. digits, letters or digits, end of lines, whitespace etc.
/// </summary>
public class Characters<T> : Definition<T>
{
    public TokenMode Mode { get; }

    public Characters(TokenMode mode, T id, bool discard) : base(id, discard)
    {
        Mode = mode;
    }

    public bool IsMode(char c)
    {
        return Mode switch
        {
            TokenMode.Any => true,
            TokenMode.Letter => char.IsLetter(c),
            TokenMode.Digit => char.IsDigit(c),
            TokenMode.LetterOrDigit => char.IsLetterOrDigit(c),
            TokenMode.Whitespace => char.IsWhiteSpace(c),
            TokenMode.EndOfLine => c == 13 || c == 10,
            _ => false
        };
    }
}

/// <summary>
/// Capture a section. A section takes a starting text, ending text, and whether it takes
/// tokens or just strings. Useful for defining custom strings like "hello world!" and treating
/// the entire text as a single token defined by the quotes.
/// </summary>
public class Section<T> : Strings<T>
{
    public string EndText { get; }
    public bool SectionTakesTokens { get; }

    public Section(string text, string endText, bool takesTokens, T id, bool discard)
        : base(text, id, discard)
    {
        if (string.IsNullOrEmpty(endText))
            throw new ArgumentException("End text must not be empty.", nameof(endText));

        EndText = endText;
        SectionTakesTokens = takesTokens;
    }
}

/// <summary>
/// Match specific strings, like "=" or "and".
/// </summary>
public class Strings<T> : Definition<T>
{
    public string Text { get; }

    public Strings(string text, T id, bool discard) : base(id, discard)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Text must not be empty.", nameof(text));

        Text = text;
    }
}

/// <summary>
/// Define an escape character. Not actually used as a token, but for internal state-keeping.
/// </summary>
public class Escape<T> : Definition<T>
{
    public char EscapeChar { get; }

    public Escape(char escapeChar) : base(default!, false)
    {
        EscapeChar = escapeChar;
    }
}
