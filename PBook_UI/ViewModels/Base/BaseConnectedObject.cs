using PBook_Client_DAL;
using PBook_Model;

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
    public Book SelectedContact;

}