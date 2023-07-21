using IOMundoConsole.Models;
using IOMundoConsole.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMundoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContext dataContext = new DataContext();
            OfferRepository offerRepository = new OfferRepository(dataContext);
            DataGenerator dataGenerator = new DataGenerator(offerRepository);
            dataGenerator.GenerateOffers();
        }
    }
}
