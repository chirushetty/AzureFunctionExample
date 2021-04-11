using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class CosmosUserRepositoryOptions
    {
        public CosmosUserRepositoryOptions(string accountEndpoint, string accountKey, string databaseName)
        {
            AccountEndpoint = accountEndpoint;
            AccountKey = accountKey;
            DatabaseName = databaseName;
        }
        public string AccountEndpoint { get; set; }
        public string AccountKey { get; set; }
        public string DatabaseName { get; set; }
    }
}
