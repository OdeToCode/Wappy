using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Wappy.Storage.Blobs
{
    public class SasBlobBuilder
    {
        private readonly CloudBlobClient _blobClient;

        public SasBlobBuilder(string connectionStringName = "StorageConnectionString")
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionStringName));
            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public string GenerateSasUrl(string containerName, string blobName, SharedAccessBlobPolicy policy)
        {
            var container = _blobClient.GetContainerReference(containerName);
            var blob = container.GetBlockBlobReference(blobName);
            string sasBlobToken = blob.GetSharedAccessSignature(policy);
            return blob.Uri + sasBlobToken;
        }
    }
}
