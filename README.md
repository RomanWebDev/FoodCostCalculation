# NutritionPriceAlgorithm

A C# Library implementation of algorithm for calculating nutrition. 
You can add your own indexing rules implementing IIndexationRule.

# Usage

Create an instance of algorithm 

Input:

List\<UnixTimestamp> WorkedDaysUnix - List of working days in Unix Time Stamp Format (Alternative List\<double>)

double StockPrice - Starting price

```
var algorithm = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(new DateTime(2021, 05, 1), new DateTime(2021, 05, 31)), 200.0);
```
Or you can provide date in UnixTimeStamp format
```
var algorithm = new NutritionPriceAlgorithm(NutritionPriceUtils.GenerateWorkingDays(1619816400, 1622408400), 200.0);
```

When you can add rule:

```
algorithm.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300));
``` 

And compute price:

```
algorithm.Compute();
``` 

Table Representation:

```
algorithm.ComputeTable();
``` 

See example task realisation in FoodCostCalculation project.

Also see different usage in unit test project.