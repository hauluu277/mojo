using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace mojoportal.CoreHelpers
{
    public class MapDataHelper<T, V> where T : class where V:class
    {
        public static T MapData(V source)
        {
            var result = (T)Activator.CreateInstance(typeof(T));
            var obj = source.GetType().GetProperties();
            var outType = result.GetType();
            foreach (PropertyInfo info in obj)
            {
                PropertyInfo outfo = ((info != null) && info.CanRead)
               ? outType.GetProperty(info.Name, info.PropertyType)
               : null;
                if (outfo != null && outfo.CanWrite && outfo.PropertyType.Equals(info.PropertyType))
                {
                    var value = info.GetValue(source);
                    if (value != null)
                    {
                        var firstData = result.GetType().GetProperties().Where(x => x.Name == info.Name).FirstOrDefault();
                        if (firstData != null && firstData.CanWrite)
                        {
                            firstData.SetValue(result, value, null);
                        }
                    }
                }
            }
            return result;
        }

        public static List<T> MapDataList(List<V> source)
        {
            var result = new List<T>();
            foreach (var item in source)
            {
                result.Add(MapData(item));
            }

            return result;
        }

        public static void SetProperties<TIn, TOut>(TIn input, TOut output, ICollection<string> includedProperties)
        where TIn : class
        where TOut : class
        {
            if ((input == null) || (output == null)) return;
            Type inType = input.GetType();
            Type outType = output.GetType();
            foreach (PropertyInfo info in inType.GetProperties())
            {
                PropertyInfo outfo = ((info != null) && info.CanRead)
                    ? outType.GetProperty(info.Name, info.PropertyType)
                    : null;
                if (outfo != null && outfo.CanWrite
                    && (outfo.PropertyType.Equals(info.PropertyType)))
                {
                    if ((includedProperties != null) && includedProperties.Contains(info.Name))
                        outfo.SetValue(output, info.GetValue(input, null), null);
                    else if (includedProperties == null)
                        outfo.SetValue(output, info.GetValue(input, null), null);
                }
            }
        }
    }
}
