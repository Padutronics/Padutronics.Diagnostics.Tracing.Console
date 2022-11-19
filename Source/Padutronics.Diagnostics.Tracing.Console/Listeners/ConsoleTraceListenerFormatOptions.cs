using System;
using System.Collections.Generic;

namespace Padutronics.Diagnostics.Tracing.Console.Listeners;

public sealed class ConsoleTraceListenerFormatOptions
{
    public ConsoleColor LineNumberColor { get; set; } = ConsoleColor.Magenta;

    public ConsoleColor MemberNameColor { get; set; } = ConsoleColor.Green;

    public ConsoleColor NamespaceColor { get; set; } = ConsoleColor.DarkGray;

    public IDictionary<TraceSeverity, ConsoleColor> SeverityToColorMappings { get; } = new Dictionary<TraceSeverity, ConsoleColor>
    {
        [TraceSeverity.Error] = ConsoleColor.Red,
        [TraceSeverity.Warning] = ConsoleColor.Yellow
    };

    public ConsoleColor TextColor { get; set; } = ConsoleColor.Gray;

    public ConsoleColor TypeNameColor { get; set; } = ConsoleColor.Cyan;
}