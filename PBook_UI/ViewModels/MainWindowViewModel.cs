using System.Collections.ObjectModel;
using System.Reactive;
using PBook_Model;
using PBook_UI.Viewmodels.Base;
using ReactiveUI;

namespace PBook_UI.Viewmodels;

public class MainWindowViewModel : BaseConnectedObject
{
    
    private ObservableCollection<PhoneType> _phoneTypes;
    public ObservableCollection<Book> Contacts { get; set; } = [];

    private ObservableCollection<PhoneType> PhoneTypes
    {
        get => _phoneTypes;
        set => this.RaiseAndSetIfChanged(ref _phoneTypes, value);
    }

    public Book SelectedContactModel
    {
        get => SelectedContact;
        set => this.RaiseAndSetIfChanged(ref SelectedContact, value);
    }
    
    public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public MainWindowViewModel()
    {
        //Contacts = GetContacts(Service.Dal_GetAllBooks_Async().Result);
        PhoneTypes =
        [
            new PhoneType { Id = 1, Type = PhoneTypeEnum.home.ToString() },
            new PhoneType { Id = 2, Type = PhoneTypeEnum.mobile.ToString() },
            new PhoneType { Id = 3, Type = PhoneTypeEnum.work.ToString() }
        ];
        CreateCommand = ReactiveCommand.Create(AddNewContact);
        DeleteCommand = ReactiveCommand.Create(DeleteSelectedContact);
        EditCommand = ReactiveCommand.Create(OnEdit);
    }

    private void AddNewContact()
    {
        var newContact = new Book
        {
            Id = Contacts.Count + 1,
            Person = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Patronymic = "John"
            },
            Type = PhoneTypes[1],
            Number = "123",
        };
       // _ = Service.Dal_AddBook(newContact);
        Contacts.Add(newContact);
        SelectedContact = newContact;
    }

    private void DeleteSelectedContact()
    {
        if (SelectedContact == null) return;
        //Service.Dal_DeleteBook(SelectedContact.Id);
        Contacts.Remove(SelectedContact);
        SelectedContact = null;
    }

    private ObservableCollection<Book> GetContacts(IEnumerable<Book> dbContacts)
    {
        var temp = new ObservableCollection<Book>();
        foreach (var contact in dbContacts)
        {
            temp.Add(contact);
        }
        return temp;
    }
    
    private void OnEdit()
    {
        if (SelectedContact == null) return;

        var editWindow = new EditContactWindow();
        var viewModel = new EditContactViewModel(SelectedContact);

        editWindow.DataContext = viewModel;
        viewModel.OwnerWindow = editWindow;

        bool? result = editWindow.ShowDialog();

        if (result == true)
        {
            SelectedContact = Contacts.FirstOrDefault(c => c.Id == SelectedContact.Id);
        }
        
    }
}