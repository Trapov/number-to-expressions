namespace NumberToExpressions.Expressions {

  using System;
  using System.Collections.Generic;
  using System.Linq;

  public sealed class IterativeToStringExpressionsEvaluator : IToStringExpressionsEvaluator {
    public String Evaluate(ExpressionNode root) {
      if (root.Left == null) {
        return root.Value.ToString();
      }

      var operators = new List<(String, List<String>)>();
      var s = new Stack<ExpressionNode>(new[] { root });

      while (s.Any()) {
        var node = s.Pop();

        if (node.Left == null) {
          (operators[0].Item2).Insert(0, node.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
          while ((operators[0].Item2).Count == 3 && operators.Count > 1) {
            var done = operators.ElementAt(0);
            operators.RemoveAt(0);

            (operators[0].Item2).Insert(0, String.Format("(({0}{1}{2})+{3})", done.Item2[0], done.Item1, done.Item2[1], done.Item2[2]));
          }

        } else {
          operators.Insert(0, (node.Operation.ToString(), new List<String>()));
          s.Push(node.Left);
          s.Push(node.Middle);
          s.Push(node.Right);
        }
      }

      var final = operators[0];
      return String.Format("(({0}{1}{2})+{3})", final.Item2[0], final.Item1, final.Item2[1], final.Item2[2]);
    }
  }
}
