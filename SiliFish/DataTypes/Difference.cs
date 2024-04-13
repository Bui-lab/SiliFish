namespace SiliFish.DataTypes
{
    public class Difference
    {
        public string Item;
        public string Parameter;
        public string Value1;
        public string Value2;

        public bool SingleString => string.IsNullOrEmpty(Parameter) && string.IsNullOrEmpty(Value1) && string.IsNullOrEmpty(Value2);
        public Difference(string item)
        {
            Item = item;
        }
        public Difference(string item, object val1, object val2)
        {
            Item = item;
            Value1 = val1?.ToString();
            Value2 = val2?.ToString();
        }

        public Difference(string item, string parameter, object val1, object val2)
        {
            Item = item;
            Parameter = parameter;
            Value1 = val1?.ToString();
            Value2 = val2?.ToString();
        }
    }
}
