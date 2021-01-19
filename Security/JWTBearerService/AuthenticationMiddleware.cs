using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
//using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DCODatabase.ToolBox.Security.JWTBearerService
{

    // //Dont work on Standard but work in core (CancelEventArgs't install)
       
    // //ligne a ajouté  dans startup.cs  ConfigureServices(IServiceCollection services) (APÏ)
    // //services.AddTokenAuthentication(Configuration);
       
    // //Ligne à ajoutée dans startup.cs // public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    // //app.UseAuthentication();  

    //public static class AuthenticationExtension
    //{
    //    //Add following Nugget Pack : System.IdentityModel.Tokens.Jwt  // Toolbox
    //    //Add following Nugget Pack : Microsoft.IdentityModel.Tokens   // Toolbox
    //    //Add following Nugget Pack : Microsoft.AspNetCore.Authentication.JwtBearer  // API

    //    public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
    //    {
    //        var secret = config.GetSection("JwtConfig").GetSection("secret").Value;

    //        var key = Encoding.ASCII.GetBytes(secret);
    //        services.AddAuthentication(x =>
    //        {
    //            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //        })
    //        .AddJwtBearer(x =>
    //        {
    //            x.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                IssuerSigningKey = new SymmetricSecurityKey(key),
    //                ValidateIssuer = false,
    //                ValidateAudience = false
    //                //ValidIssuer = "localhost",
    //                //ValidAudience = "localhost"
    //            };
    //        });

    //        return services;
    //    }
    //}
}