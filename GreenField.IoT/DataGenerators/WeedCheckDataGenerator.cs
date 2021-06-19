using System;
using System.Collections.Generic;
using GreenField.IoT.Models;
using GreenField.IoT.Utils;

namespace GreenField.IoT.DataGenerators
{
    public class WeedCheckDataGenerator
    {
        public static WeedDetectedResult Generate(List<Weed> weeds)
        {
            if (weeds == null || weeds.Count == 0)
            {
                return null;
            }
            
            if (!DecisionHelper.ShouldDoAction(50))
            {
                return null;
            }

            var rand = new Random();
            var weedNumber = rand.Next(0, weeds.Count);
            var count = rand.Next(5, 100);

            return new WeedDetectedResult()
            {
                WeedId = weeds[weedNumber].Id,
                Percentage = count
            };
        }
    }
}