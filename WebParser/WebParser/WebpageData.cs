using AngleSharp;
using AngleSharp.Dom;

namespace WebParser
{
    internal class WebpageData
    {
        public async Task<List<Country>> CountriesAsync()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync("https://yandex.ru/pogoda/moscow");
            var countries =
           document
           .QuerySelectorAll("div")
           .Where(e =>  e.ClassName == "forecast-briefly__name")
           .ToList();

            List<Country> parsedCountries = new();
            foreach (var country in countries)
            {
                var lines = country
                .Text()
                .Split("\n")
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

                parsedCountries.Add(new Country()
                {
                    Day = lines[0].Trim(),
                    //Time = lines[1].Split(':')[1].Trim(),
                    //Population = lines[2].Split(':')[1].Trim(),
                    //Area_KM_Squared = lines[3].Split(':')[1].Trim()
                });
            }

            var output = parsedCountries.ToList();
            return output;
        }
    }
}
