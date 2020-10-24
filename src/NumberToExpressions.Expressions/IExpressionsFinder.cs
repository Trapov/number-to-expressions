namespace NumberToExpressions.Expressions {
  public interface IExpressionsFinder {
    ExpressionNode FindExpressions(System.Double number, System.UInt32 deepLength);
  }
}
