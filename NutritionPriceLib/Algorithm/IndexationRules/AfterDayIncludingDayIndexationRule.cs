namespace NutritionPriceLib.Algorithm.IndexationRules
{
    public class AfterDayIncludingDayIndexationRule : IIndexationRule
    {
        private uint DayAfter;
        private double IndexPrice;

        public AfterDayIncludingDayIndexationRule(uint DayAfter, double IndexPrice)
        {
            if (DayAfter > 31)
                throw new System.Exception("Day cannot be greater than 31");

            this.DayAfter = DayAfter;
            this.IndexPrice = IndexPrice;
        }
        public bool Check(double CurrentTime)
        {
            return NutritionPriceUtils.UnixTimeStampToDateTime(CurrentTime).Day >= DayAfter;
        }

        public double GetPrice()
        {
            return IndexPrice;
        }
    }
}
