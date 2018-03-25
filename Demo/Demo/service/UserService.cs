using Demo.model;

using log4net;
using System.Collections.Generic;

namespace Demo.service
{
    public class UserService
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static UserService instance;

        private UserService()
        {
          
  
        }

        public static UserService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserService();
                }
                return instance;
            }
        }

        public User GetLoginUser()
        {
            //save the historys records into database
            return new User("MengKe");
        }
    }
}

