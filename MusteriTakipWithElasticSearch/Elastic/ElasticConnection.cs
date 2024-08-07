﻿using Elastic.Clients.Elasticsearch.TransformManagement;
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

        public async void DeletefromElastic(ElasticsearchClient _client, Musteri musteri)
        {

            var existIndex = _client.Indices.ExistsAsync(IndexName);

            if (existIndex != null)
            {
                var deleteFromElastic = await _client.DeleteByQueryAsync<Musteri>(IndexName, d => d.Query(q => q.Term(t => t.Field(f => f.MusteriNo).Value(musteri.MusteriNo))));
                // musterinin musteri nosuna gore musteriyi elasticten sil
            }

            else
            {
                throw new Exception("Geçerli index bulunamadı.");
            }
        }

        public async void UpdateElastic(ElasticsearchClient _client, Musteri musteri)
        {

            var existIndex = _client.Indices.ExistsAsync(IndexName);

            if (existIndex != null) 
            {
                var inlineScript = new InlineScript() 
                {
                   
                    Source = $"ctx._source.{"musteri_adi"} = params.ad; ctx._source.{"musteri_soyadi"} = params.soyad; " +
                    $"ctx._source.{"musteri_tel"} = params.telefon_no; ctx._source.{"musteri_eposta"} = params.eposta",
                    Params = new Dictionary<string, object>
                    {
                             { "ad", musteri.MusteriAdi },
                             { "soyad", musteri.MusteriSoyadi },
                             { "telefon_no", musteri.MusteriTel },
                             { "eposta", musteri.MusteriEposta }
                      }
                };

                var script = new Script(inlineScript);
                var updateElastic = await _client.UpdateByQueryAsync<Musteri>(IndexName, d => d
                                                                        .Query(q => q
                                                                        .Term(m => m
                                                                            .Field(f => f.MusteriNo)
                                                                            .Value(musteri.MusteriNo)))
                                                                            .Script(script));

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
