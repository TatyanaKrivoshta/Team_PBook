using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows;
using PBook_GUI.Viewmodels.Base;
using PBook_Model;
using ReactiveUI;

namespace PBook_GUI.Viewmodels;

public class EditContactViewModel : BaseConnectedObject
{
    private Book _contact;

    public Book Contact
    {
        get => _contact;
        set => this.RaiseAndSetIfChanged(ref _contact, value);
    }

    public ObservableCollection<string> PhoneTypes { get; set; }

    private string _selectedType;

    public string SelectedType
    {
        get => _selectedType;
        set => this.RaiseAndSetIfChanged(ref _selectedType, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public Window OwnerWindow { get; set; }

    public EditContactViewModel(Book contact)
    {
        _contact = contact;
        PhoneTypes =
        [
            "mobile",
            "work",
            "home"
        ];
        SelectedType = PhoneTypes.FirstOrDefault(x => x == _contact.Type);

        SaveCommand = ReactiveCommand.Create(SaveChanges);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    private void SaveChanges()
    {
        if (Contact != null)
        {
            switch (SelectedType)
            {
                case "mobile":
                    Contact.Type = SelectedType;
                    Service.Dal_UpdateBook_Async(Contact, 1);
                    CloseWindow(true);
                    break;
                case "work":
                    Contact.Type = SelectedType;
                    Service.Dal_UpdateBook_Async(Contact, 2);
                    CloseWindow(true);
                    break;
                case "home":
                    Contact.Type = SelectedType;
                    Service.Dal_UpdateBook_Async(Contact, 3);
                    CloseWindow(true);
                    break;
                default:
                    Contact.Number = string.Empty;
                    break;
            }
        }
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
}