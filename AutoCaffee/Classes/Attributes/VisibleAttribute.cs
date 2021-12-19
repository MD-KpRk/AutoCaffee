namespace AutoCaffee
{
    public class VisibleAttribute : System.Attribute
    {
        public bool visible { get; set; }
        public VisibleAttribute(bool visible) { this.visible = visible; }
    }
}
