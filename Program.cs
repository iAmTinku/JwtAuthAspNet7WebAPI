//https://github.com/iAmTinku/JwtAuthAspNet7WebAPI/tree/main
//https://www.youtube.com/watch?v=KRVjIgr-WOU
//the Program.cs file is indeed one of the first files to run. It is responsible for setting up the application, configuring services,
//and defining the request handling pipeline. The appsettings.json file is used for configuration, but it is read and utilized within
//the code, typically during the setup in Program.cs


using JwtAuthAspNet7WebAPI.Core.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//This initializes a new instance of the WebApplicationBuilder class, which is used to configure the application, including services and
//the request pipeline. automatically loasd appsetitings.json, environment variables, which can be accessed by builder.configuration
var builder = WebApplication.CreateBuilder(args);


//all builder.services is used to  register services with the Dependency Injection (DI) container. These services can include framework
//services (like Entity Framework, Identity, MVC), third-party services, and your own application services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add DB
//This sets up the database context using Entity Framework Core, connecting to a SQL Server database.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//add identity -> "Adding Identity" involves registering the necessary services with the ASP.NET Core dependency injection (DI) container.
//This is done using the AddIdentity method, which sets up the default services for managing users, roles, and authentication.
//The AddEntityFrameworkStores method tells Identity to use Entity Framework Core for storing user information.
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();




//Config Identity -> "Configuring Identity" means setting up options and customizing the behavior of the Identity system.
//This can include configuring password requirements, lockout settings, user validation settings, etc.
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
});
//add authentication andd jwtbearer
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });



// piepline start, the below line marks the end of configuration and builds web application
var app = builder.Build();

//This sets up the middleware components that handle requests and responses
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// These lines add the authentication and authorization middleware to the request pipeline, ensuring that authentication and authorization are handled properly for incoming requests.
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
