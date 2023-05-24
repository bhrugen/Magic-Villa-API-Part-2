using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IApiMessageRequestBuilder
    {
        HttpRequestMessage Build(APIRequest apiRequest);
    }
}
