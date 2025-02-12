using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using PBook_Model;
using ReactiveUI;
using Team_PBook_wpf.Viewmodels.Bases;

namespace Team_PBook_wpf.Viewmodels;
//TODO ПРОБЛЕМА С ПОТОКАМИ + ДОБАВИТЬ ОТПРАВКУ НА СЕРВЕР
public class CreateContactViewModel : BaseConnectedObject
{
    private Person _person;
    private PhoneType _selectedPhoneType;
    private string _number;

    public ObservableCollection<PhoneType> PhoneTypes { get; set; }

    public Person Person
    {
        get => _person;
        set => this.RaiseAndSetIfChanged(ref _person, value);
    }

    public PhoneType SelectedPhoneType
    {
        get => _selectedPhoneType;
        set => this.RaiseAndSetIfChanged(ref _selectedPhoneType, value);
    }

    public string Number
    {
        get => _number;
        set => this.RaiseAndSetIfChanged(ref _number, value);
    }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public CreateContactViewModel()
    {
        PhoneTypes = TypesShare;
        Person = new Person();
        SelectedPhoneType = PhoneTypes[1]; // По умолчанию выбираем первый тип
        Number = "";

        SaveCommand = ReactiveCommand.Create(SaveContact);
        CancelCommand = ReactiveCommand.CreateFromTask(async () => await Cancel());
    }
    

    private void SaveContact()
    {
        if (string.IsNullOrWhiteSpace(Person.First_name) ||
            string.IsNullOrWhiteSpace(Person.Last_name) ||
            string.IsNullOrWhiteSpace(Number))
        {
            MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var newBook = new Book
        {
            Person = Person,
            Type = SelectedPhoneType,
            Number = Number
        };
        CloseWindow(true);
    }

    private async Task Cancel()
    {
        CloseWindow(false);
        await Task.CompletedTask;
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