using City_Info.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//now we will build services for our  DBcontext  and  input the name our DBContext into the angle bracket  just like we did in the data 
builder.Services.AddDbContext<CityDBContext>(options => options.UseInMemoryDatabase("ContactDB"));

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //checks weather if this is a development environment
{
	app.UseSwagger(); // it is importtant that use swagger comes before swagger UI
	app.UseSwaggerUI();
} 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
