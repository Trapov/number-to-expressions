namespace NumberToExpressions.Web.Controllers {

  using System;
  using System.Globalization;

  using Microsoft.AspNetCore.Mvc;

  using NumberToExpressions.Application.Expressions;


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
      public UInt32 Complexity { get; set; }
    }

    [HttpPost("/api/expressions")]
    public IActionResult Expressions(
        [FromBody] FindExpressionsBinding getExpressionsBinding
    ) {

      var result = _expressionsEvaluator.Evaluate(_expressionsFinder.FindExpressions(getExpressionsBinding.Number, getExpressionsBinding.Complexity));

      return Ok(new { Value = string.IsNullOrWhiteSpace(result) ? getExpressionsBinding.Number.ToString(CultureInfo.InvariantCulture) : result });
    }
  }
}
