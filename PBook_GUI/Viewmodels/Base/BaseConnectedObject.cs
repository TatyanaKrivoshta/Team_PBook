using PBook_Client_DAL;

namespace PBook_GUI.Viewmodels.Base;

public enum PhoneTypeEnum
{
    Mobile = 1,
    Work = 2,
    Home = 3
}

public class BaseConnectedObject : ViewModelBase
{
    protected readonly ClientDAL Service = new ClientDAL();
}