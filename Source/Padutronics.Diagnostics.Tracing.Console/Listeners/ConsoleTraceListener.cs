using Padutronics.Diagnostics.Tracing.Filters;
using Padutronics.Diagnostics.Tracing.Listeners;
using System.Collections.Generic;

namespace Padutronics.Diagnostics.Tracing.Console.Listeners;

using System;

public sealed class ConsoleTraceListener : TraceListener
{
    private const ConsoleColor LineNumberColor = ConsoleColor.Magenta;
    private const ConsoleColor MemberNameColor = ConsoleColor.Green;
    private const ConsoleColor NamespaceColor = ConsoleColor.DarkGray;
    private const ConsoleColor TextColor = ConsoleColor.Gray;
    private const ConsoleColor TypeNameColor = ConsoleColor.Cyan;

    private readonly IReadOnlyDictionary<TraceSeverity, ConsoleColor> severityToColorMappings = new Dictionary<TraceSeverity, ConsoleColor>
    {
        [TraceSeverity.Error] = ConsoleColor.Red,
        [TraceSeverity.Warning] = ConsoleColor.Yellow
    };

    public ConsoleTraceListener()
    {
    }

    public ConsoleTraceListener(ITraceFilter filter) :
        base(filter)
    {
    }

    public override void ProcessTrace(TraceEntry entry)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;

        Console.Write(new string(' ', entry.Format.IndentLevel));

        Console.ForegroundColor = NamespaceColor;
        Console.Write(entry.Caller.Namespace);

        Console.ForegroundColor = TextColor;
        Console.Write('.');

        Console.ForegroundColor = TypeNameColor;
        Console.Write(entry.Caller.TypeName);

        Console.ForegroundColor = TextColor;
        Console.Write('.');

        Console.ForegroundColor = MemberNameColor;
        Console.Write(entry.Caller.MemberName);

        Console.ForegroundColor = TextColor;
        Console.Write(':');

        Console.ForegroundColor = LineNumberColor;
        Console.Write(entry.Caller.LineNumber);

        if (!severityToColorMappings.TryGetValue(entry.Trace.Severity, out ConsoleColor color))
        {
            color = TextColor;
        }

        Console.ForegroundColor = color;
        Console.Write($" {entry.Trace.Message}");

        Console.WriteLine();

        Console.ForegroundColor = foregroundColor;
    }
}