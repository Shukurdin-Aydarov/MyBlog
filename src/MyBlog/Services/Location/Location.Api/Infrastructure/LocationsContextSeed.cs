using Location.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Location.Api.Infrastructure
{
    public class LocationsContextSeed
    {
        private readonly LocationsContext context;

        public LocationsContextSeed(IApplicationBuilder app)
        {
            var settings = app.ApplicationServices
                              .GetRequiredService<IOptions<LocationSettings>>();

            context = new LocationsContext(settings);
        }

        public async Task SeedAsync()
        {
            var collections = context.Locations
                                     .Database
                                     .GetCollection<Locations>(nameof(Locations));

            var exist = collections.AsQueryable().Any();

            if (exist) return;

            await SetIndexesAsync();
            await SetAsiaAsync();
            await SetNorthAmericaAsync();
            await SetBarcelonaLocationsAsync();
            await SetSouthAmericaAsync();
            await SetAfricaAsync();
            await SetEuropeAsync();
            await SetAustraliaAsync();
        }

        private async Task SetIndexesAsync()
        {
            var builder = Builders<Locations>.IndexKeys;
            var keys = builder.Geo2DSphere(x => x.Location);
            var model = new CreateIndexModel<Locations>(keys);
            await context.Locations.Indexes.CreateOneAsync(model);
        }

        private async Task SetNorthAmericaAsync()
        {
            var us = new Locations
            {
                LocationId = 1,
                Code = "NA",
                Description = "North America"
            };
            us.SetLocation(-103.219329, 48.803281);
            us.SetArea(GetNorthAmericaPoligon());
            await context.Locations.InsertOneAsync(us);
            await SetUSLocationsAsync(us.Id);
        }

        private async Task SetUSLocationsAsync(string parentId)
        {
            var us = new Locations
            {
                ParentId = parentId,
                Code = "US",
                Description = "United States",
                LocationId = 2
            };
            us.SetLocation(-101.357386, 41.650455);
            us.SetArea(GetUSPoligon());
            await context.Locations.InsertOneAsync(us);
            await SetWashingtonLocationsAsync(us.Id);
        }

        private async Task SetWashingtonLocationsAsync(string parentId)
        {
            var wht = new Locations
            {
                ParentId = parentId,
                Code = "WHT",
                Description = "Washington",
                LocationId = 3
            };
            wht.SetLocation(-119.542781, 47.223652);
            wht.SetArea(GetWashingtonPoligon());
            await context.Locations.InsertOneAsync(wht);
            await SetSeattleLocationsAsync(wht.Id);
            await SetRedmondLocationsAsync(wht.Id);
        }

        private async Task SetSeattleLocationsAsync(string parentId)
        {
            var stl = new Locations
            {
                ParentId = parentId,
                Code = "SEAT",
                Description = "Seattle",
                LocationId = 4
            };
            stl.SetArea(GetSeattlePoligon());
            stl.SetLocation(-122.330747, 47.603111);
            await context.Locations.InsertOneAsync(stl);
        }

        private async Task SetRedmondLocationsAsync(string parentId)
        {
            var rdm = new Locations
            {
                ParentId = parentId,
                Code = "REDM",
                Description = "Redmond",
                LocationId = 5
            };
            rdm.SetLocation(-122.122887, 47.674961);
            rdm.SetArea(GetRedmondPoligon());
            await context.Locations.InsertOneAsync(rdm);
        }

        private async Task SetBarcelonaLocationsAsync()
        {
            var bcn = new Locations
            {
                Code = "BCN",
                Description = "Barcelona",
                LocationId = 6
            };
            bcn.SetLocation(2.156453, 41.395226);
            bcn.SetArea(GetBarcelonaPoligon());
            await context.Locations.InsertOneAsync(bcn);
        }

        private async Task SetSouthAmericaAsync()
        {
            var sa = new Locations
            {
                Code = "SA",
                Description = "South America",
                LocationId = 7
            };
            sa.SetLocation(-60.328704, -16.809748);
            sa.SetArea(GetSouthAmericaPoligon());
            await context.Locations.InsertOneAsync(sa);
        }

        private async Task SetAfricaAsync()
        {
            var afc = new Locations
            {
                Code = "AFC",
                Description = "Africa",
                LocationId = 8
            };
            afc.SetLocation(19.475383, 13.063667);
            afc.SetArea(GetAfricaPoligon());
            await context.Locations.InsertOneAsync(afc);
        }

        private async Task SetEuropeAsync()
        {
            var eu = new Locations
            {
                Code = "EU",
                Description = "Europe",
                LocationId = 9
            };
            eu.SetLocation(13.147258, 49.947844);
            eu.SetArea(GetEuropePoligon());
            await context.Locations.InsertOneAsync(eu);
        }

        private async Task SetAsiaAsync()
        {
            var asa = new Locations
            {
                Code = "AS",
                Description = "Asia",
                LocationId = 10
            };
            asa.SetLocation(97.522257, 56.069107);
            asa.SetArea(GetAsiaPoligon());
            await context.Locations.InsertOneAsync(asa);
        }

        private async Task SetAustraliaAsync()
        {
            var aus = new Locations()
            {
                Code = "AUS",
                Description = "Australia",
                LocationId = 11
            };
            aus.SetLocation(133.733195, -25.010726);
            aus.SetArea(GetAustraliaPoligon());
            await context.Locations.InsertOneAsync(aus);
        }

        private GeoJson2DGeographicCoordinates[] GetSouthAmericaPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-91.43724, 13.29007),
                new GeoJson2DGeographicCoordinates(-87.96315, -27.15081),
                new GeoJson2DGeographicCoordinates(-78.75404, -50.71852),
                new GeoJson2DGeographicCoordinates(-59.14765, -58.50773),
                new GeoJson2DGeographicCoordinates(-50.08813, -42.22419),
                new GeoJson2DGeographicCoordinates(-37.21044, -22.56725),
                new GeoJson2DGeographicCoordinates(-36.61675, -0.38594),
                new GeoJson2DGeographicCoordinates(-44.46056, -16.6746),
                new GeoJson2DGeographicCoordinates(-91.43724, 13.29007)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetAfricaPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-12.68724, 34.05892),
                new GeoJson2DGeographicCoordinates(-18.33301, 20.77313),
                new GeoJson2DGeographicCoordinates(-14.13503, 6.21292),
                new GeoJson2DGeographicCoordinates(1.40221, -14.23693),
                new GeoJson2DGeographicCoordinates(22.41485, -35.55408),
                new GeoJson2DGeographicCoordinates(51.86499, -25.39831),
                new GeoJson2DGeographicCoordinates(53.49269, 4.59405),
                new GeoJson2DGeographicCoordinates(35.102, 26.14685),
                new GeoJson2DGeographicCoordinates(21.63319, 33.75767),
                new GeoJson2DGeographicCoordinates(6.58235, 37.05665),
                new GeoJson2DGeographicCoordinates(-12.68724, 34.05892)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetEuropePoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-11.73143, 35.27646),
                new GeoJson2DGeographicCoordinates(-10.84462, 35.25123),
                new GeoJson2DGeographicCoordinates(-10.09927, 35.26833),
                new GeoJson2DGeographicCoordinates(49.00838, 36.56984),
                new GeoJson2DGeographicCoordinates(36.63837, 71.69807),
                new GeoJson2DGeographicCoordinates(-10.88788, 61.13851),
                new GeoJson2DGeographicCoordinates(-11.73143, 35.27646)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetNorthAmericaPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-168.07786, 68.80277),
                new GeoJson2DGeographicCoordinates(-119.60378, 32.7561),
                new GeoJson2DGeographicCoordinates(-116.01966, 28.94642),
                new GeoJson2DGeographicCoordinates(-98.52265, 14.49378),
                new GeoJson2DGeographicCoordinates(-71.18188, 34.96278),
                new GeoJson2DGeographicCoordinates(-51.97606, 48.24377),
                new GeoJson2DGeographicCoordinates(-75.39806, 72.93141),
                new GeoJson2DGeographicCoordinates(-168.07786, 68.80277)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetAsiaPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(31.1592, 45.91629),
                new GeoJson2DGeographicCoordinates(32.046, 45.89479),
                new GeoJson2DGeographicCoordinates(62.32261, -4.45013),
                new GeoJson2DGeographicCoordinates(154.47713, 35.14525),
                new GeoJson2DGeographicCoordinates(-166.70788, 68.62211),
                new GeoJson2DGeographicCoordinates(70.38837, 75.89335),
                new GeoJson2DGeographicCoordinates(32.00274, 67.23428),
                new GeoJson2DGeographicCoordinates(31.1592, 45.91629)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetAustraliaPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(100.76857, -45.74117),
                new GeoJson2DGeographicCoordinates(101.65538, -45.76273),
                new GeoJson2DGeographicCoordinates(167.08823, -50.66317),
                new GeoJson2DGeographicCoordinates(174.16463, -34.62579),
                new GeoJson2DGeographicCoordinates(160.94837, -5.01004),
                new GeoJson2DGeographicCoordinates(139.29462, -7.86376),
                new GeoJson2DGeographicCoordinates(101.61212, -11.44654),
                new GeoJson2DGeographicCoordinates(100.76857, -45.74117)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetUSPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-62.88205, 48.7985),
                new GeoJson2DGeographicCoordinates(-129.3132, 48.76513),
                new GeoJson2DGeographicCoordinates(-120.9496, 30.12256),
                new GeoJson2DGeographicCoordinates(-111.3944, 30.87114),
                new GeoJson2DGeographicCoordinates(-78.11975, 24.24979),
                new GeoJson2DGeographicCoordinates(-62.88205, 48.7985)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetSeattlePoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-122.36238, 47.82929),
                new GeoJson2DGeographicCoordinates(-122.42091, 47.6337),
                new GeoJson2DGeographicCoordinates(-122.37371, 47.45224),
                new GeoJson2DGeographicCoordinates(-122.20788, 47.50259),
                new GeoJson2DGeographicCoordinates(-122.26934, 47.73644),
                new GeoJson2DGeographicCoordinates(-122.36238, 47.82929)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetRedmondPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-122.15432, 47.73148),
                new GeoJson2DGeographicCoordinates(-122.17673, 47.72559),
                new GeoJson2DGeographicCoordinates(-122.16904, 47.67851),
                new GeoJson2DGeographicCoordinates(-122.16136, 47.65036),
                new GeoJson2DGeographicCoordinates(-122.15604, 47.62746),
                new GeoJson2DGeographicCoordinates(-122.01562, 47.63463),
                new GeoJson2DGeographicCoordinates(-122.04961, 47.74244),
                new GeoJson2DGeographicCoordinates(-122.15432, 47.73148)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetWashingtonPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(-124.68633, 48.8943),
                new GeoJson2DGeographicCoordinates(-124.32962, 45.66613),
                new GeoJson2DGeographicCoordinates(-116.73824, 45.93384),
                new GeoJson2DGeographicCoordinates(-116.96912, 49.04282),
                new GeoJson2DGeographicCoordinates(-124.68633, 48.8943)
            };
        }

        private GeoJson2DGeographicCoordinates[] GetBarcelonaPoligon()
        {
            return new []
            {
                new GeoJson2DGeographicCoordinates(2.033879, 41.383858),
                new GeoJson2DGeographicCoordinates(2.113741, 41.419068),
                new GeoJson2DGeographicCoordinates(2.188778, 41.451153),
                new GeoJson2DGeographicCoordinates(2.235266, 41.418033),
                new GeoJson2DGeographicCoordinates(2.137101, 41.299536),
                new GeoJson2DGeographicCoordinates(2.033879, 41.383858)
            };
        }
    }
}
