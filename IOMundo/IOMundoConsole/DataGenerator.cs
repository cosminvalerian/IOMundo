using IOMundoConsole.Models;
using IOMundoConsole.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMundoConsole
{
    public class DataGenerator
    {
        private IOfferRepository _offerRepository;
        private List<string> _services;
        private List<string> _personCombinations;

        public DataGenerator(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
            ResetServices();
            GenerateCombinations();
        }

        /// <summary>
        /// Generates offers, inserts them in the database and prints them in the console.
        /// </summary>
        public async void GenerateOffers()
        {
            List<Offer> offerList = new List<Offer>();

            while (_services.Count > 0)
            {
                string service = GetService();
                int pricePerDay = GenerateRandomNumberInRange(100, 1000, service);
                int pricePerAdult = GenerateRandomNumberInRange(20, (int)(pricePerDay * 0.2), service); // Between 20 and 20% of price per day

                for (int duration = 1; duration <= 10; duration++)
                {
                    for (int daysUntilCheckIn = 0; daysUntilCheckIn <= 365; daysUntilCheckIn++)
                    {
                        foreach (string combination in _personCombinations)
                        {
                            Offer offer = new Offer();

                            offer.ServiceCode = service;
                            offer.StayDurationNights = (uint)duration;
                            offer.CheckInDate = DateTime.Now.Date.AddDays(daysUntilCheckIn);
                            offer.PersonCombination = combination;
                            offer.PricePerAdult = pricePerAdult;
                            offer.PricePerChild = (int)(pricePerAdult * 0.5);
                            offer.Price = CalculatePrice(combination, duration, pricePerDay, pricePerAdult, (int)offer.PricePerChild);
                            offer.ShowStrikePrice = GenerateRandomBoolean(service);

                            if (!offer.ShowStrikePrice)
                            {
                                offer.StrikePrice = 0;
                            }
                            else
                            {
                                int strikePricePerDay = GenerateRandomNumberInRange(50, pricePerDay, service);
                                int strikePricePerAdult = GenerateRandomNumberInRange(10, (int)(strikePricePerDay * 0.2), service);
                                offer.StrikePricePerAdult = strikePricePerAdult;
                                offer.StrikePricePerChild = (int)(strikePricePerAdult * 0.5);
                                offer.StrikePrice = CalculatePrice(combination, duration, strikePricePerDay, strikePricePerAdult, (int)offer.StrikePricePerChild);
                            }

                            offer.LastUpdated = DateTime.Now;

                            PrintOffer(offer);

                            offerList.Add(offer);
                        }
                    }

                }
            }
            await _offerRepository.AddManyAsync(offerList);
            _offerRepository.SaveChanges();
        }

        /// <summary>
        /// Uses a seed parameter to get a random integer value.
        /// </summary>
        private int GenerateRandomNumberInRange(int min, int max, string seed)
        {
            Random random = new Random(seed.GetHashCode());
            return random.Next(min, max);
        }

        /// <summary>
        /// Uses a seed parameter to get a random boolean value.
        /// </summary>
        private bool GenerateRandomBoolean(string seed)
        {
            Random random = new Random(seed.GetHashCode());
            return random.Next(2) == 0;
        }

        /// <summary>
        /// Returns first service from the list then removes it.
        /// </summary>
        private string GetService()
        {
            string service = _services[0];
            _services.RemoveAt(0);
            return service;
        }

        /// <summary>
        /// The function outs the number of adults in children in a combination. The pattern for the combination is xAyC where x is the number of adults and y is the number of children.
        /// </summary>
        public void GetPeopleFromCombinations(string combinations, out int adults, out int children)
        {
            adults = combinations[0] - '0';
            children = (int)combinations[2] - '0';
        }

        /// <summary>
        /// Calculates price based on given parameters
        /// </summary>
        public decimal CalculatePrice(string combination, int days, int pricePerDay, int pricePerAdult, int pricePerChild)
        {
            GetPeopleFromCombinations(combination, out int adults, out int children);
            return pricePerDay * days + pricePerAdult * adults + pricePerChild * children;
        }

        /// <summary>
        /// Generates combinations of adults and children. The pattern for the combination is xAyC where x is the number of adults and y is the number of children.
        /// </summary>
        private void GenerateCombinations()
        {
            _personCombinations = new List<string>();

            for (int adults = 1; adults <= 4; adults++)
            {
                for (int children = 0; children <= 4; children++)
                {
                    _personCombinations.Add($"{adults}A{children}C");
                }
            }
        }

        /// <summary>
        /// Prints the offer in console.
        /// </summary>
        /// <param name="offer"></param>
        private void PrintOffer(Offer offer)
        {
            Console.WriteLine("Check in date: " + offer.CheckInDate);
            Console.WriteLine("Stay Duration: " + offer.StayDurationNights);
            Console.WriteLine("Person combination: " + offer.PersonCombination);
            Console.WriteLine("Service Code: " + offer.ServiceCode);
            Console.WriteLine("Price: " + offer.Price);
            Console.WriteLine("Price Per Adult: " + offer.PricePerAdult);
            Console.WriteLine("Price Per Child: " + offer.PricePerChild);

            if (offer.ShowStrikePrice)
            {
                Console.WriteLine("Strike Price: " + offer.StrikePrice);
                Console.WriteLine("Strike Price Per Adult: " + offer.StrikePricePerAdult);
                Console.WriteLine("Strike Price Per Children: " + offer.StrikePricePerChild);
            }

            Console.WriteLine("Last updated: " + offer.LastUpdated);
        }

        /// <summary>
        /// Repopulates the service list.
        /// </summary>
        public void ResetServices()
        {
            _services = new List<string> {
                "HRO-ROLEIIS-MCL",
                "HRO-ROLEIIS-FBA",
                "HRO-ROLEIIS-FBB",
                "HRO-ROLEIIS-FBA-H",
                "HRO-ROLEIIS-FBC",
                "HRO-ROLEIIS-FFI-H",
                "HRO-ROVIEIS-MVE",
                "HRO-ROVIEIS-MVE-H",
                "HRO-ROVIEIS-FFH-H",
                "HRO-RONURIS-MVE",
                "HRO-RONURIS-MNE",
                "HRO-ROHENIS-MPR-2SZ",
                "HRO-ROHENIS-MPR-2SZ-H",
                "HRO-ROHENIS-MPR",
                "HRO-ROHENIS-MPR-H",
                "HRO-ROHENIS-LON",
                "HRO-ROHENIS-LJA",
                "HRO-ROHAMIS-MNE",
                "HRO-ROHAMIS-MNE-H",
                "HRO-ROLACIS-MVE",
                "HRO-ROHAMIS-MVE",
                "HRO-ROHAMIS-MVE-H",
                "HRO-ROLACIS-MJU",
                "HRO-ROLACIS-MVE-H",
                "HRO-ROLACIS-MPA",
                "HRO-ROLACIS-FFA",
                "HRO-ROLACIS-FFA-H",
                "HRO-RORUGIS-MVE",
                "HRO-RORUGIS-FFH",
                "HRO-ROLACIS-FFB",
                "HRO-ROBURIS-MEX",
                "HRO-ROBURIS-MEX-M",
                "HRO-RODORIS-MCL",
                "HRO-RODURIS-MVE-H",
                "HRO-RODURIS-MPR",
                "HRO-ROECKIS-MEX",
                "HRO-ROELBIS-LON",
                "HRO-ROELBIS-LJA",
                "HRO-ROESCIS-MPR-2SZ",
                "HRO-ROESSIS-MMA",
                "HRO-ROESSIS-MMA-H",
                "HRO-ROFRIIS-MCL",
                "HRO-ROFRIIS-MCL-H",
                "HRO-ROHUNIS-FFH",
                "HRO-ROHUNIS-FFH-H",
                "HRO-ROKISIS-MCL",
                "HRO-ROOYTIS-MEX",
                "HRO-ROTOSIS-MEX",
                "HRO-ROTOSIS-MEX-M",
                "HRO-ROWALIS-MVE",
                "HRO-ROWALIS-MVE-H",
                "HRO-ROWALIS-MCL",
                "HRO-ROWINIS-LAM",
                "HRO-ROWINIS-FFH"
            };
        }
    }
}
