using Microsoft.WindowsAzure.Storage.Blob;
using Wappy.Storage.Time;

namespace Wappy.Storage.SasPolicy
{
    public static class DefaultSasPolicy
    {
        public static SharedAccessBlobPolicy FourHourWrite = new SharedAccessBlobPolicy()
        {
            SharedAccessStartTime = UtcTime.Now().AddMinutes(-15),
            SharedAccessExpiryTime = UtcTime.Now().AddHours(4),
            Permissions = SharedAccessBlobPermissions.Write
        };

        public static SharedAccessBlobPolicy FourHourRead = new SharedAccessBlobPolicy()
        {
            SharedAccessStartTime = UtcTime.Now().AddMinutes(-15),
            SharedAccessExpiryTime = UtcTime.Now().AddHours(4),
            Permissions = SharedAccessBlobPermissions.Write
        };
    }
}
