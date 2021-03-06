namespace Readible.Requests
{
    public class BookRequest
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Rating { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
