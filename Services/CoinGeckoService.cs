using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Parcial2024.Services
{
    public class CoinGeckoService
    {
        private readonly HttpClient _httpClient;

        public CoinGeckoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para obtener el precio de BTC en USD
        public async Task<decimal> GetBtcPriceInUsdAsync()
        {
            var response = await _httpClient.GetAsync("https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

                if (data.TryGetProperty("bitcoin", out JsonElement bitcoin) &&
                    bitcoin.TryGetProperty("usd", out JsonElement usdPrice))
                {
                    return usdPrice.GetDecimal();
                }
            }

            throw new Exception("Error fetching data from CoinGecko");
        }

        // Método para obtener el precio de USD en BTC
        public async Task<decimal> GetUsdPriceInBtcAsync()
        {
            // Usar el método anterior para obtener el precio de BTC en USD
            var btcPriceInUsd = await GetBtcPriceInUsdAsync();
            if (btcPriceInUsd == 0)
            {
                throw new Exception("BTC price in USD is zero, unable to calculate USD to BTC");
            }
            return 1 / btcPriceInUsd; // Inverso de la tasa
        }
    }
}
