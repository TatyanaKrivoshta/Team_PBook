using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive;
using System.Windows;
using PBook_Model;
using PBook_UI.Viewmodels.Base;
using ReactiveUI;

namespace PBook_UI.Viewmodels;

public class EditContactViewModel : BaseConnectedObject
{
    //TODO Пофикить баги с сохранением типов телефона и отображением в UI
    private Book _contact;

    public Book Contact
    {
        get => _contact;
        set => this.RaiseAndSetIfChanged(ref _contact, value);
    }
    
    private string _selectedType;

    public ObservableCollection<string> PhoneTypes { get; set; }

    public string SelectedType
    {
        get => _selectedType;
        set
        {
            _selectedType = value;
            OnPropertyChanged(nameof(SelectedType));
        }
    }
    
    public new event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public EditContactViewModel(Book contact)
    {
        _contact = contact;
        SelectedType = _contact.Type!.Type;
        
        PhoneTypes =
        [
            PhoneTypeEnum.work.ToString(),
            PhoneTypeEnum.home.ToString(),
            PhoneTypeEnum.mobile.ToString()
        ];

        SaveCommand = ReactiveCommand.Create(SaveChanges);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }
    

    private void SaveChanges()
    {
        if (Contact != null)
        {
            Contact.Person = _contact.Person;
            Contact.Type.Type = _selectedType;
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