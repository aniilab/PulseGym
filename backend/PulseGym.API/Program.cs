﻿using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using PulseGym.API.Extensions;
using PulseGym.API.Middlewares;
using PulseGym.DAL;
using PulseGym.DAL.Models;
using PulseGym.DAL.Repositories;
using PulseGym.Logic.Facades;
using PulseGym.Logic.Services;

using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<PulseGymDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionString")));

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
})
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<PulseGymDbContext>()
.AddTokenProvider(builder.Configuration.GetSection("ApplicationName").Value, typeof(DataProtectorTokenProvider<User>));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Authentication:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Authentication:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Authentication:AccessTokenSecret").Value))
    };
});

// ConfigureMapster
MapsterExtension.AddMapsterConfiguration();

// Repositories
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IClientMembershipProgramRepository, ClientMembershipProgramRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IGroupClassRepository, GroupClassRepository>();
builder.Services.AddScoped<IMembershipProgramRepository, MembershipProgramRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IWorkoutRequestRepository, WorkoutRequestRepository>();

// Facades
builder.Services.AddScoped<IActivityFacade, ActivityFacade>();
builder.Services.AddScoped<IClientFacade, ClientFacade>();
builder.Services.AddScoped<IGroupClassFacade, GroupClassFacade>();
builder.Services.AddScoped<IMembershipProgramFacade, MembershipProgramFacade>();
builder.Services.AddScoped<ITrainerFacade, TrainerFacade>();
builder.Services.AddScoped<IWorkoutFacade, WorkoutFacade>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();

// Middlewares
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

