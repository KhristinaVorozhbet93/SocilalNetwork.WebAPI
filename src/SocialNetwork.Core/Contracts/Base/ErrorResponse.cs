using System.Net;

namespace SocialNetwork.Core.Contracts.Base
{
    public record ErrorResponse(string Message, int? HttpStatusCode = null);
}
