using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOMundoConsole;
using IOMundoConsole.Models;
using IOMundoConsole.Repositories.Interfaces;
using IOMundoWPF.Models;

namespace IOMundoWPF
{
    public class SearchService
    {
        private IOfferRepository _offerRepository;

        public SearchService(IOfferRepository offerRepository) 
        { 
            _offerRepository = offerRepository; 
        }

        /// <summary>
        /// Searches for offers based on the request object.
        /// </summary>
        public async Task<List<Offer>> SearchAvailability(RequestObject requestObject)
        {
            if (requestObject == null) 
                throw new ArgumentNullException();

            if (!AuthenthicateUser(requestObject.Credentials))
                throw new Exception("Bad credentials");

            string combination = CreateCombination(requestObject.Adults, requestObject.Children);

            return await _offerRepository.GetAvailable(requestObject.CheckInDate, (uint)requestObject.Duration, combination);
        }

        /// <summary>
        /// Transforms string into a valid combination. The pattern for the combinatin is xAyC where x is the number of adults and y the number of children
        /// </summary>
        private string CreateCombination(int adults, int children)
        {
            return $"{adults}A{children}C";
        }

        /// <summary>
        /// Simulates a login. Returns true if UserName and Password are not empty.
        /// </summary>
        private bool AuthenthicateUser(Credentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.Password))
                return false;

            return true;
        }
    }
}
