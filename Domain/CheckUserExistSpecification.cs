using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class CheckUserExistSpecification: GenericSpecification<User>
    {
        public CheckUserExistSpecification(User newuser)
        {
            Expression = user => user.UserName == newuser.UserName &&
                user.FirstName == newuser.FirstName && user.LastName == newuser.LastName;
        }
    }
}
