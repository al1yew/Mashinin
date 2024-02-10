using HtmlAgilityPack;
using Mashinin.DTOs.StatisticsDTOs;
using Mashinin.Entities;
using Mashinin.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Mashinin.Implementations
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StatisticsService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<ExtractedCarPriceDetailsDTO>> GetAveragePriceForOneCar(GetStatisticsDTO getStatisticsDTO)
        {
            Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == getStatisticsDTO.MakeId);
            int turboAzMakeId = make.TurboAzId;

            Model model = await _unitOfWork.ModelRepository.GetAsync(x => x.Id == getStatisticsDTO.ModelId);
            int turboAzModelId = model.TurboAzId;

            int bodyType = getStatisticsDTO.BodyType; //category yazilir, enumi duzeltdim
            int fuelType = getStatisticsDTO.FuelType; // enum sovpadaet
            int engineVolume = getStatisticsDTO.EngineVolume; //rastut po 100, zadani v select optione, mene de ele gelecek
            double odometer = getStatisticsDTO.Odometer; //ponatnoe delo
            int transmissionType = getStatisticsDTO.TransmissionType; //enum popravil
            int year = getStatisticsDTO.Year; //vse pon

            string url = $"https://turbo.az/autos?" +
                $"q%5Bmake%5D%5B%5D={turboAzMakeId}" +
                $"&q%5Bmodel%5D%5B%5D={turboAzModelId}" +
                $"&q%5Bcategory%5D%5B%5D={bodyType}" +
                $"&q%5Byear_from%5D={year - 2}" +
                $"&q%5Byear_to%5D={year + 2}" +
                $"&q%5Bfuel_type%5D%5B%5D={fuelType}" +
                $"&q%5Btransmission%5D%5B%5D={transmissionType}" +
                $"&q%5Bengine_volume_from%5D={engineVolume - 100}" +
                $"&q%5Bengine_volume_to%5D={engineVolume + 100}" +
                $"&q%5Bmileage_from%5D={odometer - 20000}" +
                $"&q%5Bmileage_to%5D={odometer + 20000}" +
                $"&q%5Bcrashed%5D=1" +
                $"&q%5Bpainted%5D=1" +
                $"&q%5Bfor_spare_parts%5D=0" +
                $"&page=1";

            HttpResponseMessage response = null;

            int carsCount = 0; //butun mashinlarin sayi

            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync(url);
            }

            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();

                var wwwRootPath = _webHostEnvironment.WebRootPath;

                var filePath = Path.Combine(wwwRootPath, "example.html");

                await System.IO.File.WriteAllTextAsync(filePath, res);

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(res);

                //getting count of cars
                var pElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='products-container']//p[@class='products-title-amount']");

                if (pElement != null)
                {
                    var innerText = pElement.InnerText;

                    var numberIndex = innerText.IndexOf(' ');
                    if (numberIndex != -1 && int.TryParse(innerText.Substring(0, numberIndex), out int number))
                    {
                        carsCount = number; // bu bize elanlarin sayini verir!
                    }
                }

                var productElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='products']/div[@class='products-i']");

                List<ExtractedCarPriceDetailsDTO> detailsList = new List<ExtractedCarPriceDetailsDTO>();

                if (productElements != null)
                {
                    foreach (var productElement in productElements)
                    {
                        var priceElement = productElement.SelectSingleNode(".//div[@class='product-price']");

                        var priceText = priceElement?.InnerText.Trim();

                        if (!string.IsNullOrEmpty(priceText))
                        {
                            var parts = priceText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            if (parts.Length >= 1)
                            {
                                string priceValue = parts[0].Replace(" ", "") + parts[1];

                                string currency = parts[parts.Length - 1];

                                detailsList.Add(new ExtractedCarPriceDetailsDTO
                                {
                                    Price = double.Parse(priceValue),
                                    Currency = currency
                                });
                                //check if USD, or EUR, and convert to azn, then give as AZN values.
                                //podumat nad errorami, i pustim arrayem, vdruq budet pusto tipa.

                                //dla praktiki s Page, nado vibrat e220 mers, 1996-2000,
                                //a odometr delat ne -20000, a -100000? cisto dla praktiki,
                                //shto b elan sayi 50den cox olsun. i praktikovatsa 
                                //mojno vicislit kolvo stranic, naverhu v kode, brosit zapros vsem stranicam, iz vsex poluchennix html
                                //vitashit products container, ili products-i, sobrat vse v kakoy to string, i uje potom delat:
                                //var productElements = htmlDocument.DocumentNode.SelectNodes("//div[@class='products']/div[@class='products-i']");
                            }
                        }
                    }
                }
                return detailsList;
            }
            return new List<ExtractedCarPriceDetailsDTO>();
        }
    }
}