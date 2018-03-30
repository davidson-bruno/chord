namespace Transposer.Util
{
    public class DescriptionAttribute : System.Attribute
    {
        public string Symbol { get; }
        public string Name { get; }

        public DescriptionAttribute(string symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }
    }
}
