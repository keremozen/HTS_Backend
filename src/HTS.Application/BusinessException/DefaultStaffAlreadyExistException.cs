namespace HTS.BusinessException;
using  Volo.Abp;

public class DefaultStaffAlreadyExistException: BusinessException
{
    public DefaultStaffAlreadyExistException()
        : base(ErrorCode.DefaultStaffAlreadyExist)
    {
    }
}