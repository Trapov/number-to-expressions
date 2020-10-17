namespace NumberToExpressions.Application.Operations {

  using System;

  public sealed class Divide : IOperation {
    private readonly Random _random;

    public Divide() : this(null) { }
    public Divide(Int32? seed) {
      _random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public (Double y, Double z, Double a) Handle(Double x) {
      var divideBy = _random.Next(1, 15);
      var multiplied = x * divideBy;
      return (multiplied, divideBy, 0);
    }

    public override String ToString() => "/";
  }
}
