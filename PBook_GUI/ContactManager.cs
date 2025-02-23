using System.Collections.ObjectModel;
using PBook_Model;
using ReactiveUI;

namespace PBook_GUI;

public class ContactManager : ReactiveObject
{
    private static ObservableCollection<Book> _contacts;
    public static ContactManager Instance { get; } = new ContactManager();

    public ObservableCollection<Book> Contacts
    {
        get => _contacts;
        set => this.RaiseAndSetIfChanged(ref _contacts, value);
    }

    private ContactManager()
    {
        _contacts = [];
    }
    
    public void AddContact(Book contact)
    {
        _contacts.Add(contact);
    }

    public void RemoveContact(Book contact)
    {
        _contacts.Remove(contact);
    }

    public void ClearContacts()
    {
        _contacts.Clear();
        Contacts.Clear();
    }
}