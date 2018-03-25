using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.model
{
    public class User
    {
        private string mName;

        public User(string name)
        {
            mName = name;
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
    }
}
