using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace DCODatabase.ToolBox.Security.JWT
{
    public class TokenService
    {
        private readonly string _signature, _issuer, _audience;
        private readonly int _delay;
        private readonly string _hashtype;

        private readonly JwtSecurityTokenHandler _handler; // Read or Write Token

        // Can be tested into JWT.io website

        // ************************************************************************************************
        // for a console Application
        // add into App.config
        //  <appSettings>
        //	    <add key = "signature" value="azertyuiopqsdfghjklmwxcvbnaaaaaaaaaaaaaaaaaaaaaaaaaaaa"/>
        //	    <add key = "delay" value="84600"/>
        //	    <add key = "hashtype" value="HS256"/>
        //  </appSettings>

        // ************************************************************************************************
        //for an API

        // add into appsettings.json
        //     "Security": {
        //     "Signature": "masignaturedecoop",
        //     "Delay":  3600

        // Add into Startup.cs of the API in ConfigureServices

        //     // DCO ADD
        //     //services.AddTransient<TokenService>((x) => new TokenService(
        //     // or 
        //     // services.AddTransient(typeof(TokenService),(x) => new TokenService(
        //     // or

        // real code to add
        //  services.AddTransient((x) => new TokenService(
        //    Configuration.GetSection("Security").GetValue<string>("Signature"), 
        //        null, 
        //        null, 
        //        Configuration.GetSection("Security").GetValue<int>("Delay")
        //    ));

        // Then to use it
        //public IActionResult UserLogin([FromBody] SM.User u, [FromServices] TokenService ts)
        //{
            //if (u == null)
            //    return BadRequest();
            //else
            //{
            //    string token = ts.EncodeToken(S.ServiceLocator.Instance.usersService.Login(u.Email, u.Passwd));
            //    return Ok(token);
            //}
        //}

        public TokenService()
        {
            _signature = ConfigurationManager.AppSettings.Get("signature");
            _issuer = ConfigurationManager.AppSettings.Get("issuer");
            _audience = ConfigurationManager.AppSettings.Get("audience");
            int.TryParse(ConfigurationManager.AppSettings.Get("delay"), out _delay);

            if (_signature == null || _delay <= 0) throw new ConfigurationErrorsException();

            _handler = new JwtSecurityTokenHandler();

            _hashtype = ConfigurationManager.AppSettings.Get("hashtype");
            if (_hashtype == "" || _hashtype == null) _hashtype = SecurityAlgorithms.HmacSha512;

        }
        public TokenService(string signature, string issuer, string audience, int delay, string hashtype = SecurityAlgorithms.HmacSha256)
        {
            _signature = signature;
            _issuer = issuer;
            _audience = audience;
            _delay = delay;
            _handler = new JwtSecurityTokenHandler();
            _hashtype = hashtype;
        }

        public string EncodeToken<T>(T o)
        {
            // SecurityAlgorithms.HmacSha256 could be passed in paramter
            JwtSecurityToken token = new JwtSecurityToken(
                _issuer,
                _audience,
                GetClaims(o),
                DateTime.Now,
                DateTime.Now.AddSeconds(_delay),
                GetSigningCredential(_hashtype)
                );

            // structure is 
            //header
            //payload
            //Verify Signature

            return _handler.WriteToken(token);
        }

        private SigningCredentials GetSigningCredential(string hashtype)
        {
            // SecurityAlgorithms.HmacSha256 could be passed in paramter
            return new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signature)), hashtype);
        }

        private IEnumerable<Claim> GetClaims<T>(T o)
        {
            //* create a claim for each properties (ID,LastName,FirstName,....)

            //** could be use o.GetType() to replace typeof(T)
            // better to use typeof becaus eyou dont have the object instance with that 
            // IEnumerable < PropertyInfo > props = o.GetType().GetProperties();

            IEnumerable<PropertyInfo> props = typeof(T).GetProperties();
            return props.Select(p => new Claim(p.Name, p.GetValue(o).ToString()));
        }

        public ClaimsPrincipal DecodeToken(string token)
        {
            try
            {
                return _handler.ValidateToken(token, GetParmeters(), out SecurityToken key);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private TokenValidationParameters GetParmeters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = _issuer != null,
                ValidIssuer = _issuer,
                ValidateAudience = _audience != null,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signature))
            };
        }
    }
}
