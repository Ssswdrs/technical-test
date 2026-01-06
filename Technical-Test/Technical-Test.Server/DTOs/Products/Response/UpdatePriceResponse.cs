public class UpdatePriceResponse
{
    public int TotalRequested { get; set; }
    public int TotalUpdated { get; set; }
    public List<int> NotFoundProductIds { get; set; } = new();
}
