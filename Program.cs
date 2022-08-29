//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Diagnostics;

string input = "watch.txt";
if (args.Length > 0 && File.Exists(args[0]))
{
    input = args[0];
}

if (!File.Exists(input))
{
    throw new FileNotFoundException(input);
}

foreach (string line in await File.ReadAllLinesAsync(input))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        // Empty line
        continue;
    }

    if (line.StartsWith('#'))
    {
        // Comment
        continue;
    }

    string expanded = Environment.ExpandEnvironmentVariables(line);
    Console.WriteLine(expanded);
    if (!File.Exists(expanded))
    {
        Console.WriteLine("File doesn't exist.");
        continue;
    }

    string filename = Path.GetFileNameWithoutExtension(expanded);
    Process[] processes = Process.GetProcessesByName(filename);
    if (processes.Any())
    {
        Console.WriteLine("Process already running.");
        continue;
    }

    Console.WriteLine("Starting process.");
    Process.Start(expanded);
}