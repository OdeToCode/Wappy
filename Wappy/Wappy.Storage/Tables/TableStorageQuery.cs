using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace Wappy.Storage.Tables
{
    /// <summary>
    /// A base class for Table Storage queries. The idea is to derive from this class and 
    /// modify the Query field with the details of the query to execute. 
    /// </summary>
    /// <typeparam name="T">The type of object to return</typeparam>
    public class StorageQuery<T> where T : ITableEntity, new()
    {
        protected TableQuery<T> Query;

        public StorageQuery()
        {
            Query = new TableQuery<T>();
        }

        /// <summary>
        /// Executes the query and handles continuation tokens
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> ExecuteOn(CloudTable table)
        {
            var token = new TableContinuationToken();
            var segment = table.ExecuteQuerySegmented(Query, token);
            while (token != null)
            {
                foreach (var result in segment)
                {
                    yield return result;
                }
                segment = table.ExecuteQuerySegmented(Query, token);
                token = segment.ContinuationToken;
            }
        }

        /// <summary>
        /// A query helper for WHERE value >= from && value <= low
        /// </summary>
        /// <param name="key">Name of key to search (PartitionKey, most likely)</param>
        /// <param name="from">Low value</param>
        /// <param name="to">High value</param>
        /// <returns></returns>
        protected string InclusiveRangeFilter(string key, string from, string to)
        {
            var low = TableQuery.GenerateFilterCondition(key, QueryComparisons.GreaterThanOrEqual, from);
            var high = TableQuery.GenerateFilterCondition(key, QueryComparisons.LessThanOrEqual, to);
            return TableQuery.CombineFilters(low, TableOperators.And, high);
        }
    }
}