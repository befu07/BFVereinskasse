using BFVereinskasse.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BFVereinskasse.Models
{
    public class StatsIndexVM
    {
        public IEnumerable<Zahlung> Payments { get; set; }

        public JsonSerializerOptions opts => new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles };
        //public decimal[] Amounts { get { return Payments.Select(o => o.Betrag).ToArray(); } }
        public string Names { get{ return JsonSerializer.Serialize(Payments.Select(o => o.Mitglied.Nachname).ToArray(), opts); } }
        public string Amounts { get{ return JsonSerializer.Serialize(Payments.Select(o => o.Betrag).ToArray(), opts); } }
        public string PaymentsJson { get{ return JsonSerializer.Serialize(Payments.ToArray(), opts); } }
    }
}
