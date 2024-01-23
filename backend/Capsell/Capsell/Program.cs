using Capsell.DataProvide;
using Capsell.Repositories.Authenticate;
using Capsell.Repositories.Orders;
using Capsell.Repositories.Products;
using Capsell.Repositories.Shops;
using Capsell.Services.Authenticate;
using Capsell.Services.Cache;
using Capsell.Services.Orders;
using Capsell.Services.Products;
using Capsell.Services.Shops;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
.Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddDbContext<CapsellDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CapsellDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IRegisterationRepo, RegisterationRepo>();
builder.Services.AddScoped<IRegisterationService, RegisterationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IShopRepo, ShopRepo>();
builder.Services.AddScoped<ICacheService, CacheService>();





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Ravan Service", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
});
});

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
