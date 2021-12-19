namespace AutoCaffee
{
    public class ColumnNameAttribute : System.Attribute
    {
        public string Name { get; set; }
        public ColumnNameAttribute(string Name) { this.Name = Name; }
    }
}
