using BFVereinskasse.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BFVereinskasse.Models
{
    public class StatsIndexVM
    {
        public IEnumerable<Zahlung> Payments { get; set; }
        public IEnumerable<Zahlung> HighestPayments => Payments.OrderByDescending(o => o.Betrag).Take(5).ToList();
        public IEnumerable<Zahlung> HighestNegativePayments => Payments.OrderBy(o => o.Betrag).Take(5).ToList();

        public string HighestJson
        {
            get
            {
                return 
                JsonSerializer.Serialize((HighestPayments), opts);

            }
        }
        public string SumIn => Payments.Where(o => o.Betrag >= 0).Sum(o => o.Betrag).ToString("c");
        public string SumOut => Payments.Where(o => o.Betrag < 0).Sum(o => o.Betrag).ToString("c");
        public string SumTotal => Payments.Sum(o => o.Betrag).ToString("c");

        public JsonSerializerOptions opts => new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        //public decimal[] Amounts { get { return Payments.Select(o => o.Betrag).ToArray(); } }
        public string Names { get { return JsonSerializer.Serialize(Payments.Select(o => o.Mitglied.Nachname).ToArray(), opts); } }
        public string Amounts { get { return JsonSerializer.Serialize(Payments.Select(o => o.Betrag).ToArray(), opts); } }
        public string PaymentsJson { get { return JsonSerializer.Serialize(Payments.ToArray(), opts); } }
    }
}
