using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Algebra.SecureCoding.Serializable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userData = new UserData { Name = "Ivor Marić", Oib = "111111111119" };
            var badData = new BadData { Name = "Muhahaha" };

            Serialize<UserData>(userData, "data.ser");
            Serialize<BadData>(badData, "data_bad.ser");

            var serializedData = ReceiveSerializedData("data.ser");
            var serializedDataBad = ReceiveSerializedData("data_bad.ser");

            Deserialize(serializedData);
            Deserialize(serializedDataBad);

            Console.ReadKey();
        }

        static void Serialize<T>(object data, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, data);
            }
        }

        static void Deserialize(byte[] serializedData)
        {
            using (MemoryStream ms = new MemoryStream(serializedData))
            {
                IFormatter formatter = new BinaryFormatter();
                dynamic deserializeObject = formatter.Deserialize(ms);
                Console.WriteLine(deserializeObject.Name);
            }
        }

        static byte[] ReceiveSerializedData(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
                
        }
    }



    [Serializable]
    public class UserData
    {
        public string Name { get; set; }
        public string Oib { get; set; }
    }

    [Serializable]
    public class BadData
    {
        public string Name { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            ExecuteMaliciousCode();
        }

        private void ExecuteMaliciousCode()
        {
            Console.WriteLine("Executing malicious code!");
        }
    }
}
