using System.Reactive;
using System.Windows;
using PBook_Model;
using ReactiveUI;
using Team_PBook_wpf.Viewmodels.Bases;

namespace Team_PBook.Viewmodels;
//TODO ПРОБЛЕМА С ПОТОКАМИ + ДОБАВИТЬ ОТПРАВКУ НА СЕРВЕР

public class EditContactViewModel : BaseConnectedObject
{
    private Book _contact;

    public Book Contact
    {
        get => _contact;
        set => this.RaiseAndSetIfChanged(ref _contact, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public EditContactViewModel(Book contact)
    {
        Contact = new Book
        {
            Person = contact.Person,
            Type = contact.Type,
            Number = contact.Number
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
            Contact.Person = _contact.Person;
            Contact.Type = _contact.Type;
            Contact.Number = _contact.Number;
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