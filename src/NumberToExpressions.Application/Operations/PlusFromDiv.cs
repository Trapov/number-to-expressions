namespace NumberToExpressions.Application.Operations {

  using System;

  public sealed class PlusFromDiv : IOperation {
    public (Double y, Double z, Double a) Handle(Double x) {
      var reminder = x % 2;
      var div = Math.Floor(x / 2);
      return (div, div, reminder);
    }

    public override String ToString() => "+";
  }
}
