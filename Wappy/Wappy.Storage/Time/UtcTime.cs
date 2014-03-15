using System;

namespace Wappy.Storage.Time
{
    public static class UtcTime
    {
        public static Func<DateTime> Now = () => DateTime.UtcNow;
    }
}
