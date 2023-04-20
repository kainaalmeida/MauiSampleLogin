using Flurl;
using Flurl.Http;
using MauiSampleLogin.Helper;
using MauiSampleLogin.Models.Restaurant;
using MonkeyCache.LiteDB;
using Newtonsoft.Json;

namespace MauiSampleLogin.Services.Restaurants
{
    public class RestaurantService : IRestaurantService
    {
        public async Task<IList<RestaurantResponse>> GetAllAsync(string token)
        {
            string _key = "restaurants";

            try
            {
                if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                    return Barrel.Current.Get<IList<RestaurantResponse>>(key: _key);

                if (!Barrel.Current.IsExpired(key: _key))
                    return Barrel.Current.Get<IList<RestaurantResponse>>(key: _key);

                var response = await Constants.BASE_URL
                    .AppendPathSegment("/restaurants")
                    .WithOAuthBearerToken(token)
                    .GetAsync();

                if (response.ResponseMessage.IsSuccessStatusCode)
                {
                    var content = await response.ResponseMessage
                        .Content
                        .ReadAsStringAsync();

                    var restaurants = JsonConvert
                        .DeserializeObject<IList<RestaurantResponse>>(content);

                    Barrel.Current.Add(key: _key, data: restaurants, expireIn: TimeSpan.FromHours(1));

                    return restaurants;
                }
            }
            catch (FlurlHttpException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<RestaurantResponse>();
        }
    }
}
