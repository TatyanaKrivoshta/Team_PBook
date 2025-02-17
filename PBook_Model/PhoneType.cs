using ReactiveUI;

namespace PBook_Model;

public class PhoneType : ReactiveObject
{
    public int Id { get; set; }
    public string Type { get; set; }
}