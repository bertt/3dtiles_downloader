﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using threedtiles_exporter;

namespace src
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tilesetjson = File.ReadAllText("tileset.json");
            var json = JsonConvert.DeserializeObject<TileSetJson>(tilesetjson);
            var httpClient = new HttpClient();
            foreach (var tile in json.root.children)
            {
                var tile_url = tile.content.uri;
                var res = await httpClient.GetAsync($"http://myserver/{tile_url}");
                var bytes = await res.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes(tile_url, bytes);
                Console.Write(".");
            }

        }
    }
}
