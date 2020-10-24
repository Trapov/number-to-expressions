namespace NumberToExpressions.Web {
  using System;

  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

  using NumberToExpressions.Expressions;
  using NumberToExpressions.Expressions.Operations;

  public class Startup {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
      services.AddControllersWithViews();

      var seed = Guid.NewGuid().GetHashCode();

      var finder = new IterativeExpressionsFinder(new IOperation[] {
        new PlusFromDiv(),
        new Plus(seed),
        new Substruct(seed),
        new Multiply(seed),
        new Divide(seed)
      }, seed);
      var evaluator = new IterativeToStringExpressionsEvaluator();

      services.AddSingleton<IExpressionsFinder>(finder);
      services.AddSingleton<IToStringExpressionsEvaluator>(evaluator);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }
      app.UseStaticFiles();

      app.UseRouting();
      app.UseEndpoints(endpoints => {
        endpoints.MapDefaultControllerRoute();
      });
    }
  }
}
