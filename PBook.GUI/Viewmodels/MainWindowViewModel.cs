using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows;
using PBook_Model;
using PBook.GUI.Viewmodels.Base;
using ReactiveUI;

namespace PBook.GUI.Viewmodels;

public class MainWindowViewModel : BaseConnectedObject
{
    private ObservableCollection<PhoneType> _phoneTypes;
    private Book _selectedContact;

    public Book SelectedContact
    {
        get => _selectedContact;
        set => this.RaiseAndSetIfChanged(ref _selectedContact, value);
    }

    public ObservableCollection<Book> Contacts => ContactManager.Instance.Contacts;


    public ObservableCollection<PhoneType> PhoneTypes
    {
        get => _phoneTypes;
        set => this.RaiseAndSetIfChanged(ref _phoneTypes, value);
    }

    public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public MainWindowViewModel()
    {
        InitializeAsync().ConfigureAwait(false);
        InitializePhoneTypes();
        CreateCommand = ReactiveCommand.Create(AddNewContact);
        DeleteCommand = ReactiveCommand.Create(DeleteSelectedContact);
        EditCommand = ReactiveCommand.Create(OnEdit);

    }

    private void InitializePhoneTypes()
    {
        _phoneTypes = new ObservableCollection<PhoneType>
        {
            new() { Id = (int)PhoneTypeEnum.work, Type = PhoneTypeEnum.work.ToString() },
            new() { Id = (int)PhoneTypeEnum.home, Type = PhoneTypeEnum.home.ToString() },
            new() { Id = (int)PhoneTypeEnum.mobile, Type = PhoneTypeEnum.mobile.ToString() }
        };
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
            Type = _phoneTypes[2],
            Number = "1234567890"
        };
        ContactManager.Instance.AddContact(newContact);
        Service.Dal_AddBook(newContact.FirstName, newContact.LastName, newContact.Patronymic, newContact.Type.Id,
            newContact.Number);
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
        var viewModel = new EditContactViewModel(SelectedContact, _phoneTypes);
        editWindow.DataContext = viewModel;
        viewModel.OwnerWindow = editWindow;

        if (editWindow.ShowDialog() == true)
        {
            Service.Dal_UpdateBook_Async(SelectedContact);
        }

        new MainWindow().Show();
        Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
    }
}