namespace HTS.BusinessException;
using  Volo.Abp;

public class HTSBusinessException: BusinessException
{
    public HTSBusinessException(string errorCode)
        : base(errorCode)
    {
    }
}