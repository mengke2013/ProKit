using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Design.entity;

namespace Design.service
{
    class TemperatureService
    {
        private static TemperatureService instance;
        private Furnace[] furnaces;
        private const byte furnaceCount = 6;

        public static TemperatureService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TemperatureService();
                }
                return instance;
            }
        }

        private TemperatureService()
        {
            furnaces = new Furnace[furnaceCount];
            for (int i = 0; i < furnaceCount; ++i)
            {
                furnaces[i] = new Furnace();
            }
        }

        public Furnace GetFurnace(byte index)
        {
            return furnaces[index];
        }
    }
}
