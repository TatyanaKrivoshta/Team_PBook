using System.Collections.ObjectModel;
using PBook_Model;
using ReactiveUI;

namespace PBook_UI;

public class ContactManager : ReactiveObject
{
    private ObservableCollection<Book> _contacts;
    public static ContactManager Instance { get; } = new ContactManager();

    public ObservableCollection<Book> Contacts
    {
        get => _contacts;
        set => this.RaiseAndSetIfChanged(ref _contacts, value);
    }

    private ContactManager()
    {
        Contacts = [];
    }

    public void AddContact(Book contact)
    {
        Contacts.Add(contact);
    }

    public void RemoveContact(Book contact)
    {
        Contacts.Remove(contact);
    }
}
