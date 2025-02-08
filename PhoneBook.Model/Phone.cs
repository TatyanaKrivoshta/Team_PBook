namespace PhoneBook.Model;

public enum PhoneType
{
    Home, 
    Work,
    NoType
}

public class Phone
{
    public int Id { get; set; }
    public PhoneType Type { get; set; } = PhoneType.NoType;
    public string Number { get; set; }
    
    public int PersonId { get; set; }
    public  Person? Person { get; set; }
}