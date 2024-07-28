using Elastic.Clients.Elasticsearch.TransformManagement;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusteriTakipWithElasticSearch.Models;

namespace MusteriTakipWithElasticSearch.Elastic
{

    public class ElasticConnection
    {
        private const string IndexName = "musteriler";
        public ElasticConnection() { }

        public ElasticsearchClient CreateConnection()
        {
            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
           .Authentication(new BasicAuthentication("elastic", "changeme"));

            var client = new ElasticsearchClient(settings);

            if (client == null)
            {
                throw new Exception("Connection Başarısız Oldu");
            }

            else
            {
                return client;
            }
        }

        public async void WritetoElastic(ElasticsearchClient _client, Musteri musteri)
        {

            var existIndex = _client.Indices.ExistsAsync(IndexName);

            if (existIndex != null)
            {
                var writeToElastic = await _client.IndexAsync(musteri, idx => idx.Index(IndexName)); // index name musteriler olana musteri nesnesini indexle
            }

            else 
            {
                throw new Exception("Geçerli index bulunamadı.");
            }
        }

        public async Task<List<Musteri>> ElasticSearchQuery(ElasticsearchClient _client)
        {

            var existIndex = _client.Indices.ExistsAsync(IndexName);

            var searchResult = await _client.SearchAsync<Musteri>(m => m.Index(IndexName)
            .Query(q => q
            .Bool(b => b
            .Must(m => m
            .Match(m => m
            .Field(f => f.MusteriAdi).Query("elif")))))); // sorgu degistirelecek

            foreach (var result in searchResult.Documents)
            {
                Console.WriteLine(result.MusteriEposta);
            }

            return searchResult.Documents.ToList();

        }





    }

}
