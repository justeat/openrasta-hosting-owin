namespace OpenRastaAPIProject
{
    public class HeresSomeObject : IHeresSomeObject
    {
        public string GetValue()
        {
            return "some text returned from IoC object";
        }
    }
}