using ReactiveUI;

namespace Team_PBook;

public class Contact : ReactiveObject
{
    private string _lastName;
    private string _firstName;
    private string _middleName;
    private string _workPhone;
    private string _homePhone;

    public string LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged(ref _lastName, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => this.RaiseAndSetIfChanged(ref _firstName, value);
    }

    public string MiddleName
    {
        get => _middleName;
        set => this.RaiseAndSetIfChanged(ref _middleName, value);
    }

    public string WorkPhone
    {
        get => _workPhone;
        set => this.RaiseAndSetIfChanged(ref _workPhone, value);
    }

    public string HomePhone
    {
        get => _homePhone;
        set => this.RaiseAndSetIfChanged(ref _homePhone, value);
    }

    public string FullName => $"{LastName} {FirstName} {MiddleName}";
}