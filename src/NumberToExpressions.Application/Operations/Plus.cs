namespace NumberToExpressions.Application.Operations {

  using System;

  public sealed class Plus : IOperation {
    private readonly Random _random;
    public Plus() : this(null) { }
    public Plus(Int32? seed) {
      _random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public (Double y, Double z, Double a) Handle(Double x) {
      var subtrBy = _random.Next(1, 15);
      var y = x - subtrBy;
      return (y, subtrBy, 0);
    }

    public override String ToString() => "+";
  }
}
