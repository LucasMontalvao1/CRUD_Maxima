using API.Infra.Data;
using API.Repositorys.Interfaces;
using API.Repositorys;
using API.Services.Interfaces;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MySqlConnectionDB>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
