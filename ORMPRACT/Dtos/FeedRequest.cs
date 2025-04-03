using Microsoft.AspNetCore.Mvc;

namespace TravelGateway.DTO;

public class FeedRequest
{
    public int limit = 10;
    public int offset = 0;
}