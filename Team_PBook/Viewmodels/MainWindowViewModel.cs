using System.Collections.ObjectModel;
using System.Reactive;
using PBook_Model;
using ReactiveUI;
using Team_PBook_wpf.Viewmodels.Bases;
using Team_PBook.Viewmodels;

namespace Team_PBook_wpf.Viewmodels;

public class MainWindowViewModel : BaseConnectedObject
{
    private Book _selectedContact;
    private ObservableCollection<Book> _contacts;

    public ObservableCollection<Book> Contacts
    {
        get => _contacts;
        set => this.RaiseAndSetIfChanged(ref _contacts, value);
    }

    public Book SelectedContact
    {
        get => _selectedContact;
        set => this.RaiseAndSetIfChanged(ref _selectedContact, value);
    }

    public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public MainWindowViewModel()
    {
        Contacts = ContactsShare;
        CreateCommand = ReactiveCommand.CreateFromTask(async () => await OnCreate());
        DeleteCommand = ReactiveCommand.Create(DeleteSelectedContact);
        EditCommand = ReactiveCommand.Create(OnEdit);
    }

    private async Task OnCreate()
    {
        var createWindow = new CreateContactWindow();
        var viewmodel = new CreateContactViewModel();

        createWindow.DataContext = viewmodel;
        viewmodel.OwnerWindow = createWindow;
        bool? result = createWindow.ShowDialog();
        await Task.FromResult(result);
    }


    private void DeleteSelectedContact()
    {
        // TODO УДАЛЕНИЕ В СЕРВЕР
        if (SelectedContact != null)
        {
            Contacts.Remove(SelectedContact);
            SelectedContact = null;
        }
    }

    private void OnEdit()
    {
        if (SelectedContact == null) return;

        var editWindow = new EditContactInfoWindow();
        var viewModel = new EditContactViewModel(SelectedContact);

        editWindow.DataContext = viewModel;
        viewModel.OwnerWindow = editWindow;

        bool? result = editWindow.ShowDialog();

        if (result == true)
        {
            SelectedContact = Contacts.FirstOrDefault(c => c == SelectedContact);
        }
    }
}