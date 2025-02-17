using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows;
using PBook_Model;
using PBook_UI.Viewmodels.Base;
using ReactiveUI;

namespace PBook_UI.Viewmodels;

public class EditContactViewModel : BaseConnectedObject
{
    private Book _contact;
    public Book Contact
    {
        get => _contact;
        set => this.RaiseAndSetIfChanged(ref _contact, value);
    }

    private PhoneType _selectedType;
    public ObservableCollection<PhoneType> PhoneTypes { get; set; }
    public PhoneType SelectedType
    {
        get => _selectedType;
        set => this.RaiseAndSetIfChanged(ref _selectedType, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public Window OwnerWindow { get; set; }

    public EditContactViewModel(Book contact, ObservableCollection<PhoneType> phoneTypes)
    {
        _contact = contact;
        SelectedType = _contact.Type;

        PhoneTypes = phoneTypes;
        SaveCommand = ReactiveCommand.Create(SaveChanges);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    private void SaveChanges()
    {
        if (Contact != null)
        {
            Contact.Type = SelectedType;
            CloseWindow(true);
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