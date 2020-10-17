namespace NumberToExpressions.Application.Operations {

  using System;

  public interface IOperation {
    (Double y, Double z, Double a) Handle(Double x);
  }
}
