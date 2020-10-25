namespace NumberToExpressions.Expressions.Operations {

  using System;

  public sealed class PlusFromDiv : IOperation {
    public (Double y, Double z, Double a) Handle(Double x) {
      var reminder = x % 2;
      var div = x > 0 ? Math.Floor(x / 2) : Math.Ceiling(x / 2);
      return (div, div, reminder);
    }

    public override String ToString() => "+";
  }
}
