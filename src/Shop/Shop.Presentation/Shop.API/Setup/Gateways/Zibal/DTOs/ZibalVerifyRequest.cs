namespace Shop.API.Setup.Gateways.Zibal.DTOs;

public class ZibalVerifyRequest
{
    public string Merchant { get; private set; }
    public long TrackId { get; private set; }

    public ZibalVerifyRequest(long trackId, string merchant)
    {
        TrackId = trackId;
        Merchant = merchant;
    }
}