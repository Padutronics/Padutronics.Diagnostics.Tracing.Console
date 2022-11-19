using Padutronics.Diagnostics.Tracing.Filters;
using Padutronics.Diagnostics.Tracing.Listeners;

namespace Padutronics.Diagnostics.Tracing.Console.Listeners;

using System;

public sealed class ConsoleTraceListener : TraceListener
{
    private readonly ConsoleTraceListenerFormatOptions formatOptions;

    public ConsoleTraceListener() :
        this(new ConsoleTraceListenerFormatOptions())
    {
    }

    public ConsoleTraceListener(ConsoleTraceListenerFormatOptions formatOptions)
    {
        this.formatOptions = formatOptions;
    }

    public ConsoleTraceListener(ITraceFilter filter) :
        this(filter, new ConsoleTraceListenerFormatOptions())
    {
    }

    public ConsoleTraceListener(ITraceFilter filter, ConsoleTraceListenerFormatOptions formatOptions) :
        base(filter)
    {
        this.formatOptions = formatOptions;
    }

    public override void ProcessTrace(TraceEntry entry)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;

        Console.Write(new string(' ', entry.Format.IndentLevel));

        Console.ForegroundColor = formatOptions.NamespaceColor;
        Console.Write(entry.Caller.Namespace);

        Console.ForegroundColor = formatOptions.TextColor;
        Console.Write('.');

        Console.ForegroundColor = formatOptions.TypeNameColor;
        Console.Write(entry.Caller.TypeName);

        Console.ForegroundColor = formatOptions.TextColor;
        Console.Write('.');

        Console.ForegroundColor = formatOptions.MemberNameColor;
        Console.Write(entry.Caller.MemberName);

        Console.ForegroundColor = formatOptions.TextColor;
        Console.Write(':');

        Console.ForegroundColor = formatOptions.LineNumberColor;
        Console.Write(entry.Caller.LineNumber);

        if (!formatOptions.SeverityToColorMappings.TryGetValue(entry.Trace.Severity, out ConsoleColor color))
        {
            color = formatOptions.TextColor;
        }

        Console.ForegroundColor = color;
        Console.Write($" {entry.Trace.Message}");

        Console.WriteLine();

        Console.ForegroundColor = foregroundColor;
    }
}