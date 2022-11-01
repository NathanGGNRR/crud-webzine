using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using Webzine.Business;
using Webzine.Business.Contracts;
using Webzine.EntitiesContext;
using Webzine.Entity;
using Webzine.Repository;
using Webzine.Repository.Contracts;

namespace Webzine.WebApplication
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Si le fichier de config contient
            if (Configuration["DataSource"] == "factory")
            {
                // StaticFactory
                StaticFactory.UpdateBinavigabilite();
            }

            else if (Configuration["DataSource"] == "local")
            {
                using (var client = new WebzineDbContext())
                {
                    if (Configuration["ResetDb"] == "true")
                    {
                        client.Database.EnsureDeleted();
                        client.Database.EnsureCreated();

                        // Load data in database
                        LocalSeeder seeder = new LocalSeeder(client, StaticFactory.Styles, StaticFactory.Titres, StaticFactory.Artistes, StaticFactory.Commentaires, StaticFactory.LienStyles);
                        seeder.LoadData();
                        client.SaveChanges();
                    }

                    else
                    {
                        client.Database.EnsureCreated();
                    }
                }
            }

            else if (Configuration["DataSource"] == "spotify")
            {
                // sqlite
                using (var client = new WebzineDbContext())
                {

                    client.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                    if (Configuration["ResetDb"] == "true")
                    {
                        client.Database.EnsureDeleted();
                        client.Database.EnsureCreated();

                        SpotifySeeder spotify = new SpotifySeeder("1a3478c9f18842bc9fee8692aa574973", "943d3a231566443ba543d0e20a673d44");

                        var playlist = spotify.GetPlaylist(Configuration["SpotifyPlaylistId"]);
                        Seeder seed = spotify.SeedFromPlaylist(client, playlist);

                        client.SaveChanges();

                        var spotifyStyle = client.Styles.FirstOrDefault();
                        List<LienStyle> ls = new List<LienStyle>();
                        client.Titres.ToList().ForEach(t => ls.Add(new LienStyle { IdStyle = spotifyStyle.IdStyle, IdTitre = t.IdTitre }));

                        ls.ForEach(l => client.LienStyles.Add(l));

                        client.SaveChanges();
                    }

                    else
                    {
                        client.Database.EnsureCreated();
                    }
                }
            }
            else
            {
                // erreur mise de factory par default
                StaticFactory.UpdateBinavigabilite();
            }

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddScoped<ITitreBusiness, TitreBusiness>();
            if (Configuration["DataSource"] == "factory")
            {
                services.AddSingleton(typeof(IArtisteRepository), new LocalArtisteRepository());
                services.AddSingleton(typeof(ICommentaireRepository), new LocalCommentaireRepository());
                services.AddSingleton(typeof(IStyleRepository), new LocalStyleRepository());
                services.AddSingleton(typeof(ITitreRepository), new LocalTitreRepository());
            }
            else if (Configuration["DataSource"] == "spotify" || Configuration["DataSource"] == "local")
            {
                services.AddSingleton(typeof(IArtisteRepository), new DbArtisteRepository());
                services.AddSingleton(typeof(ICommentaireRepository), new DbCommentaireRepository());
                services.AddSingleton(typeof(IStyleRepository), new DbStyleRepository());
                services.AddSingleton(typeof(ITitreRepository), new DbTitreRepository());

                if (Configuration["DatabaseType"] == "sqlite")
                {
                    services.AddEntityFrameworkSqlite().AddDbContext<WebzineDbContext>().AddConnections();
                }
            }

            else
            {
                services.AddSingleton(typeof(IArtisteRepository), new LocalArtisteRepository());
                services.AddSingleton(typeof(ICommentaireRepository), new LocalCommentaireRepository());
                services.AddSingleton(typeof(IStyleRepository), new LocalStyleRepository());
                services.AddSingleton(typeof(ITitreRepository), new LocalTitreRepository());
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();


            app.UseEndpoints(endpoints =>
            {

                #region Administration

                //Route for page for admin panel
                //Route Adminsitration/Artistes/page/2
                endpoints.MapControllerRoute(
                    name: "mainPage",
                    pattern: "{area}/{controller=Artistes}/page/{id}",
                    defaults: new { action = "Navigate" }
                );

                List<string> controllers = new List<string> { "Styles", "Artistes", "Titres" };
                foreach (string item in controllers)
                {
                    //Route : Administration/controllers/Edit/ID ou Create
                    endpoints.MapControllerRoute(
                       name: "admin" + item + "Manage",
                       pattern: "{area}/" + item.Substring(0, item.Length - 1) + "/{fonction}/{id?}",
                       defaults: new { controller = item, action = "Manage" }
                   );


                    //Route : Administration/controllers/Delete/ID
                    endpoints.MapControllerRoute(
                        name: "admin" + item + "Delete",
                        pattern: "{area}/" + item.Substring(0, item.Length - 1) + "/{action=Delete}/{id}",
                        defaults: new { controller = item }
                    );
                }

                //Route : Administration/Commentaire/Delete/ID
                endpoints.MapControllerRoute(
                    name: "adminCommentairesDelete",
                    pattern: "{area}/Commentaire/{action=Delete}/{id}",
                    defaults: new { controller = "Commentaires" }
                );

                //Route : Administration/Contoller with s (Styles / Artistes/ Titres/ Commentaires)/Index ou Create
                endpoints.MapControllerRoute(
                    name: "areasWithoutParam",
                    pattern: "{area}/{controller=Home}/{action=Index}"
                );


                #endregion

                #region Global
                //Route controller/id/artist/titre
                endpoints.MapControllerRoute(
                    name: "titreFromArtist",
                    pattern: "{controller}/{id}/{artiste}/{titre}",
                    defaults: new { action = "titreFromArtist" }
                );

                //Route /titre/commenter
                endpoints.MapControllerRoute(
                  name: "commenter",
                  pattern: "/titre/commenter",
                  defaults: new { controller = "Titre", action = "Commentaire" }
              );

                //Route /titre/liker for like an track
                endpoints.MapControllerRoute(
                    name: "liker",
                    pattern: "/titre/liker",
                    defaults: new { controller = "Titre", action = "Like" }
                );

                //Route  /titres/Style/NomStyle/page/IDPage
                endpoints.MapControllerRoute(
                    name: "stylePage",
                    pattern: "/titres/{controller=Style}/{name}/page/{id}",
                    defaults: new { controller = "Style", action = "Navigate" }
                );

                //Route /titres/Style/NomStyle 
                endpoints.MapControllerRoute(
                    name: "style",
                    pattern: "/titres/{controller=Style}/{name}",
                    defaults: new { controller = "Style", action = "Index" }
                );

                //Route for page of Home
                //Route page/1
                endpoints.MapControllerRoute(
                    name: "mainPage",
                    pattern: "page/{id}",
                    defaults: new { controller = "Home", action = "Navigate" }
                );

                //Route Controller/Id (Artiste/PNL or Titre/1)
                //Id can be a string or int
                endpoints.MapControllerRoute(
                    name: "main",
                    pattern: "{controller}/{id}",
                    defaults: new { action = "Index" }
                );

                //Route Controller/Action (Titres / Artistes/ Styles)
                //Main route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );

                #endregion

            });
        }
    }
}
