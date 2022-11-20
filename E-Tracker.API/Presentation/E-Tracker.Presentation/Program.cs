var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options=>options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(config=>config.RegisterValidatorsFromAssemblyContaining<CreateProductViewModel>())
    .ConfigureApiBehaviorOptions(config=>config.SuppressModelStateInvalidFilter = true);
builder.Services.AddInfrastructureService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPersistenceService();
builder.Services.AddInfrastructureServices();
builder.Services.AddDbConnection(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddSignalRService();
builder.AddLogger();
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHubs();
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.Run();
