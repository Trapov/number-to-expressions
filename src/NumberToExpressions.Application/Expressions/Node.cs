namespace NumberToExpressions.Application.Expressions {

  using System;
  using System.Diagnostics;

  using NumberToExpressions.Application.Operations;

  [DebuggerDisplay("{Value} = {Left?.Value} {Operation} {Middle?.Value} + {Right?.Value}")]
  public sealed class Node {
    public IOperation Operation { get; set; }
    public Double Value { get; set; }

    public Boolean IsLeft { get; set; }
    public Boolean IsMiddle { get; set; }
    public Boolean IsRight { get; set; }

    public Node Left { get; set; }
    public Node Middle { get; set; }
    public Node Right { get; set; }

    public Node Parent { get; set; }
  }
}
