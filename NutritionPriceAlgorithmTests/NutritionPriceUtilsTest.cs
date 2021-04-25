using NUnit.Framework;

using NutritionPriceLib.Algorithm;

namespace NutritionPriceAlgorithmTests
{
    public class NutritionPriceUtilsTest
    {

        [Test]
        public void Generate_Working_Days_Count_Test()
        {
            // 1619816400 -> 01.05.2021
            // 1622408400 -> 31.05.2021

            // Act
            var result = NutritionPriceUtils.GenerateWorkingDays(1619816400, 1622408400);
            // Assert
            Assert.AreEqual(21, result.Count);
        }
    }
}
