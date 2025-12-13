var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<Business.Contract.I_ItemMasterManager, Business.Implementation.ItemMasterManager>();
builder.Services.AddScoped<Data.Contract.I_ItemMasterRepository, Data.Repository.ItemMasterRepository>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
