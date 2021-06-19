using System;

namespace GreenField.IoT.Utils
{
    public class DecisionHelper
    {
        public static bool ShouldDoAction(int possibility)
        {
            var random = new Random();
            var value =random.Next(0, 101);
            return value <= possibility;
        }
    }
}