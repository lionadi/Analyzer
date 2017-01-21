using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MongoDB.Bson;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace Analyzer.Common
{
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }


    public static class Extensions
    {


        //public static void Shuffle<T>(this IList<T> list)
        //{
        //    RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        //    int n = list.Count;
        //    while (n > 1)
        //    {
        //        byte[] box = new byte[1];
        //        do provider.GetBytes(box);
        //        while (!(box[0] < n * (Byte.MaxValue / n)));
        //        int k = (box[0] % n);
        //        n--;
        //        T value = list[k];
        //        list[k] = list[n];
        //        list[n] = value;
        //    }
        //}

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        public static T GetRandom<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                return list[k];
            }

            return list.FirstOrDefault();
        }

        public static DateTime ParseDateTime(this String dateTime)
        {
            string format, pattern, tz;
            DateTime result = DateTime.MinValue;
            CultureInfo provider = CultureInfo.InvariantCulture;
            pattern = @"[a-zA-Z]+, [0-9]+ [a-zA-Z]+ [0-9]+ [0-9]+:[0-9]+:[0-9]+ (?<timezone>[a-zA-Z]+)";

            Regex findTz = new Regex(pattern, RegexOptions.Compiled);

            tz = findTz.Match(dateTime).Result("${timezone}");

            format = "ddd, dd MMM yyyy HH:mm:ss " + tz;

            try
            {
                result = DateTime.ParseExact(dateTime, format, provider);
            }
            catch (FormatException ex)
            {
                Analyzer.Common.Logger.ExceptionLoggingService.Instance.WriteError("Error in trying to parse datetime: " + dateTime, ex);
            }

            return result;
        }

        //public static void AddToInner<T, Y>(this IDictionary<T, IList<Y>> dict, T key, Y value)
        //{
        //    IList<Y> values;
        //    if (!dict.TryGetValue(key, out values))
        //    {
        //        values = new List<Y>();
        //        dict[key] = values;
        //    }

        //    values.Add(value);
        //}
    }
}
