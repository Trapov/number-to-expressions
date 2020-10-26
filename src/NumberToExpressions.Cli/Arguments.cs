namespace NumberToExpressions.Cli {

  using System;

  using CommandLine;

  public sealed class Arguments {

    [Value(0, Required = true, HelpText = "Number to find expressions")]
    public Double Number { get; private set; }

    [Option(longName: "complexity", shortName: 'c', Default = 4u, Required = false)]
    public UInt32 Complexity { get; set; }

    [Option(longName: "seed", shortName: 's', Required = false)]
    public Int32 Seed { get; private set; }

    [Option(longName: "file", shortName: 'f', Required = false)]
    public string FileName { get; private set; }

    [Option(longName: "verbose", shortName: 'v', Required = false)]
    public Boolean Verbose { get; private set; }
  }

}
