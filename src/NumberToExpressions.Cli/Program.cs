namespace NumberToExpressions.Cli {

  using System;
  using System.Diagnostics;

  using NumberToExpressions.Expressions;
  using NumberToExpressions.Expressions.Operations;

  public static class Program {
    public static void Main(String[] args) {
      var arguments = ParseArgs(args);

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

      Console.WriteLine(evaulator.Evaluate(node));
      
      if (arguments.Verbose) {
        stopWatch.Stop();
        Console.WriteLine("----------------");
        Console.WriteLine("\u001b[32;1mNumber:\u001b[0m{0}", arguments.Number);
        Console.WriteLine("\u001b[32;1mComplexity:\u001b[0m {0}", arguments.Complexity);
        Console.WriteLine("\u001b[32mSeed:\u001b[0m {0}", arguments.Seed);
        Console.WriteLine("\u001b[32mElapsed:\u001b[0m {0} ms", stopWatch.ElapsedMilliseconds);
      }
    }

    private static Arguments ParseArgs(String[] args) {
      Arguments arguments = null;

      try {
        arguments = new Arguments(args);
      }
      catch (InvalidOperationException e) {
        Console.WriteLine(e.Message);
        Console.WriteLine("------------");
        Console.WriteLine(e.InnerException);
        Environment.Exit(-1);
      }

      return arguments;
    }
  }

}
