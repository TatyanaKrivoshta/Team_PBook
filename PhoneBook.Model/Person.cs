namespace PhoneBook.Model;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public List<Phone> Phones { get; set; } = [];
    public bool IsDeleted { get; set; } = false;
}