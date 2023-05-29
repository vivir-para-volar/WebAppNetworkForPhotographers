namespace UserClientAppNetworkForPhotographers.API.ApiRequests
{
    public class ApiPhotos
    {
        public static async Task<Stream> GetPhotoByName(int contentId, string name, string token)
        {
            var response = await ApiRequest.Get($"{ApiUrl.Photos}/{contentId}/{name}", token);
            return await response.Content.ReadAsStreamAsync();
        }
    }
}
