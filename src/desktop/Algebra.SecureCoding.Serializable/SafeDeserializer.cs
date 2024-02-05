using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Algebra.SecureCoding.Serializable
{
    public static class SafeDeserializer
    {
        private static readonly List<string> SafeTypes = new List<string>
        {
            typeof(UserData).AssemblyQualifiedName,
        };

        public static object Deserialize(byte[] serializedData)
        {
            using (MemoryStream ms = new MemoryStream(serializedData))
            {
                var formatter = new SafeBinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        public class SafeBinaryFormatter : IFormatter
        {
            public SerializationBinder Binder { get; set; }
            public StreamingContext Context { get; set; } = new StreamingContext(StreamingContextStates.All);
            public ISurrogateSelector SurrogateSelector { get; set; }

            public SafeBinaryFormatter()
            {
                this.Binder = new SafeSerializationBinder();
            }

            public object Deserialize(Stream serializationStream)
            {
                var binaryFormatter = new BinaryFormatter
                {
                    Binder = this.Binder,
                    Context = this.Context,
                    SurrogateSelector = this.SurrogateSelector
                };

                return binaryFormatter.Deserialize(serializationStream);
            }

            public void Serialize(Stream serializationStream, object graph)
            {
                //Dont need it
                throw new NotImplementedException("Serialization is not supported.");
            }
        }

        private class SafeSerializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                string assemblyQualifiedTypeName = $"{typeName}, {assemblyName}";

                if (SafeTypes.Contains(assemblyQualifiedTypeName))
                {
                    return Type.GetType(assemblyQualifiedTypeName);
                }
                else
                {
                    throw new SerializationException($"Attempt to deserialize unauthorized type: {typeName}. Deserialization aborted.");
                }
            }
        }
    }


}
