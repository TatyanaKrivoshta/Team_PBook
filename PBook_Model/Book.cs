namespace PBook_Model
{
    public class Book
    {
        public int Id { get; set; }
        public required Person Person  { get; set; }
        public required PhoneType? Type { get; set; } 
        public required string? Number { get; set; }
    }
}
