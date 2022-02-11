using System;

namespace Microsoft.AspNetCore.Mvc
{
    internal static class Guard
    {
        public static T NotNull<T>(T value, string paramName)
        {
            if (value is null)
                throw new ArgumentNullException(paramName);
            return value;
        }
    }
}