

using System.Text;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddAuthentication("Admin")
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
            
        };
    });
//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage<AzureStorage>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();

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

app.UseAuthorization();
app.MapControllers();

app.Run();
