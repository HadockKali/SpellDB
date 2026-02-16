using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace SpellDB
{
    public class DDragonParser
    {
        private const string VersionRequestUrl = "https://ddragon.leagueoflegends.com/api/versions.json";
        private static string ChampionRequestUrl
        {
            get { return string.Format("http://ddragon.leagueoflegends.com/cdn/{0}/data/en_US/championFull.json", GetLatestLeaguePatchVersion()); }
        }
        public static void ParseAndBuildSpellDb()
        {
            List<string> spellDb = new List<string>();

            Console.WriteLine("Parsing DDragoon Data...");
            JObject parsedData = ParseChampionData();
            Console.WriteLine("Sucessfully Parsed DDragon Data");

            Console.WriteLine("Building SpellDB...");

            foreach (string championName in parsedData["keys"]?.ToObject<Dictionary<string, string>>()?.Values)
            {
                JToken[] spellData = parsedData["data"]?[championName]?["spells"].ToArray();
                Champion championObject = GenerateChampionObject(spellData);

                if(championName == parsedData["keys"]?.ToObject<Dictionary<string, string>>()?.Values.Last())
                {
                    spellDb.Add(
                    $"    {(char)34}{championName}{(char)34}:\n" +
                    $"    {JsonConvert.SerializeObject(championObject)}\n"
                    );
                }
                else
                {
                    spellDb.Add(
                    $"    {(char)34}{championName}{(char)34}:\n" +
                    $"    {JsonConvert.SerializeObject(championObject)},\n"
                    );
                }
            }

            using(StreamWriter spellDbJson = new StreamWriter(File.Create(Directory.GetCurrentDirectory() + @"/SpellDB.json")))
            {
                spellDbJson.WriteLine("{");

                foreach(string championSpellData in spellDb)
                {
                    if(championSpellData == spellDb.Last())
                    {
                        spellDbJson.WriteLine(championSpellData + "}");
                    }
                    else spellDbJson.WriteLine(championSpellData);
                }
            }

            Console.WriteLine("Successfully built SpellDB");
        }

        private static Champion GenerateChampionObject(JToken[] spellData)
        {
            return new Champion()
            {
                Q = new Champion.QSlot()
                {
                    Name = spellData[0]["id"]?.ToString(),
                    MaxRank = spellData[0]["maxrank"].ToObject<int>(),
                    CoolDown = spellData[0]["cooldown"]?.ToObject<int[]>(),
                    CoolDownBurn = spellData[0]["cooldownBurn"]?.ToString(),
                    Cost = spellData[0]["cost"]?.ToObject<int[]>(),
                    CostType = spellData[0]["costType"]?.ToString(),
                    MaxAmmo = spellData[0]["maxammo"]?.ToString(),
                    Range = spellData[0]["range"]?.ToObject<int[]>(),
                    RangeBurn = spellData[0]["rangeBurn"]?.ToString(),
                },

                W = new Champion.WSlot()
                {
                    Name = spellData[1]["id"]?.ToString(),
                    MaxRank = spellData[1]["maxrank"].ToObject<int>(),
                    CoolDown = spellData[1]["cooldown"]?.ToObject<int[]>(),
                    CoolDownBurn = spellData[1]["cooldownBurn"]?.ToString(),
                    Cost = spellData[1]["cost"]?.ToObject<int[]>(),
                    CostType = spellData[1]["costType"]?.ToString(),
                    MaxAmmo = spellData[1]["maxammo"]?.ToString(),
                    Range = spellData[1]["range"]?.ToObject<int[]>(),
                    RangeBurn = spellData[1]["rangeBurn"]?.ToString(),
                },

                E = new Champion.ESlot()
                {
                    Name = spellData[2]["id"]?.ToString(),
                    MaxRank = spellData[2]["maxrank"].ToObject<int>(),
                    CoolDown = spellData[2]["cooldown"]?.ToObject<int[]>(),
                    CoolDownBurn = spellData[2]["cooldownBurn"]?.ToString(),
                    Cost = spellData[2]["cost"]?.ToObject<int[]>(),
                    CostType = spellData[2]["costType"]?.ToString(),
                    MaxAmmo = spellData[2]["maxammo"]?.ToString(),
                    Range = spellData[2]["range"]?.ToObject<int[]>(),
                    RangeBurn = spellData[2]["rangeBurn"]?.ToString(),
                },

                R = new Champion.RSlot()
                {
                    Name = spellData[3]["id"]?.ToString(),
                    MaxRank = spellData[3]["maxrank"].ToObject<int>(),
                    CoolDown = spellData[3]["cooldown"].ToObject<int[]>(),
                    CoolDownBurn = spellData[3]["cooldownBurn"]?.ToString(),
                    Cost = spellData[3]["cost"]?.ToObject<int[]>(),
                    CostType = spellData[3]["costType"]?.ToString(),
                    MaxAmmo = spellData[3]["maxammo"]?.ToString(),
                    Range = spellData[3]["range"]?.ToObject<int[]>(),
                    RangeBurn = spellData[3]["rangeBurn"]?.ToString(),
                }
            };
        }

        private static JObject ParseChampionData()
        {
            string championDataString = new WebClient().DownloadString(ChampionRequestUrl);

            try
            {
                return JObject.Parse(championDataString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: \n{ex}");
                throw new Exception("ChampionDataParseFailedException");
            }
        }

        private static string GetLatestLeaguePatchVersion()
        {
            try
            {
                return JArray.Parse(new WebClient().DownloadString(VersionRequestUrl))[0].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: \n{ex}");
                throw new Exception("FailedToRetrievePatchVersionException");
            }
        }
    }
}
