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

        node.Left = new ExpressionNode {
          Value = x,
          IsLeft = true,
          Parent = node
        };

        node.Middle = new ExpressionNode {
          Value = y,
          IsMiddle = true,
          Parent = node
        };

        node.Right = new ExpressionNode {
          Value = reminder,
          IsRight = true,
          Parent = node
        };

        stack.Push(node.Left);
        stack.Push(node.Middle);
        stack.Push(node.Right);
      }

      return rootNode;
    }
  }
}
