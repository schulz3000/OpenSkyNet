using System.Collections.Generic;

namespace OpenSkyNet
{
    static class QueryStringBuilder
    {
        public static string Create(Dictionary<string,object> dict)
        {
            string query=string.Empty;

            if (dict.Count > 0)
            {
                query += "?";
                foreach (var key in dict.Keys)
                {
                    query += $"{key}={dict[key]}&";
                }

                query.TrimEnd('&');
            }

            return query;
        }
    }
}
