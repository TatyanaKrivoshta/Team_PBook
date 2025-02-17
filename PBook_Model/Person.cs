using ReactiveUI;

namespace PBook_Model;

public class Person : ReactiveObject
{
    private string? _firstName;
    private string? _lastName;
    private string? _patronymic;

    public int Id { get; set; }

    public string? FirstName
    {
        get => _firstName;
        set => this.RaiseAndSetIfChanged(ref _firstName, value);
    }

    public string? LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged(ref _lastName, value);
    }

    public string? Patronymic
    {
        get => _patronymic;
        set => this.RaiseAndSetIfChanged(ref _patronymic, value);
    }

    public string FullName => $"{LastName} {FirstName} {Patronymic}";
}