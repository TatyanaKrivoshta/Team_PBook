namespace PBook_Model;

public enum PhoneTypeEnum
{
    work = 1,
    home = 2,
    mobile = 3
}

public class PhoneType
{
    public int Id { get; set; }
    public PhoneTypeEnum Type { get; set; }

    public string GetTypeString()
    {
        return Type.ToString();
    }
}