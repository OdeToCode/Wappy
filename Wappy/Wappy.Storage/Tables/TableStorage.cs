using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Wappy.Storage.Tables
{
    /// <summary>
    /// A class for Azure Table Storage access
    /// </summary>
    /// <typeparam name="T">T is a TableEntity type to save and query</typeparam>
    public class TableStorage<T> where T : ITableEntity, new()
    {
        /// <summary>
        /// Connect to a table in table storage, the table is created if it doesn't exist
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="connectionName">Name of the storage connection string in app.config</param>
        public TableStorage(string tableName, string connectionName = "StorageConnectionString")
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting(connectionName));
            var tableClient = storageAccount.CreateCloudTableClient();

            Table = tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();
        }

        /// <summary>
        /// Insert a new entity into table storage
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <returns>The ETag for the new entity</returns>
        public virtual string Insert(T entity)
        {
            var operation = TableOperation.Insert(entity);
            var result = Table.Execute(operation);
            return result.Etag;
        }
       
        protected CloudTable Table;
    }
}