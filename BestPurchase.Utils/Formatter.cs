using BestPurchase.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BestPurchase.Utils
{
    public class Formatter
    {
        public static byte[] Serialize<T>(T entity) where T : EntityBase
        {

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, entity);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Position = 0;
            return ms.ToArray();
        }
    }
}
