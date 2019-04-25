using Checkout.CrossCutting.Core.Serializer;

namespace Checkout.CrossCutting.Framework.Serializer
{
    public class DotNetSerializerFactory : ISerializerFactory
    {
        ISerializer ISerializerFactory.Create()
        {
            return new Serializer();
        }
    }
}
