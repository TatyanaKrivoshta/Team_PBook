using System.Reactive;
using System.Windows;
using ReactiveUI;

namespace Team_PBook.Viewmodels;

public class EditContactViewModel : ReactiveObject
{
    private Contact _contact;

    public Contact Contact
    {
        get => _contact;
        set => this.RaiseAndSetIfChanged(ref _contact, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public EditContactViewModel(Contact contact)
    {
        Contact = new Contact
        {
            LastName = contact.LastName,
            FirstName = contact.FirstName,
            MiddleName = contact.MiddleName,
            WorkPhone = contact.WorkPhone,
            HomePhone = contact.HomePhone
        };

        SaveCommand = ReactiveCommand.Create(SaveChanges);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    public EditContactViewModel()
    {
    }

    private void SaveChanges()
    {
        // Сохраняем изменения в оригинальный контакт
        if (Contact != null)
        {
            Contact.LastName = _contact.LastName;
            Contact.FirstName = _contact.FirstName;
            Contact.MiddleName = _contact.MiddleName;
            Contact.WorkPhone = _contact.WorkPhone;
            Contact.HomePhone = _contact.HomePhone;
        }

        CloseWindow(true);
    }

    private void Cancel()
    {
        CloseWindow(false);
    }

    private void CloseWindow(bool isSaved)
    {
        if (OwnerWindow != null)
        {
            OwnerWindow.DialogResult = isSaved;
            OwnerWindow.Close();
        }
    }

    public Window OwnerWindow { get; set; }
}