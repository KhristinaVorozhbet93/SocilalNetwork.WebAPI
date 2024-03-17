using System.Net;

namespace SocilalNetwork.WebAPI.HttpModels.Responses
{
    public record ErrorResponse(string Message, HttpStatusCode? HttpStatusCode = null);
}
