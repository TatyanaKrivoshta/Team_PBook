using ReactiveUI;

namespace PBook_Model;
public class Book : ReactiveObject
{
    private Person _person;
    private PhoneType _type;
    private string _number;

    public int Id { get; set; }

    public Person Person
    {
        get => _person;
        set => this.RaiseAndSetIfChanged(ref _person, value);
    }

    public PhoneType Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    public string Number
    {
        get => _number;
        set => this.RaiseAndSetIfChanged(ref _number, value);
    }
}