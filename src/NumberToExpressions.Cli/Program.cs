namespace NumberToExpressions.Cli {

  using System;
  using System.Diagnostics;
  using System.IO;

  using CommandLine;

  using NumberToExpressions.Expressions;
  using NumberToExpressions.Expressions.Operations;


  public static class Program {
    public static void Main(String[] args) {
      Parser.Default.ParseArguments<Arguments>(args)
        .WithParsed(NumberToExpressionLogic);
    }

    private static void NumberToExpressionLogic(Arguments arguments) {
      var finder = new IterativeExpressionsFinder(new IOperation[] {
        new PlusFromDiv(),
        new Plus(arguments.Seed),
        new Substruct(arguments.Seed),
        new Multiply(arguments.Seed),
        new Divide(arguments.Seed)
      }, arguments.Seed);

      Stopwatch stopWatch = null;
      if (arguments.Verbose) {
        stopWatch = new Stopwatch();
        stopWatch.Start();
      }
      var evaulator = new IterativeToStringExpressionsEvaluator();
      var node = finder.FindExpressions(arguments.Number, arguments.Complexity);

      if (string.IsNullOrWhiteSpace(arguments.FileName)) {
        Console.WriteLine(evaulator.Evaluate(node));
      } else {
        File.WriteAllText(arguments.FileName, evaulator.Evaluate(node));
      }

      if (arguments.Verbose) {
        stopWatch.Stop();
        Console.WriteLine("----------------");
        Console.WriteLine("\u001b[32;1mNumber:\u001b[0m{0}", arguments.Number);
        Console.WriteLine("\u001b[32;1mComplexity:\u001b[0m {0}", arguments.Complexity);
        Console.WriteLine("\u001b[32mSeed:\u001b[0m {0}", arguments.Seed);
        Console.WriteLine("\u001b[32mElapsed:\u001b[0m {0} ms", stopWatch.ElapsedMilliseconds);
      }
    }
  }
}
