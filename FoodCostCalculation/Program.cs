using System;

using NutritionPriceLib.Algorithm;
using NutritionPriceLib.Algorithm.IndexationRules;

namespace FoodCostCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            // See working examples in tests

            //Task A

            var algorithmA = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(1619816400, 1622408400), 200.0);

            algorithmA.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300.0));

            Console.WriteLine($"Result for TASK A:{algorithmA.Compute()}");

            //Task B

            var data = NutritionPriceUtils.GenerateWorkingDays(new DateTime(2020, 04, 3), new DateTime(2020, 04, 5));
            data.AddRange(NutritionPriceUtils.GenerateWorkingDays(new DateTime(2020, 04, 13), new DateTime(2020, 04, 30)));

            var algorithmB = new NutritionPriceAlgorithm(data, 200.0);

            algorithmB.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300.0));

            Console.WriteLine($"Result for TASK B:{algorithmB.Compute()}");
        }
    }
}
