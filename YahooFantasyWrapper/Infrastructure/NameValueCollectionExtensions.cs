using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using YahooFantasyWrapper.Client;

namespace YahooFantasyWrapper.Infrastructure
{
    public static class NameValueCollectionExtensions
    {
        public static string GetOrThrowUnexpectedResponse(this NameValueCollection collection, string key)
        {
            var value = collection[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new UnexpectedResponseException(key);
            }
            return value;
        }
    }
}
