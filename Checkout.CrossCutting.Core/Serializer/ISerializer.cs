namespace Checkout.CrossCutting.Core.Serializer
{
    public interface ISerializer
    {
        string XmlSerialize(object objectToSerialize);
        T XmlDeserialize<T>(string xml);
    }
}
