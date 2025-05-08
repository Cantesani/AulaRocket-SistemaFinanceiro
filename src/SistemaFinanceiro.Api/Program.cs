using SistemaFinanceiro.Api.Filtros;
using SistemaFinanceiro.Api.MiddleWare;
using SistemaFinanceiro.Application;
using SistemaFinanceiro.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(option => option.Filters.Add(typeof(ExceptionFiltro)));

//Metodo criado a partir do parametro 'this' na injecao de dependencia. (chamado de extension)
//Feito para nao precisar instanciar o metodo. 
//Para nao precisar instanciar o metodo. 
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
