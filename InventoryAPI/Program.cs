using System.Threading.RateLimiting;
using Asp.Versioning;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


// Rate limiter

// builder.Services.AddRateLimiter(options =>
// {
//     options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//     {
//         return RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(),
//          factory: partition =>
//             new FixedWindowRateLimiterOptions
//             {
//                 PermitLimit = 1,
//                 Window = TimeSpan.FromSeconds(10),
//                 QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
//                 QueueLimit = 0
//             });
//     });
//     options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
// });

// builder.Services.AddRateLimiter(option =>
// option.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
// return RateLimitPartition.GetConcurrencyLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(),
// factory: partition =>
//     new ConcurrencyLimiterOptions
//     {
//         PermitLimit = 1,
//         QueueLimit = 0
//     })));

// builder.Services.AddRateLimiter(option =>
// {
//     option.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//     {
//         return RateLimitPartition.GetSlidingWindowLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(),
//         factory: partition =>
//             new SlidingWindowRateLimiterOptions
//             {
//                 PermitLimit = 4,
//                 Window = TimeSpan.FromSeconds(10),
//                 SegmentsPerWindow = 2,
//                 QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
//                 QueueLimit = 2
//             });
//     });
// });


builder.Services.AddRateLimiter(option =>
{
    option.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
    {
        return RateLimitPartition.GetTokenBucketLimiter(partitionKey: httpContext.Request.Headers.Host.ToString(),
        factory: partition =>
            new TokenBucketRateLimiterOptions
            {
                TokenLimit = 3,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2,
                ReplenishmentPeriod = TimeSpan.FromSeconds(10),
                TokensPerPeriod = 2,
                AutoReplenishment = true
            }
           );
    });
});

builder.Services.AddScoped<Business.Contract.I_ItemMasterManager, Business.Implementation.ItemMasterManager>();
builder.Services.AddScoped<Data.Contract.I_ItemMasterRepository, Data.Repository.ItemMasterRepository>();

var connectionString =
    $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
    $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"User={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
    $"AllowPublicKeyRetrieval=True;" +
    $"SslMode=None;";

// var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
Console.WriteLine($"Connection String: {connectionString}");

builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "Test API - .NET 9",
                Version = "v1",
                Description = "API documentation for .NET 9 application"
            });
        });



// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1.0);
    options.AssumeDefaultVersionWhenUnspecified = false;
    options.ReportApiVersions = true;

    // Read version from URL, Header, Query
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("api-version"),
        new QueryStringApiVersionReader("api-version")
    );
    // options.ApiVersionReader = new UrlSegmentApiVersionReader();

}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

app.UseExceptionHandler(x => { });
app.UseRateLimiter();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
    options.DocumentTitle = "Test .NET 9 API Documentation";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
