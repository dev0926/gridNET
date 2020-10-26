﻿using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GridShared.Utility
{
    public static class JsonSerializerOptionsExtensions
    {
        public static JsonSerializerOptions AddOdataSupport(this JsonSerializerOptions jsonOptions)
        {
            jsonOptions.IgnoreNullValues = true;
            // required for Blazor WA
            var converters = jsonOptions.Converters.Where(r => r.CanConvert(typeof(DateTime)));
            if (converters != null)
                for (int i = converters.Count() - 1; i >= 0; i--)
                {
                    jsonOptions.Converters.Remove(converters.ElementAt(i));
                }
            // required for Blazor WA
            jsonOptions.Converters.Add(new ODataDateTimeConverter());

            jsonOptions.Converters.Add(new JsonStringEnumConverter(null));
            return jsonOptions;
        }

        public static JsonSerializerOptions AddByteArraySupport(this JsonSerializerOptions jsonOptions)
        {
            jsonOptions.IgnoreNullValues = true;
            // required for Blazor WA
            var converters = jsonOptions.Converters.Where(r => r.CanConvert(typeof(byte[])));
            if (converters != null)
                for (int i = converters.Count() - 1; i >= 0; i--)
                {
                    jsonOptions.Converters.Remove(converters.ElementAt(i));
                }
            // required for Blazor WA
            jsonOptions.Converters.Add(new ByteArrayConverter());
            return jsonOptions;
        }
    }
}
