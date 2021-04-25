using System;
using System.Collections.Generic;

using NutritionPriceLib.Algorithm.IndexationRules;

namespace NutritionPriceLib.Algorithm
{
    using UnixTimestamp = Double;

    /// <summary>
    /// This class performs the power calculation algorithm.
    /// You can add your own indexation rule by implementing interface IIndexationRule
    ///    <example>For example:
    ///         <code>
    ///             List<double> days = NutritionPriceUtils.GenerateWorkingDays(new DateTime(2021, 05, 1), new DateTime(2021, 05, 31));
    ///             var algorithm = new NutritionPriceAlgorithm(days, 200.0);
    ///             algorithm.AddIndexationRule(new AfterDayIncludingDayIndexationRule(20, 300));
    ///             var result = algorithm.Compute();
    ///         </code>
    ///     </example>
    /// </summary>
    public class NutritionPriceAlgorithm
    {
        /// <summary>
        ///     List of working days represented by UnixTimestamp
        /// </summary>
        private List<UnixTimestamp> WorkedDaysUnix;
        /// <summary>
        ///     List of IndexationRules
        /// </summary>
        private List<IIndexationRule> IndexationRules;
        /// <summary>
        ///     Stock price without indexing
        /// </summary>
        private double StockPrice;

        /// <summary>
        ///     In: List<UnixTimestamp> WorkedDaysUnix - List of double represent unix time
        ///     In: double StockPrice - Start Price
        /// </summary>
        public NutritionPriceAlgorithm(List<UnixTimestamp> WorkedDaysUnix, double StockPrice)
        {
            this.WorkedDaysUnix = WorkedDaysUnix;
            this.StockPrice = StockPrice;

            IndexationRules = new List<IIndexationRule>();
        }

        /// <summary>
        ///     In: IIndexationRule Rule - Abstract rule for price calculation (Implement two methods Check(double UnixTimestamp), GetPrice())
        /// </summary>
        public void AddIndexationRule(IIndexationRule Rule)
        {
            IndexationRules.Add(Rule);
        }

        /// <summary>
        ///     Compute final result with added rules
        /// </summary>
        public double Compute()
        {
            double result = 0.0f;

            foreach (var Day in WorkedDaysUnix)
            {
                bool RuleSet = false;

                foreach (var Rule in IndexationRules)
                {
                    RuleSet = Rule.Check(Day);
                    result += RuleSet ? Rule.GetPrice() : 0.0f;
                }

                result += !RuleSet ? StockPrice : 0.0f;
            }

            return result;
        }
        /// <summary>
        ///     Compute final result with added rules. Return Table (Key - Working day Timestamp, Value - Price)
        /// </summary>
        public Dictionary<UnixTimestamp, double> ComputeTable()
        {
            var result = new Dictionary<UnixTimestamp, double>();

            foreach (var Day in WorkedDaysUnix)
            {
                bool RuleSet = false;

                foreach (var Rule in IndexationRules)
                {
                    RuleSet = Rule.Check(Day);
                    if (RuleSet)
                    {
                        result.Add(Day, Rule.GetPrice());
                    }
                }
                if (!RuleSet)
                    result.Add(Day, StockPrice);
            }

            return result;
        }
    }
}
