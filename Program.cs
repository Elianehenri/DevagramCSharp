using DevagramCSharp.Models;
using DevagramCSharp.Repository;
using DevagramCSharp.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Text;
using DevagramCSharp.Repository.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuraçao do banco de dados
var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DevagramContext>(options => options.UseSqlServer(connectionstring));
builder.Services.AddScoped<ISeguidorRepository, SeguidorRepositoryImpl>();
builder.Services.AddScoped<IPublicacaoRepository, PublicacaoRepositoryImpl>();
builder.Services.AddScoped<IComentarioRepository, ComentarioRepositoryImpl>();
builder.Services.AddScoped<ICurtidaRepository, CurtidaRepositoryImpl>();


builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryImpl>();


//token service => todas as APis tenha verificaçao JWT
var chaveCriptografia = Encoding.ASCII.GetBytes(ChaveJWT.ChaveSecreta);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(autenticacao =>
{
    autenticacao.RequireHttpsMetadata = false;
    autenticacao.SaveToken = true;
    autenticacao.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chaveCriptografia),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
