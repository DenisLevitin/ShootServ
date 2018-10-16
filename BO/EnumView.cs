namespace BO
{
    public class EnumView
    {
        public int Id { get; private set; }
        
        public string EnumValue { get; private set; }
        
        public string EnumDescription { get; private set; }

        public EnumView(int id, string enumValue, string enumDescription)
        {
            Id = id;
            EnumValue = enumValue;
            enumDescription = enumDescription;
        }
    }
}