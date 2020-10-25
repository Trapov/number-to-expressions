namespace NumberToExpressions.Expressions {

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NumberToExpressions.Expressions.Internal;
  using NumberToExpressions.Expressions.Operations;

  public sealed class IterativeExpressionsFinder : IExpressionsFinder {

    private readonly CircullarReadOnlyCollection<IOperation> _circullarOperations;

    public IterativeExpressionsFinder(IOperation[] operations, Int32? seed) {
      _circullarOperations = new CircullarReadOnlyCollection<IOperation>(operations, seed);
    }

    public ExpressionNode FindExpressions(Double number, UInt32 deepLength) {

      var rootNode = new ExpressionNode {
        Value = number
      };
      var node = rootNode;
      Double x, y, reminder = 0;
      var stack = new Stack<ExpressionNode>();
      while (deepLength-- > 0) {

        if (stack.Any()) {
          node = stack.Pop();
        }

        var operation = _circullarOperations.Next();
        node.Operation = operation;

        (x, y, reminder) = operation.Handle(node.Value);

        if (IsLeftOrMiddleLesserThanZero(x, y)) {
          continue;
        }

        node.Left = new ExpressionNode {
          Value = x,
          IsLeft = true,
          Parent = node
        };
        stack.Push(node.Left);

        node.Middle = new ExpressionNode {
          Value = y,
          IsMiddle = true,
          Parent = node
        };
        stack.Push(node.Middle);

        node.Right = new ExpressionNode {
          Value = reminder,
          IsRight = true,
          Parent = node
        };
        PushIfGreaterThanZero(stack, node.Right);
      }

      return rootNode;
    }

    private static Boolean IsLeftOrMiddleLesserThanZero(Double left, Double middle) {
      return Math.Abs(left) <= 0 || Math.Abs(middle) <= 0;
    }

    private static void PushIfGreaterThanZero(Stack<ExpressionNode> stack, ExpressionNode node) {
      if (Math.Abs(node.Value) > 0) {
        stack.Push(node);
      }
    }
  }
}
