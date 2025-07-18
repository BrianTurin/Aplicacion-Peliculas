using System;

namespace BBBBFLIX.Users
{
    public interface ICurrentUserService
    {
        Guid? GetCurrentUserId();
    }
}
