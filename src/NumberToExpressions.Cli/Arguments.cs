namespace NumberToExpressions.Cli {

  using System;
  using System.Linq;
  internal sealed class Arguments {

    private static class Defaults {
      internal static readonly UInt32 ComplexityDefault = 4;
      internal static readonly Int32 SeedDefault = Guid.NewGuid().GetHashCode();
      internal static readonly Boolean VerboseDefault = false;
    }

    internal readonly Double Number;
    internal readonly UInt32 Complexity;
    internal readonly Int32 Seed;
    internal readonly Boolean Verbose;

    internal Arguments(string[] args) {
      try {
        var required = args.Where(s => !s.Contains("--"));
        var optionals = args.Where(s => s.Contains("--")).ToArray();

        Number = Double.Parse(required.Single(), System.Globalization.CultureInfo.InvariantCulture);

        Complexity =
          GetOptionalOrDefault(
            args,
            nameof(Complexity),
            s => UInt32.Parse(s, System.Globalization.CultureInfo.InvariantCulture),
            Defaults.ComplexityDefault
          );

        Seed =
          GetOptionalOrDefault(
            args,
            nameof(Seed),
            s => Int32.Parse(s, System.Globalization.CultureInfo.InvariantCulture),
            Defaults.SeedDefault
          );

        Verbose =
          GetOptionalOrDefault(
            args,
            nameof(Verbose),
            s => string.IsNullOrWhiteSpace(s) || Boolean.Parse(s),
            Defaults.VerboseDefault
          );
      }
      catch (Exception e) {
        throw new InvalidOperationException("Usage ./number-to-expressions (number) [--complexity 4] [--seed 1231] [--verbose]", e);
      }
    }

    private static T GetOptionalOrDefault<T>(
      string[] optionals,
      string name,
      Func<String, T> parseFunc,
      T @default) {

      var argStr = optionals.FirstOrDefault(
        o => o.Contains(
            name,
            StringComparison.OrdinalIgnoreCase
          )
      );

      if (string.IsNullOrWhiteSpace(argStr)) {
        return @default;
      }
      var toRemove = $"--{name}";
      var arg = argStr.Substring(toRemove.Length);
      arg = arg.Replace("=", "");
      return parseFunc(arg);
    }
  }
}
