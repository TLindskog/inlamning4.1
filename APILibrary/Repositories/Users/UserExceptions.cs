using System;

namespace UserApi.Repositories
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
        }
    }

    public class NonUniqueUserName : UserException
    {
        public NonUniqueUserName(string message = "The UserName was not unique.") : base(message)
        {
        }
    }

    public class NonUniqueId : UserException
    {
        public NonUniqueId(string message = "The Id was not unique.") : base(message)
        {
        }
    }

    public class NonValidUserName : UserException
    {
        public NonValidUserName(string message = "The UserName was not valid.") : base(message)
        {
        }
    }

    public class NonValidEmail : UserException
    {
        public NonValidEmail(string message = "The Email was not valid.") : base(message)
        {
        }
    }
}