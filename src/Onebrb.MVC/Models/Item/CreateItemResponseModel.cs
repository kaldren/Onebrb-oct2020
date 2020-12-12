namespace Onebrb.MVC.Models.Item
{
    public class CreateItemResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
    }
}
