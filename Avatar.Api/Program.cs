using AutoMapper;
using Avatar.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Avatar.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(opts =>
    {
        opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper configuration
var mapperConfig = new MapperConfiguration(mapper => {
    mapper.AddProfile(new Avatar.Api.Models.MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Register application services
builder.Services.ExtendServices();

var app = builder.Build();

// Seed database data and ensure tables are created
using (var scope = app.Services.CreateScope())
{
    //var store = scope.ServiceProvider.GetRequiredService<IDataStore>();
    //await store.EnsureMigrate();
}

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