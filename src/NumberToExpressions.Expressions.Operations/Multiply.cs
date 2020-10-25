namespace NumberToExpressions.Expressions.Operations {

  using System;

  public sealed class Multiply : IOperation {
    private readonly Random _random;

    public Multiply() : this(null) { }
    public Multiply(Int32? seed) {
      _random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public (Double y, Double z, Double a) Handle(Double x) {
      var divideBy = _random.Next(1, 15);
      var reminder = (x % divideBy);
      var div = x > 0 ? Math.Floor(x / divideBy) : Math.Ceiling(x / divideBy);
      return (div, divideBy, reminder);
    }

    public override String ToString() => "*";
  }
}
