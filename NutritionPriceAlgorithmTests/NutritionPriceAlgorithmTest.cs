using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using NutritionPriceLib.Algorithm;
using NutritionPriceLib.Algorithm.IndexationRules;

namespace NutritionPriceAlgorithmTests
{
    public class NutritionPriceAlgorithmTest
    {
        [Test]
        public void Stock_Price_Full_Mounth_ComputeTest()
        {
            // Arrange
            var algorithm = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(new DateTime(2021, 05, 1), new DateTime(2021, 05, 31)), 200.0);
            // Act
            var result = algorithm.Compute();
            // Assert
            Assert.AreEqual(4200, result);
        }
        [Test]
        public void Stock_Price_Full_Mounth_With_Day_Rule_Compute_Test()
        {
            //   May 2021 (21 Working Day)
            // #############################
            // (200 * 13) + (300 * 8) = 5000 

            // Arrange
            var algorithm = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(1619816400, 1622408400), 200.0);
            algorithm.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300));
            // Act
            var result = algorithm.Compute();
            // Assert
            Assert.AreEqual(5000, result);
        }
        [Test]
        public void Stock_Price_Full_Mounth_With_Day_Rule_ComputeTable_Test()
        {
            //   May 2021 (21 Working Day)
            // #############################

            var expectedResult = new List<double>
            {
                                    //  0,  0,  
                200,200,200,200,200,//  0,  0,
                200,200,200,200,200,//  0,  0,
                200,200,200,300,300,//  0,  0,
                300,300,300,300,300,//  0,  0,
                300
            };

            // Arrange
            var algorithm = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(1619816400, 1622408400), 200.0);
            algorithm.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300));
            // Act
            var result = algorithm.ComputeTable();
            // Assert
            Assert.AreEqual(expectedResult, result.Values.ToList());
        }


        [Test]
        public void Stock_Price_Full_Ten_Years_Prefomance_ComputeTest()
        {
            // Arrange
            var algorithm = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(new DateTime(2011, 01, 1), new DateTime(2021, 12, 31)), 200.0);
            // Act
            var result = algorithm.Compute();
            // Assert
            Assert.AreEqual(574000, result);
        }

        [Test]
        public void Stock_Price_Full_Ten_Years_With_Day_Rule_Prefomance_ComputeTest()
        {
            // Arrange
            var algorithm = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(new DateTime(2011, 01, 1), new DateTime(2021, 12, 31)), 200.0);
            algorithm.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300));
            // Act
            var result = algorithm.Compute();
            // Assert
            Assert.AreEqual(681900, result);
        }

        [Test]
        public void TaskB_Test()
        {
            var data = NutritionPriceUtils.GenerateWorkingDays(new DateTime(2020, 04, 3), new DateTime(2020, 04, 5));
            data.AddRange(NutritionPriceUtils.GenerateWorkingDays(new DateTime(2020, 04, 13), new DateTime(2020, 04, 30)));
            // Arrange
            var algorithm = new NutritionPriceAlgorithm(data, 200.0);
            algorithm.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300));
            // Act
            var result = algorithm.Compute();
            // Assert
            Assert.AreEqual(3900, result);
        }
    }
}