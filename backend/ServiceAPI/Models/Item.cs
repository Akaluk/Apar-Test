namespace ServiceAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Reason { get; set; } = "";
        public string Status { get; set; } = "pending";
    }
}