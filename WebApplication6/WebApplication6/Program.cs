using PracticeAPI.Services.CharacterService;
using PracticeAPI.Services.GameAccountService;
using PracticeAPI.Services.QuestService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICharacterService, CharacterService>(); // add singleton to save data state between requests
builder.Services.AddSingleton<IQuestService, QuestService>();
builder.Services.AddSingleton<IGameAccountService, GameAccountService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
