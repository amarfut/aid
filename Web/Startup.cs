using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle("Google", options =>
                {
                    options.ClientId = "684772232762-1o7rdk7do85q1ctvpb56o4of1oh2djdi.apps.googleusercontent.com";
                    options.ClientSecret = "Kjzp_QFOVeXwpJA2f_E4bnyG";
                    options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            var identity = (ClaimsIdentity)context.Principal.Identity;
                            var profileImg = context.User["picture"].ToString();
                            identity.AddClaim(new Claim(Constants.ProfileImage, profileImg));
                            return Task.FromResult(0);
                        }
                    };
                })
                .AddFacebook("Facebook", options =>
                {
                    options.ClientId = "2380770835522263";
                    options.ClientSecret = "425b8e40404250c37792cf67c26d549e";
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            var identity = (ClaimsIdentity)context.Principal.Identity;
                            Claim nameIdentifier = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                            string profileImg = $"https://graph.facebook.com/{nameIdentifier.Value}/picture?type=large";
                            identity.AddClaim(new Claim(Constants.ProfileImage, profileImg));
                            return Task.FromResult(0);
                        }
                    };
                })
                .AddVkontakte("VKontakte", options =>
                {
                    options.ClientId = "7216246";
                    options.ClientSecret = "q5eUFXzAOjH2fB2yvXl0";
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            string givenname = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                            string surname = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                            string photo = context.Principal.Claims.FirstOrDefault(c => c.Type == "urn:vkontakte:photo_thumb:link")?.Value;

                            string userName = $"{givenname} {surname}";

                            var identity = (ClaimsIdentity)context.Principal.Identity;
                            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                            identity.AddClaim(new Claim(Constants.ProfileImage, photo));

                            return Task.FromResult(0);
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
