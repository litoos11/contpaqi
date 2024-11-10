using contpaqi.Middlewares;
using Contpaqi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Xml a Json",
        Version = "v1",
        Description = "Api para convertir xml a json",
        Contact = new()
        {
            Name = "Litoos11",
            Email = "litoos11@outook.com",
        }
    });

    //Obtención de token de oauth
    //TODO: Obtengo un 403 forbiden, por el momento el tokecn debe genrarse con POSTMAN
    options.AddSecurityDefinition("oauth2", new()
    {
        Type = SecuritySchemeType.OAuth2,
        // Name = "Auth0",
        Flows = new()
        {
            AuthorizationCode = new()
            {
                AuthorizationUrl = new("https://dev-otrwvksw.us.auth0.com/authorize?audience=https://invoice-transformation.cti.com"),
                TokenUrl = new("https://dev-otrwvksw.us.auth0.com/oauth/token"),
            }
        }
    });

    //Requiere el token de aouth para todas las peticiones 
    options.AddSecurityRequirement(new()
    {
        {
            new()
            {
                Reference = new()
                {
                    Id = "oauth2",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IXmlServices, XmlServices>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://dev-otrwvksw.us.auth0.com/authorize";
        options.Audience = "https://invoice-transformation.cti.com";
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseErrorResponse();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(e =>
{
    _ = e.MapControllers().RequireAuthorization();
});
app.MapControllers();

app.Run();
