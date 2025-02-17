using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows;
using PBook_Model;
using PBook_UI.Viewmodels.Base;
using ReactiveUI;

namespace PBook_UI.Viewmodels;

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
        InitializePhoneTypes();
        CreateCommand = ReactiveCommand.Create(AddNewContact);
        DeleteCommand = ReactiveCommand.Create(DeleteSelectedContact);
        EditCommand = ReactiveCommand.Create(OnEdit);

        //InitializeAsync().ConfigureAwait(false);
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
        // Пример загрузки данных из базы данных
        // var dbContacts = await Service.Dal_GetAllBooks_Async();
        // foreach (var contact in dbContacts)
        // {
        //     _contacts.Add(contact);
        // }
    }

    private void AddNewContact()
    {
        var newContact = new Book
        {
            Id = ContactManager.Instance.Contacts.Count + 1,
            Person = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Patronymic = "Smith"
            },
            Type = new PhoneType { Id = 1, Type = "Mobile" },
            Number = "1234567890"
        };

        ContactManager.Instance.AddContact(newContact);
    }

    private void DeleteSelectedContact()
    {
        if (SelectedContact == null) return;
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
            // _ = Service.Dal_UpdateBook(SelectedContact);
        }
        new MainWindow().Show();
        Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
    }
}