namespace GreenField.Common
{
    public class GreenFieldNotFoundException : GreenFieldException
    {
        public GreenFieldNotFoundException() : base("not_found")
        {
        }
        public GreenFieldNotFoundException(string message) : base("not_found", message)
        {
        }
    }
}