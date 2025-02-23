using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows;
using PBook_GUI.Viewmodels.Base;
using PBook_Model;
using ReactiveUI;

namespace PBook_GUI.Viewmodels;

public class MainWindowViewModel : BaseConnectedObject
{
    private Book _selectedContact;

    public Book SelectedContact
    {
        get => _selectedContact;
        set => this.RaiseAndSetIfChanged(ref _selectedContact, value);
    }

    public ObservableCollection<Book> Contacts => ContactManager.Instance.Contacts;

    public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public MainWindowViewModel()
    {
        ContactManager.Instance.ClearContacts();
        InitializeAsync().ConfigureAwait(false);
        CreateCommand = ReactiveCommand.Create(AddNewContact);
        DeleteCommand = ReactiveCommand.Create(DeleteSelectedContact);
        EditCommand = ReactiveCommand.Create(OnEdit);

    }
    
    private async Task InitializeAsync()
    {
        var dbContacts = await Service.Dal_GetAllBooks_Async();
        foreach (var contact in dbContacts)
        {
            ContactManager.Instance.AddContact(contact);
        }
    }

    private void AddNewContact()
    {
        var newContact = new Book
        {
            Id = ContactManager.Instance.Contacts.Count + 1,
            FirstName = "John",
            LastName = "Doe",
            Patronymic = "Smith",
            Type = PhoneTypeEnum.Mobile.ToString(),
            Number = "1234567890"
        };
        ContactManager.Instance.AddContact(newContact);
        Service.Dal_AddBook(newContact, 1);
    }

    private void DeleteSelectedContact()
    {
        if (SelectedContact == null) return;
        Service.Dal_DeleteBook(SelectedContact.Id);
        ContactManager.Instance.RemoveContact(SelectedContact);
        SelectedContact = null;
    }

    private void OnEdit()
    {
        if (SelectedContact == null) return;

        var editWindow = new EditContactWindow();
        var viewModel = new EditContactViewModel(SelectedContact);
        editWindow.DataContext = viewModel;
        viewModel.OwnerWindow = editWindow;

        if (editWindow.ShowDialog() == true)
        {
        }

        new MainWindow().Show();
        Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
    }
}