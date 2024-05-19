using HtmlAgilityPack;
using Mashinin.Entities;
using Mashinin.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;
using System.Web;

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


        public async Task CreateCars()
        {
            List<ExtractedCarDetail> dbCarDetails = await _unitOfWork.ExtractedCarDetailRepository.GetSelectedByExAsync(
                selector: x => new ExtractedCarDetail
                {
                    Link = x.Link,
                    Id = x.Id,
                });


            int[] makeIds = {
                29,
                78,
                6,
                67,
                21,
                38,
                47,
                18,
                19,
                45,
                40,
                28,
                31,
                48,
                49,
                50,
                56,
                80,
                85,
                86,
                87,
                89,
                94,
                103,
                104,
                106,
                107,
                114,
                120,
                127,
                128,
                130,
                131,
                151,
                152,
                159,
                162,
                165,
                166,
                168,
                169,
                173,
                179,
                182,
                185,
                187,
                188,
                189,
                191,
                202,
                207,
                214,
                222,
                227,
                228,
                238,
                243,
                253,
                262,
                270,
                271,
                272,
                273,
                278,
                283,
                285,
                297,
                305};

            double conversionRate = 0;

            string currencyUrl = "https://azn.day.az/";

            HttpResponseMessage currencyResponse = null;

            using (HttpClient currencyClient = new HttpClient())
            {
                currencyResponse = await currencyClient.GetAsync(currencyUrl);
            }

            if (currencyResponse.IsSuccessStatusCode)
            {
                HtmlDocument currencyDoc = new HtmlDocument();
                string currencyHtml = await currencyResponse.Content.ReadAsStringAsync();
                currencyDoc.LoadHtml(currencyHtml);

                HtmlNode manatDiv = currencyDoc.DocumentNode.SelectSingleNode("//div[contains(text(), 'manat 1 avro üçün')]");
                HtmlNode tdElement = manatDiv?.ParentNode;
                string currencyRateText = tdElement?.SelectSingleNode(".//div[@class='large-text']")?.InnerText.Replace(".", "");
                conversionRate = Convert.ToDouble(currencyRateText) / 10000;
            }

            List<Make> makes = await _unitOfWork.MakeRepository.GetAllByExAsync(m => makeIds.Contains(m.Id), "Models");

            foreach (Make make in makes)
            {
                int turboAzMakeId = make.TurboAzId;

                foreach (Model model in make.Models)
                {
                    int turboAzModelId = model.TurboAzId;

                    int carsCount = 0; //butun mashinlarin sayi
                    double pageCount = 0;

                    var parameters = new Dictionary<string, string>
                    {
                        { "q[make][]", turboAzMakeId.ToString() },
                        { "q[model][]", turboAzModelId.ToString() },
                    };

                    string queryString = string.Join("&", parameters.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

                    string url = "https://turbo.az/autos?" + queryString;

                    HttpResponseMessage response = null;

                    using (HttpClient client = new HttpClient())
                    {
                        response = await client.GetAsync(url);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();

                        //var wwwRootPath = _webHostEnvironment.WebRootPath;

                        //var filePath = Path.Combine(wwwRootPath, "example.html");

                        //await System.IO.File.WriteAllTextAsync(filePath, res);

                        HtmlDocument htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(res);

                        //getting count of cars
                        var pElement = htmlDocument.DocumentNode.SelectSingleNode("//p[@class='products-title-amount']");

                        if (pElement != null)
                        {
                            var innerText = pElement.InnerText;

                            var numberIndex = innerText.IndexOf(' ');
                            if (numberIndex != -1 && int.TryParse(innerText.Substring(0, numberIndex), out int number))
                            {
                                carsCount = number; // bu bize elanlarin sayini verir!
                                pageCount = Math.Ceiling((double)carsCount / 24);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }

                    //i got pagecount, moving forward 

                    HtmlNodeCollection productElements = new HtmlNodeCollection(null);

                    for (int i = 1; i <= pageCount; i++)
                    {
                        var newParameters = new Dictionary<string, string>
                        {
                            { "q[make][]", turboAzMakeId.ToString() },
                            { "q[model][]", turboAzModelId.ToString() },
                            { "page", i.ToString() },
                        };

                        string newQueryString = string.Join("&", newParameters.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

                        string newUrl = "https://turbo.az/autos?" + newQueryString;

                        HttpResponseMessage newResponse = null;

                        using (HttpClient client = new HttpClient())
                        {
                            newResponse = await client.GetAsync(newUrl);
                        }

                        if (newResponse.IsSuccessStatusCode)
                        {
                            string responseHTML = await newResponse.Content.ReadAsStringAsync();

                            HtmlDocument htmlDocument = new HtmlDocument();
                            htmlDocument.LoadHtml(responseHTML);

                            HtmlNode productsTitleDiv = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='products-title']");

                            if (productsTitleDiv == null)
                                continue;

                            HtmlNode tzContainerDiv = productsTitleDiv.SelectSingleNode("./ancestor::div[@class='tz-container']");

                            HtmlNode productsDiv = tzContainerDiv.SelectSingleNode(".//div[@class='products']");

                            HtmlNodeCollection carCards = productsDiv.SelectNodes(".//div[contains(concat(' ', normalize-space(@class), ' '), ' products-i ')]");

                            foreach (HtmlNode carCard in carCards)
                            {
                                productElements.Add(carCard);
                            }
                        }
                    }

                    List<ExtractedCarDetail> detailsList = new List<ExtractedCarDetail>();

                    if (productElements != null)
                    {
                        foreach (var productElement in productElements)
                        {
                            #region DateTime

                            string datetimeText = productElement.SelectSingleNode(".//div[@class='products-i__datetime']")?.InnerText.Trim();
                            DateTime postCreatedAt;

                            if (datetimeText.Contains("bugün"))
                            {
                                string time = datetimeText.Substring(datetimeText.LastIndexOf(' ') + 1);

                                DateTime today = DateTime.Today;

                                postCreatedAt = DateTime.ParseExact(today.ToString("dd.MM.yyyy") + " " + time, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                            }
                            else if (datetimeText.Contains("dünən"))
                            {
                                string time = datetimeText.Substring(datetimeText.LastIndexOf(' ') + 1);

                                DateTime yesterday = DateTime.Today.AddDays(-1);

                                postCreatedAt = DateTime.ParseExact(yesterday.ToString("dd.MM.yyyy") + " " + time, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                string[] dateTimeParts = datetimeText.Split(',');

                                string datePart = dateTimeParts[1].Trim();

                                string[] dateParts = datePart.Split(new[] { '.', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                                int day = int.Parse(dateParts[0]);
                                int month = int.Parse(dateParts[1]);
                                int postYear = int.Parse(dateParts[2]);
                                int hour = int.Parse(dateParts[3]);
                                int minute = int.Parse(dateParts[4]);

                                postCreatedAt = new DateTime(postYear, month, day, hour, minute, 0);
                            }

                            #endregion

                            #region Price

                            var priceElement = productElement.SelectSingleNode(".//div[@class='product-price']");
                            var priceText = priceElement?.InnerText.Trim();

                            var priceParts = priceText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            string priceValue = "";

                            if (priceParts.Length == 3)
                            {
                                priceValue = priceParts[0] + priceParts[1];
                            }
                            else if (priceParts.Length == 2)
                            {
                                priceValue = priceParts[0];
                            }

                            string currency = priceParts[priceParts.Length - 1];
                            bool isUsd = (currency.ToLowerInvariant() == "usd" || currency == "$");
                            bool isEur = (currency.ToLowerInvariant() == "eur" || currency == "€");
                            double price = double.Parse(priceValue);

                            if (isUsd)
                            {
                                price = price * 1.7;
                            }
                            else if (isEur)
                            {
                                price = price * conversionRate;
                            }

                            #endregion

                            #region Attributes

                            var attributesElement = productElement.SelectSingleNode(".//div[@class='products-i__attributes products-i__bottom-text']");
                            var attributesText = attributesElement?.InnerText.Trim();
                            var attributeParts = attributesText.Split(',');

                            string carYear = attributeParts[0].Trim();
                            string engineCapacityText = attributeParts[1].Replace("L", "").Trim();
                            string odometerText = attributeParts[2].Replace(" km", "").Replace(" ", "").Trim();

                            double carEngineCapacity = double.Parse(engineCapacityText) * 100;
                            int carOdometer = int.Parse(odometerText);

                            #endregion

                            #region Link

                            var carLinkElement = productElement.SelectSingleNode(".//a[@class='products-i__link']");
                            string hrefValue = carLinkElement.GetAttributeValue("href", "");

                            string carLink = hrefValue.Replace("/autos/", "");

                            #endregion

                            detailsList.Add(new ExtractedCarDetail
                            {
                                Price = price,
                                Currency = "AZN",
                                Year = int.Parse(carYear),
                                EngineVolume = (int)carEngineCapacity,
                                Odometer = carOdometer,
                                PostCreatedAt = postCreatedAt,
                                CreatedAt = DateTime.UtcNow.AddHours(4),
                                MakeId = make.Id,
                                ModelId = model.Id,
                                TurboAzModelId = turboAzModelId,
                                TurboAzMakeId = turboAzMakeId,
                                IsNew = carOdometer <= 100,
                                Link = carLink
                            });
                        }
                    }

                    await _unitOfWork.ExtractedCarDetailRepository.AddRangeAsync(detailsList);
                    await _unitOfWork.CommitAsync();
                }
            }
        }

        public async Task CreateNumbers(int? skip)
        {
            List<ExtractedCarDetail> carDetails = await _unitOfWork.ExtractedCarDetailRepository.GetSelectedByExAsync(
                selector: x => new ExtractedCarDetail
                {
                    Link = x.Link,
                    Id = x.Id,
                });
            //cardetails 56876
            //numbers 56876

            List<ExtractedNumber> dbNums = await _unitOfWork.ExtractedNumberRepository.GetSelectedByExAsync(
                selector: x => new ExtractedNumber
                {
                    Id = x.Id,
                    Link = x.Link
                });

            //carDetails.Reverse();
            carDetails = carDetails.Skip(skip ?? 0).ToList();//102401

            foreach (ExtractedCarDetail carDetail in carDetails)
            {
                if (dbNums.Any(x => x.Link == carDetail.Link))
                {
                    continue;
                }

                string url = "https://turbo.az/autos/" + carDetail.Link;

                HttpResponseMessage response = null;

                using (HttpClient client = new HttpClient())
                {
                    response = await client.GetAsync(url);
                }

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    ExtractedCarDetail notFountCarDetail = await _unitOfWork.ExtractedCarDetailRepository.GetAsync(x => x.Link == carDetail.Link);

                    if (notFountCarDetail is not null)
                    {
                        _unitOfWork.ExtractedCarDetailRepository.Remove(notFountCarDetail);
                        await _unitOfWork.CommitAsync();
                    }

                    continue;
                }

                if (response.IsSuccessStatusCode)
                {
                    string html = await response.Content.ReadAsStringAsync();

                    HtmlDocument htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    HtmlNodeCollection propertyValueSpans = htmlDocument.DocumentNode.SelectNodes("//span[@class='product-properties__i-value']");

                    if (propertyValueSpans == null)
                    {
                        ExtractedCarDetail notFountCarDetail = await _unitOfWork.ExtractedCarDetailRepository.GetAsync(x => x.Link == carDetail.Link);

                        if (notFountCarDetail is not null)
                        {
                            _unitOfWork.ExtractedCarDetailRepository.Remove(notFountCarDetail);
                            await _unitOfWork.CommitAsync();
                        }
                    }

                    if (propertyValueSpans != null && propertyValueSpans.Count >= 3)
                    {
                        HtmlNode thirdPropertyValueSpan = propertyValueSpans[2];
                        HtmlNode linkElement = thirdPropertyValueSpan.SelectSingleNode(".//a");

                        if (linkElement != null)
                        {
                            string href = linkElement.GetAttributeValue("href", "");

                            string decodedHref = HttpUtility.UrlDecode(href);
                            decodedHref = HttpUtility.HtmlDecode(decodedHref);

                            var queryParams = decodedHref.Split('?')[1].Split('&')
                                .Select(param => param.Split('='))
                                .ToDictionary(pair => pair[0], pair => pair[1]);

                            string makeKey = queryParams.Keys.FirstOrDefault(key => key.Contains("q[make][]"));
                            string modelKey = queryParams.Keys.FirstOrDefault(key => key.Contains("q[model][]"));

                            int turboAzMakeId = int.Parse(queryParams[makeKey]);
                            int turboAzModelId = int.Parse(queryParams[modelKey]);

                            HtmlNode phoneLink = htmlDocument.DocumentNode.SelectSingleNode("//a[@class='product-phones__list-i']");

                            if (phoneLink != null)
                            {
                                string phoneNumber = phoneLink.GetAttributeValue("href", "");
                                phoneNumber = phoneNumber.Substring(phoneNumber.IndexOf(":") + 1).TrimStart('0');
                                phoneNumber = $"+994{phoneNumber}";

                                Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.TurboAzId == turboAzMakeId);
                                int makeId = make.Id;

                                Model model = await _unitOfWork.ModelRepository.GetAsync(x => x.TurboAzId == turboAzModelId);

                                if (model is not null)
                                {
                                    int modelId = model.Id;

                                    ExtractedNumber extractedNumber = new ExtractedNumber
                                    {
                                        Link = carDetail.Link,
                                        CreatedAt = DateTime.UtcNow.AddHours(4),
                                        TurboAzMakeId = turboAzMakeId,
                                        TurboAzModelId = turboAzModelId,
                                        MakeId = makeId,
                                        ModelId = modelId,
                                        PhoneNumber = phoneNumber
                                    };

                                    await _unitOfWork.ExtractedNumberRepository.AddAsync(extractedNumber);
                                    await _unitOfWork.CommitAsync();
                                }
                            }
                            else
                            {
                                ExtractedCarDetail notFountCarDetail = await _unitOfWork.ExtractedCarDetailRepository.GetAsync(x => x.Link == carDetail.Link);

                                if (notFountCarDetail is not null)
                                {
                                    _unitOfWork.ExtractedCarDetailRepository.Remove(notFountCarDetail);
                                    await _unitOfWork.CommitAsync();
                                }
                            }
                        }
                    }
                }
            }
        }

        public async Task RemoveDuplicates()
        {
            List<ExtractedCarDetail> details = await _unitOfWork.ExtractedCarDetailRepository.GetSelectedByExAsync(
                selector: entity => new ExtractedCarDetail
                {
                    Id = entity.Id,
                    Link = entity.Link,
                    PostCreatedAt = entity.PostCreatedAt,
                    CreatedAt = entity.CreatedAt
                });

            var duplicates = details.GroupBy(d => d.Link)
                        .Where(g => g.Count() > 1)
                        .SelectMany(g => g.OrderByDescending(x => x.CreatedAt).Skip(1)).ToList();

            _unitOfWork.ExtractedCarDetailRepository.RemoveRange(duplicates);
            await _unitOfWork.CommitAsync();

            List<ExtractedNumber> numbers = await _unitOfWork.ExtractedNumberRepository.GetSelectedByExAsync(
               selector: entity => new ExtractedNumber
               {
                   Id = entity.Id,
                   //PhoneNumber = entity.PhoneNumber,
                   Link = entity.Link,
                   CreatedAt = entity.CreatedAt
               });

            var duplicateNumbers = numbers.GroupBy(d => d.Link)
                        .Where(g => g.Count() > 1)
                        .SelectMany(g => g.OrderByDescending(x => x.CreatedAt).Skip(1)).ToList();

            _unitOfWork.ExtractedNumberRepository.RemoveRange(duplicateNumbers);
            await _unitOfWork.CommitAsync();
        }




        //public async Task<List<ExtractedCarDetail>> GetAveragePriceForOneCar(GetStatisticsDTO getStatisticsDTO)
        //{
        //    Make make = await _unitOfWork.MakeRepository.GetAsync(x => x.Id == getStatisticsDTO.MakeId);
        //    int turboAzMakeId = make.TurboAzId;


        //    Model model = await _unitOfWork.ModelRepository.GetAsync(x => x.Id == getStatisticsDTO.ModelId);
        //    int turboAzModelId = model.TurboAzId;

        //    int bodyType = getStatisticsDTO.BodyType; //category yazilir, enumi duzeltdim
        //    int fuelType = getStatisticsDTO.FuelType; // enum sovpadaet
        //    int engineVolume = getStatisticsDTO.EngineVolume; //rastut po 100, zadani v select optione, mene de ele gelecek
        //    double odometer = getStatisticsDTO.Odometer; //ponatnoe delo
        //    int transmissionType = getStatisticsDTO.TransmissionType; //enum popravil
        //    int year = getStatisticsDTO.Year; //vse pon

        //    int carsCount = 0; //butun mashinlarin sayi
        //    double pageCount = 0;

        //    var parameters = new Dictionary<string, string>
        //    {
        //        { "q[make][]", turboAzMakeId.ToString() },
        //        { "q[model][]", turboAzModelId.ToString() },
        //        //{ "q[category][]", bodyType.ToString() },
        //        //{ "q[year_from]", (year - 2).ToString() },
        //        //{ "q[year_to]", (year + 2).ToString() },
        //        //{ "q[fuel_type][]", fuelType.ToString() },
        //        //{ "q[transmission][]", transmissionType.ToString() },
        //        //{ "q[engine_volume_from]", (engineVolume - 100).ToString() },
        //        //{ "q[engine_volume_to]", (engineVolume + 100).ToString() },
        //        //{ "q[mileage_from]", (odometer - 20000).ToString() },
        //        //{ "q[mileage_to]", (odometer + 20000).ToString() },
        //        //{ "q[crashed]", "1" },
        //        //{ "q[painted]", "1" },
        //        //{ "q[for_spare_parts]", "0" }
        //    };

        //    string queryString = string.Join("&", parameters.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

        //    string url = "https://turbo.az/autos?" + queryString;

        //    HttpResponseMessage response = null;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        response = await client.GetAsync(url);
        //    }

        //    if (response.IsSuccessStatusCode)
        //    {
        //        string res = await response.Content.ReadAsStringAsync();

        //        var wwwRootPath = _webHostEnvironment.WebRootPath;

        //        var filePath = Path.Combine(wwwRootPath, "example.html");

        //        await System.IO.File.WriteAllTextAsync(filePath, res);

        //        HtmlDocument htmlDocument = new HtmlDocument();
        //        htmlDocument.LoadHtml(res);

        //        //getting count of cars
        //        var pElement = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='products-container']//p[@class='products-title-amount']");

        //        if (pElement != null)
        //        {
        //            var innerText = pElement.InnerText;

        //            var numberIndex = innerText.IndexOf(' ');
        //            if (numberIndex != -1 && int.TryParse(innerText.Substring(0, numberIndex), out int number))
        //            {
        //                carsCount = number; // bu bize elanlarin sayini verir!
        //                pageCount = Math.Ceiling((double)carsCount / 24);
        //            }
        //        }
        //    }

        //    List<ExtractedCarDetail> cars = await GetStatisticsAsync(
        //        getStatisticsDTO.MakeId, getStatisticsDTO.ModelId,
        //        pageCount, turboAzMakeId, turboAzModelId, bodyType, fuelType, engineVolume, odometer, transmissionType, year);

        //    return cars;
        //}


    }
}