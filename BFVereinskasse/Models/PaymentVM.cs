using BFVereinskasse.Data;
using System.ComponentModel.DataAnnotations;

namespace BFVereinskasse.Models
{
    public class PaymentVM
    {
        public List<Mitglied>? Members { get; set; }
        public PaymentVM(Zahlung payment)
        {
            Id = payment.Id;
            MemberId = payment.MitgliedId;
            Description = payment.Beschreibung;
            Amount = payment.Betrag;
            TimeStamp = payment.Datum;
        
        }
        public PaymentVM()
        {
            
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Mitglied")]
        public int? MemberId { get; set; }

        [Display(Name = "Beschreibung")]
        [MaxLength(50)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Pflichtfeld: Betrag")]
        [Display(Name = "Betrag")]
        //[DataType(DataType.Currency)] // geht ned
        public decimal? Amount { get; set; }

        [PastDate]
        [Display(Name = "Datum")]
        public DateTime? TimeStamp { get; set; }
        //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
        public string DateTimePickerValue { get => TimeStamp.Value.ToString("yyyy-MM-ddTHH:mm:ss"); }
    }
}
