using BBBBFLIX.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBBFLIX.EntityFrameworkCore
{
    public class GetNewCurrentUserService : ICurrentUserService
    {
        public Guid? GetCurrentUserId()
        {
            return Guid.NewGuid();
        }
    }
}
