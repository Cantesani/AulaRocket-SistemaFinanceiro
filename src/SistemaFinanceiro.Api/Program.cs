using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SistemaFinanceiro.Api.Filtros;
using SistemaFinanceiro.Api.MiddleWare;
using SistemaFinanceiro.Application;
using SistemaFinanceiro.Infrastructure;
using SistemaFinanceiro.Infrastructure.Migrations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = @"JWT Authorization header using the Bearer Schema.
                        Enter 'Beader' [space] and then your token in the next input below.
                        Example: Bearer 1234abcdfe",
        In = ParameterLocation.Header,
        Scheme = "Beader",
        Type = SecuritySchemeType.ApiKey
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
             },
        new List<string>()
        }

    });
});

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFiltro)));

//Metodo criado a partir do parametro 'this' na injecao de dependencia. (chamado de extension)
//Feito para nao precisar instanciar o metodo. 
//Para nao precisar instanciar o metodo. 
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var signinKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigninKey");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
    {
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = new TimeSpan(0),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey!))
        };
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrateDataBase();

app.Run();



async Task MigrateDataBase()
{
    await using var scope = app.Services.CreateAsyncScope();

    await DataBaseMigration.MigrateDataBase(scope.ServiceProvider);

}