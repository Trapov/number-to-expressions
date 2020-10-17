namespace NumberToExpressions.Application.Operations {

  using System;

  public sealed class Substruct : IOperation {
    private readonly Random _random;

    public Substruct() : this(null) { }
    public Substruct(Int32? seed) {
      _random = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public (Double y, Double z, Double a) Handle(Double x) {
      var plusBy = _random.Next(1, 15);
      var assigned = x + plusBy;
      return (assigned, plusBy, 0);
    }

    public override String ToString() => "-";
  }
}
