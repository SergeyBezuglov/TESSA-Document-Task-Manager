using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Nest;
using PIMS.Application;
using PIMS.Infrastructure;
using PIMS.Web;
using PIMS.Web.Extensions;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
	Args = args


});
var settings = new ConnectionSettings(new Uri("http://localhost:5173")) // Укажите правильный URI к вашему Elasticsearch
    .DefaultIndex("index.html"); // Укажите индекс по умолчанию

var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);
{   
    builder.Services.
    AddPresentation(builder).
    AddApplication().
    AddInfrastracture(builder); 
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();
{
     
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    
    app.MigrateDatabase();

    app.UseExceptionHandler("/error");

    app.UseSwaggerExtension(provider);

    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseCors("MyAllowSpecificOrigins");
    app.UseStaticFiles();
   
    app.UseRouting();
    app.UseAuthentication();

    app.UseAuthorization();

    //app.UseStatusCodePages(async context => {
    //    var request = context.HttpContext.Request;
    //    var response = context.HttpContext.Response;

    //    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)      
    //    {
    //        string str = "";
    //       }
    //});
  
    
   

    app.MapControllers();
    app.ConfigureSPA();
    
    
    app.Run();


}
public partial class Program { }