using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using Team_PBook_wpf;

namespace Team_PBook.Viewmodels;

public class MainWindowViewModel : ReactiveObject
{
    private ObservableCollection<Contact> _contacts;
    private Contact _selectedContact;

    public ObservableCollection<Contact> Contacts
    {
        get => _contacts;
        set => this.RaiseAndSetIfChanged(ref _contacts, value);
    }

    public Contact SelectedContact
    {
        get => _selectedContact;
        set => this.RaiseAndSetIfChanged(ref _selectedContact, value);
    }

    public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public MainWindowViewModel()
    {
        Contacts = new ObservableCollection<Contact>();
        SelectedContact = null;

        CreateCommand = ReactiveCommand.Create(AddNewContact);
        DeleteCommand = ReactiveCommand.Create(DeleteSelectedContact);
        EditCommand = ReactiveCommand.Create(OnEdit);
    }

    private void AddNewContact()
    {
        var newContact = new Contact
        {
            LastName = "Новая",
            FirstName = "Запись",
            MiddleName = "",
            WorkPhone = "",
            HomePhone = ""
        };
        Contacts.Add(newContact);
        SelectedContact = newContact;
    }

    private void DeleteSelectedContact()
    {
        if (SelectedContact != null)
        {
            Contacts.Remove(SelectedContact);
            SelectedContact = null;
        }
    }

    private void OnEdit()
    {
        if (SelectedContact == null) return;

        var editWindow = new EditContactInfoWindow();
        var viewModel = new EditContactViewModel(SelectedContact);

        editWindow.DataContext = viewModel;
        viewModel.OwnerWindow = editWindow;

        bool? result = editWindow.ShowDialog();

        if (result == true)
        {
            SelectedContact = Contacts.FirstOrDefault(c => c == SelectedContact);
        }    }
}
