namespace Checkout.CrossCutting.Core.Serializer
{
    public interface ISerializerFactory
    {
        ISerializer Create();
    }
}
