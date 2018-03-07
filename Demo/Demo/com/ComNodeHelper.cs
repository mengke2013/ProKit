using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using log4net;
using Rocky.Core.Opc.Ua;
using Demo.com.entity;

namespace Demo.com
{
    class ComNodeHelper
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ComNodeHelper instance;

        public static ComNodeHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComNodeHelper();
                }
                return instance;
            }
        }

        public ComRecipeStepNodeComponent ReadRecipeStep(byte selectedTybe)
        {
            //read from socket
            return null;
        }

        private ComNodeHelper() { }
    }
}
