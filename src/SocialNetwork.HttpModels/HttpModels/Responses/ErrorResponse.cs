using System.Net;

namespace SocialNetwork.HttpModels.HttpModels.Responses
{
    public record ErrorResponse(string Message, HttpStatusCode? HttpStatusCode = null);
}
