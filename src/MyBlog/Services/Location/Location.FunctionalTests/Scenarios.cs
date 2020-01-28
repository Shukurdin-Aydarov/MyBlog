using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

using Location.Api.Models;
using Location.FunctionalTests.Middleware;

namespace Location.FunctionalTests
{
    public class Scenarios
    {
        [Fact]
        public async Task SetNewUserSeattleLocation_ResponseOkStatusCode()
        {
            await SetNewUserLocationAsync(-122.315752, 47.604610, "SEAT");
        }
        
        [Fact]
        public async Task SetNewUserReadmondLocation_ResponseOkStatusCode()
        {
            await SetNewUserLocationAsync(-122.119998, 47.690876, "REDM");
        }

        [Fact]
        public async Task SetNewUserWashingtonLocation_ResponseOkStatusCode()
        {
            await SetNewUserLocationAsync(-121.040360, 48.091631, "WHT");
        }

        [Fact]
        public async Task GetAllLocations_ResponseOkStatusCode()
        {
            using(var server = TestsFixture.CreatrServer())
            {
                var response = await server.CreateClient()
                                           .GetAsync(Api.Get.Locations);

                var locations = await DeserializeAsync<List<Locations>>(response);

                Assert.NotEmpty(locations);
            }
        }

        private static async Task SetNewUserLocationAsync(double longitude, double latitude, string expectedCode)
        {
            using (var server = TestsFixture.CreatrServer())
            {
                var content = BuildRequestContent(longitude, latitude);
                var client = server.CreateClient();
                var response = await client.PostAsync(Api.Post.AddNewLocation, content);

                var userLocationByIdUrl = Api.Get.UserLocationBy(AuthorizeMiddleware.TestUserId);
                var userLocationResponse = await client.GetAsync(userLocationByIdUrl);
                var userLocation = await DeserializeAsync<UserLocation>(userLocationResponse);

                var locationByIdUrl = Api.Get.LocationBy(userLocation.LocationId);
                var locationResponse = await client.GetAsync(locationByIdUrl);
                var location = await DeserializeAsync<Locations>(locationResponse);

                Assert.Equal(expectedCode, location.Code);
            }
        }

        private static StringContent BuildRequestContent(double longitude, double latitude)
        {
            var location = new Point(longitude, latitude);
            var json = JsonConvert.SerializeObject(location);

            return new StringContent(json, Encoding.UTF8, mediaType: "application/json");
        }

        private static async Task<T> DeserializeAsync<T>(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
