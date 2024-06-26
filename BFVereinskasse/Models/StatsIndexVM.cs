using BFVereinskasse.Data;

namespace BFVereinskasse.Models
{
    public class StatsIndexVM
    {
        public IEnumerable<Zahlung> Payments { get; set; }
    }
}
