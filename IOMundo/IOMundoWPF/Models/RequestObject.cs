using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMundoWPF.Models
{
    public class RequestObject
    {
        public DateTime CheckInDate { get; set; }
        public int Duration { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }

        public Credentials Credentials { get; set; }
    }
}
