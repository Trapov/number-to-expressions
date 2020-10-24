namespace NumberToExpressions.Web.Controllers {

  using System;
  using System.Diagnostics;
  using System.Globalization;

  using Microsoft.AspNetCore.Mvc;

  using NumberToExpressions.Expressions;

  [ApiController]
  public sealed class ApiController : ControllerBase {
    private readonly IExpressionsFinder _expressionsFinder;
    private readonly IToStringExpressionsEvaluator _expressionsEvaluator;

    public ApiController(IExpressionsFinder expressionsFinder, IToStringExpressionsEvaluator expressionsEvaluator) {
      _expressionsFinder = expressionsFinder;
      _expressionsEvaluator = expressionsEvaluator;
    }

    public sealed class FindExpressionsBinding {
      public Double Number { get; set; }
      public UInt32? Complexity { get; set; } = 4;
    }

    [HttpGet("/api/expressions")]
    public IActionResult ExpressionsGet([FromQuery] FindExpressionsBinding getExpressionsBinding) =>
      Execute(getExpressionsBinding);

    [HttpPost("/api/expressions")]
    public IActionResult ExpressionsPost([FromBody] FindExpressionsBinding getExpressionsBinding) =>
      Execute(getExpressionsBinding);

    private IActionResult Execute(FindExpressionsBinding getExpressionsBinding) {
      var stopWatch = new Stopwatch();
      stopWatch.Start();

      var result = _expressionsEvaluator.Evaluate(_expressionsFinder.FindExpressions(getExpressionsBinding.Number, getExpressionsBinding.Complexity.Value));
      stopWatch.Stop();
      return Ok(new {
        Value = String.IsNullOrWhiteSpace(result) 
          ? getExpressionsBinding.Number.ToString(CultureInfo.InvariantCulture) 
          : result,
        ElapsedMs = stopWatch.ElapsedMilliseconds
      });
    }
  }
}
