using System.Collections.ObjectModel;
using PBook_Client_DAL;
using PBook_Model;
using ReactiveUI;

namespace PBook_UI.Viewmodels.Base;

public enum PhoneTypeEnum
{
    work = 1,
    home = 2,
    mobile = 3
}

public class BaseConnectedObject : ViewModelBase
{
    protected readonly ClientDAL Service = new ClientDAL();
}