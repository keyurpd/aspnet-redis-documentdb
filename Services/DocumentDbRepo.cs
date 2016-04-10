using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.OptionsModel;

namespace Keyur.AspNet.Sample
{
    public class DocumentDbRepo : IDocumentDbRepo
    {
        private IOptions<AppSettings> _appSettings;

        public DocumentDbRepo(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return Client.CreateDocumentQuery<Account>(Collection.DocumentsLink)
                .AsEnumerable();
        }

        private Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == DatabaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            return db;
        }

        private DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == CollectionId)
                              .AsEnumerable()
                              .FirstOrDefault();

            return col;
        }

        private string databaseId;
        private String DatabaseId
        {
            get
            {
                if (string.IsNullOrEmpty(databaseId))
                {
                    databaseId = _appSettings.Value.DatabaseId;
                }

                return databaseId;
            }
        }

        private string collectionId;
        private String CollectionId
        {
            get
            {
                if (string.IsNullOrEmpty(collectionId))
                {
                    collectionId = _appSettings.Value.DatabaseCollectionId;
                }

                return collectionId;
            }
        }

        private Database database;
        private Database Database
        {
            get
            {
                if (database == null)
                {
                    database = ReadOrCreateDatabase();
                }

                return database;
            }
        }

        private DocumentCollection collection;
        private DocumentCollection Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return collection;
            }
        }

        private DocumentClient client;
        private DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    string endpoint = _appSettings.Value.DatabaseUrl;
                    string authKey = _appSettings.Value.DatabaseKey;
                    Uri endpointUri = new Uri(endpoint);
                    client = new DocumentClient(endpointUri, authKey);
                }

                return client;
            }
        }
    }
}