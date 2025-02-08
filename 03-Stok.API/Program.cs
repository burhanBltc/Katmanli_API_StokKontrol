using _01_Stok.Entities.Models.Concrete;
using _02_Stok.Repositories.Context;
using _02_Stok.Repositories.Repositories.Abstract;
using _02_Stok.Repositories.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StokDbContext>(opt=>opt.UseSqlServer("Server=.;Database=StokYzl5101;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=True;"));  //migration dan sonra yazd�k

//builder.Services.AddScoped<IGenericRepo<Category>, GenericRepo<Category>>();
//builder.Services.AddScoped<IGenericRepo<Supplier>, GenericRepo<Supplier>>();
//builder.Services.AddScoped<IGenericRepo<Product>, GenericRepo<Product>>();
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));//tek tek yazmak yerine

//ili�kilileri getirirken cycle-d�ng�ye girmesin diye api katman�na newtonsoft nuget ekledik
builder.Services.AddControllers().AddNewtonsoftJson(opt=>
{
    opt.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
