using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_.NET_
{
    class MarvelManager
    {
        NetworkManager networkManager = new NetworkManager();

        public IEnumerable<Character> GetCharacters(string name,int offset = 0, int limit = 10)
        {
            string url = $"{MarvelApiConfig.BaseURL}characters?nameStartsWith={name}&ts={MarvelApiConfig.TimeStamp}&apikey={MarvelApiConfig.PublicKey}&hash={MarvelApiConfig.Hash()}&offset={offset}&limit={limit}";
            string json = networkManager.GetJsonFromUrl(url);

            JObject marvelSearch = JObject.Parse(json);
            List<JToken> results = marvelSearch["data"]["results"].Children().ToList();

            List<Character> characters = new List<Character>();
            foreach (var it in results)
            {
                characters.Add(new Character
                {
                    RealId = it["id"].ToString(),
                    Name = it["name"].ToString(),
                    ImageUrl = $"{it["thumbnail"]["path"]}.{it["thumbnail"]["extension"]}"
                });
            }


            return characters;
        }

        public IEnumerable<Comic> GetComics(string name, int offset = 0, int limit = 10)
        {
            string url = $"{MarvelApiConfig.BaseURL}comics?titleStartsWith={name}&ts={MarvelApiConfig.TimeStamp}&apikey={MarvelApiConfig.PublicKey}&hash={MarvelApiConfig.Hash()}&offset={offset}&limit={limit}";
            string json = networkManager.GetJsonFromUrl(url);

            JObject marvelSearch = JObject.Parse(json);
            List<JToken> results = marvelSearch["data"]["results"].Children().ToList();

            List<Comic> comics = new List<Comic>();
            foreach (var it in results)
            {
                comics.Add(new Comic
                {
                    RealId = it["id"].ToString(),
                    Name = it["title"].ToString(),
                    ImageUrl = $"{it["thumbnail"]["path"]}.{it["thumbnail"]["extension"]}"
                });
            }

            return comics;
        }

        public IEnumerable<Movie> GetMovies(string name,int offset = 0,int limit = 10)
        {
            string url = $"{MarvelApiConfig.BaseURL}series?titleStartsWith={name}&ts={MarvelApiConfig.TimeStamp}&apikey={MarvelApiConfig.PublicKey}&hash={MarvelApiConfig.Hash()}&offset={offset}&limit={limit}";
            string json = networkManager.GetJsonFromUrl(url);

            JObject marvelSearch = JObject.Parse(json);
            List<JToken> results = marvelSearch["data"]["results"].Children().ToList();

            List<Movie> movies = new List<Movie>();
            foreach (var it in results)
            {
                movies.Add(new Movie
                {
                    RealId = it["id"].ToString(),
                    Name = it["title"].ToString(),
                    ImageUrl = $"{it["thumbnail"]["path"]}.{it["thumbnail"]["extension"]}"
                });
            }

            return movies;
        }

        public IEnumerable<ItemPrototype> GetItemsById(string id,string from,string to)
        {
            string url = $"{MarvelApiConfig.BaseURL}{from}/{id}/{to}?ts={MarvelApiConfig.TimeStamp}&apikey={MarvelApiConfig.PublicKey}&hash={MarvelApiConfig.Hash()}";
            string json = networkManager.GetJsonFromUrl(url);

            JObject marvelSearch = JObject.Parse(json);
            List<JToken> results = marvelSearch["data"]["results"].Children().ToList();
            if(to == "characters")
            {
                List<Character> items = new List<Character>();
                foreach (var it in results)
                {
                    items.Add(new Character
                    {
                        RealId = it["id"].ToString(),
                        Name = it["name"].ToString(),
                        ImageUrl = $"{it["thumbnail"]["path"]}.{it["thumbnail"]["extension"]}"
                    });
                }
                return items;
            }
            else if(to == "comics")
            {
                List<Comic> items = new List<Comic>();
                foreach (var it in results)
                {
                    items.Add(new Comic
                    {
                        RealId = it["id"].ToString(),
                        Name = it["title"].ToString(),
                        ImageUrl = $"{it["thumbnail"]["path"]}.{it["thumbnail"]["extension"]}"
                    });
                }
                return items;
            }
            else
            {
                List<Movie> items = new List<Movie>();
                foreach (var it in results)
                {
                    items.Add(new Movie
                    {
                        RealId = it["id"].ToString(),
                        Name = it["title"].ToString(),
                        ImageUrl = $"{it["thumbnail"]["path"]}.{it["thumbnail"]["extension"]}"
                    });
                }
                return items;
            }
            
        }
    }
}
