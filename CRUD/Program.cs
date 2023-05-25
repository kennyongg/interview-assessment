using CRUD.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Interview Assessment Api", Version = "v1"});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.DocumentTitle = "Interview Assessment";
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API for Interview Assessment");
    swaggerUIOptions.RoutePrefix = String.Empty;
});

app.UseCors("CORSPolicy");

app.MapGet("/get-all-questions", async () => await QuestionsRepository.GetQuestionsAsync())
    .WithTags("Questions Endpoints");

app.MapGet("/get-question-by-id/{questionId}", async (int questionId) =>
{
    Question questionToReturn = await QuestionsRepository.GetQuestionByIdAsync(questionId);

    if (questionToReturn != null)
    {
        return Results.Ok(questionToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Questions Endpoints");

app.MapPost("/create-question", async(Question questionToCreate) =>
{
    bool createSuccessful = await QuestionsRepository.CreateQuestionAsync(questionToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Create successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Questions Endpoints");

app.MapPut("/update-question", async (Question questionToUpdate) =>
{
    bool updateSuccessful = await QuestionsRepository.UpdateQuestionAsync(questionToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Questions Endpoints");

app.MapDelete("/delete-question-by-id/{questionId}", async (int questionId) =>
{
    bool deleteSuccessful = await QuestionsRepository.DeleteQuestionAsync(questionId);

    if (deleteSuccessful)
    {
        return Results.Ok("Delete successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Questions Endpoints");

app.Run();