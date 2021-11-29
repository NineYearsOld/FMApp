using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class IDataReaderExtensions
    {
        public static string GetNullableString(this IDataReader reader, string attribute)
        {
            string g;
            if (reader.IsDBNull(reader.GetOrdinal(attribute)))
            {
                g = null;
            }
            else g = (string)reader[attribute];
            return g;
        }
        public static int? GetNullableInt(this IDataReader reader, string attribute)
        {
            int? p;
            if (reader.IsDBNull(reader.GetOrdinal(attribute)))
            {
                p = null;
            }
            else p = (int)reader[attribute];
            return p;
        }
    }
}
