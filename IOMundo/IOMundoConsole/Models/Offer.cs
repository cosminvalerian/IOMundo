using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMundoConsole.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public uint StayDurationNights { get; set; }
        public string PersonCombination { get; set; }
        public string ServiceCode { get; set; }
        public decimal Price { get; set; }
        public decimal PricePerAdult { get; set; }
        public decimal PricePerChild { get; set; }
        public decimal StrikePrice { get; set; }
        public decimal StrikePricePerAdult { get; set; }
        public decimal StrikePricePerChild { get; set; }
        public bool ShowStrikePrice { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
