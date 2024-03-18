using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WeGapApi.Data;
using WeGapApi.Models;
using Microsoft.OpenApi.Models;
using AutoMapper;
using WeGapApi.Mappings;
using WeGapApi.Repository.Interface;
using WeGapApi.Repository;
using WeGapApi.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using WeGapApi.Services.Services.Interface;
using WeGapApi.Services;
using Azure.Storage.Blobs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
    AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();


//Key in appsettings.json is accessed
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");


//Add authentication using JWT token

builder.Services.AddAuthentication(u =>
{
    u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(u =>
{
    u.RequireHttpsMetadata = false;
    u.SaveToken = true;
    u.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});

builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Adding Config
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IEmployerRepository, EmployerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IJobSkillRepository, JobSkillRepository>();
builder.Services.AddScoped<IJobTypeRepository, JobTypeRepository>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
//builder.Services.AddScoped<ILogger>();

builder.Services.AddSingleton(u => new BlobServiceClient(builder.Configuration.GetConnectionString("StorageAccount")));
builder.Services.AddSingleton<IBlobService, BlobService>();
//Adding Authorization in swagger for JWTBearer Support
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description =
        "JWT Authorization header using the bearer scheme \r\n\r\n" +
        "Enter 'Bearer' [space] and then your token in the text input below \r\n\r\n " +
        "Example : \"Bearer 1234abcdef\"",

        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
        new OpenApiSecurityScheme
        {
            Reference =  new OpenApiReference
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

