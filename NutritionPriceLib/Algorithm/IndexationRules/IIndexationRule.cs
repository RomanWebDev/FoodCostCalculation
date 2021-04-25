namespace NutritionPriceLib.Algorithm.IndexationRules
{
    public interface IIndexationRule
    {
        public bool Check(double CurrentTime);
        public double GetPrice();
    }
}
