using System.Collections.ObjectModel;
using PBook_Client_DAL;
using PBook_Model;

namespace Team_PBook_wpf.Viewmodels.Bases;

public class BaseConnectedObject : ViewModelBase
{
    protected readonly ClientDAL Service = new();
    protected static ObservableCollection<Book> ContactsShare { get; } = [];

    protected static ObservableCollection<PhoneType> TypesShare { get; } =
    [
        new PhoneType { Id = 1, Type = PhoneTypeEnum.work },
        new PhoneType { Id = 2, Type = PhoneTypeEnum.home },
        new PhoneType { Id = 3, Type = PhoneTypeEnum.mobile }
    ];
}