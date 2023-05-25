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
        .WithOrigins("http://localhost:3000");
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
    Questions questionToReturn = await QuestionsRepository.GetQuestionByIdAsync(questionId);

    if (questionToReturn != null)
    {
        return Results.Ok(questionToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Questions Endpoints");

app.MapPost("/create-question", async(Questions questionToCreate) =>
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

app.MapPut("/update-question", async (Questions questionToUpdate) =>
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

app.MapGet("/get-all-surveys", async () => await SurveyRepository.GetSurveysAsync())
    .WithTags("Surveys Endpoints");

app.MapGet("/get-survey-by-id/{surveyId}", async (int surveyId) =>
{
    Surveys surveyToReturn = await SurveyRepository.GetSurveyByIdAsync(surveyId);

    if (surveyToReturn != null)
    {
        return Results.Ok(surveyToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Surveys Endpoints");

app.MapPost("/create-survey", async (Surveys surveyToCreate) =>
{
    bool createSuccessful = await SurveyRepository.CreateSurveyAsync(surveyToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Create successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Surveys Endpoints");

app.MapPut("/update-survey", async (Surveys surveyToUpdate) =>
{
    bool updateSuccessful = await SurveyRepository.UpdateSurveyAsync(surveyToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Surveys Endpoints");

app.MapDelete("/delete-survey-by-id/{surveyId}", async (int surveyId) =>
{
    bool deleteSuccessful = await SurveyRepository.DeleteSurveyAsync(surveyId);

    if (deleteSuccessful)
    {
        return Results.Ok("Delete successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Surveys Endpoints");

app.MapGet("/get-all-survey-answers", async () => await SurveyAnswersRepository.GetSurveyAnswersAsync())
    .WithTags("Survey Answers Endpoints");

app.MapGet("/get-survey-answer-by-id/{surveyAnswerId}", async (int surveyAnswerId) =>
{
    SurveyAnswers surveyAnswerToReturn = await SurveyAnswersRepository.GetSurveyAnswerByIdAsync(surveyAnswerId);

    if (surveyAnswerToReturn != null)
    {
        return Results.Ok(surveyAnswerToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Survey Answers Endpoints");

app.MapPost("/surveys/{surveyId}/create-survey-answer/", async (int surveyId, SurveyAnswers surveyAnswerToCreate) =>
{
    bool createSuccessful = await SurveyAnswersRepository.CreateSurveyAnswerAsync(surveyAnswerToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Create successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Survey Answers Endpoints");

app.MapPut("/update-survey-answer", async (SurveyAnswers surveyAnswerToUpdate) =>
{
    bool updateSuccessful = await SurveyAnswersRepository.UpdateSurveyAnswerAsync(surveyAnswerToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Survey Answers Endpoints");

app.MapDelete("/delete-survey-answer-by-id/{surveyId}", async (int surveyAnswerId) =>
{
    bool deleteSuccessful = await SurveyAnswersRepository.DeleteSurveyAnswerAsync(surveyAnswerId);

    if (deleteSuccessful)
    {
        return Results.Ok("Delete successful.");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Survey Answers Endpoints");

app.Run();