namespace NumberToExpressions.Application.Expressions {
  public interface IExpressionsFinder {
    Node FindExpressions(System.Double number, System.UInt32 deepLength);
  }
}