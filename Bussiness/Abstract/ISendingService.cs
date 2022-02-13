using Core.Entities.Concrete;
using Core.Utilities.Result;

namespace Business.Abstract
{
    public interface ISendingService
    {
        IResult SendEmail(EmailInfo emailInfo);
    }
}
