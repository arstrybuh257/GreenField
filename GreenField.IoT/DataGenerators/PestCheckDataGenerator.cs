using System;
using System.Collections.Generic;
using GreenField.IoT.Models;
using GreenField.IoT.Utils;

namespace GreenField.IoT.DataGenerators
{
    public class PestCheckDataGenerator
    {
        public static PestDetectedResult Generate(List<Pest> pests)
        {
            if (pests == null || pests.Count == 0)
            {
                return null;
            }
            
            if (!DecisionHelper.ShouldDoAction(50))
            {
                return null;
            }

            var rand = new Random();
            var pestNumber = rand.Next(0, pests.Count);
            var count = rand.Next(10, 200);

            return new PestDetectedResult()
            {
                PestId = pests[pestNumber].Id,
                CountOnSquareMeter = count
            };
        }
    }
}