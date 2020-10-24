namespace NumberToExpressions.Expressions {

  using System;
  using System.Diagnostics;

  using NumberToExpressions.Expressions.Operations;

  [DebuggerDisplay("{Value} = {Left?.Value} {Operation} {Middle?.Value} + {Right?.Value}")]
  public sealed class ExpressionNode {
    public IOperation Operation { get; set; }
    public Double Value { get; set; }

    public Boolean IsLeft { get; set; }
    public Boolean IsMiddle { get; set; }
    public Boolean IsRight { get; set; }

    public ExpressionNode Left { get; set; }
    public ExpressionNode Middle { get; set; }
    public ExpressionNode Right { get; set; }

    public ExpressionNode Parent { get; set; }
  }
}
