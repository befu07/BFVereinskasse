using BFVereinskasse.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BFVereinskasse.Models
{
    public class CreatePaymentVM
    {
        public List<Mitglied>? Members { get; set; }

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
        public DateTime TimeStamp { get; set; }
        //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
        public string DateTimePickerValue { get; set; } = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

    }



    public class PastDateAttribute : ValidationAttribute
    {
        public PastDateAttribute()
        {
            const string defaultErrorMessage = "geht ned";
            ErrorMessage ??= defaultErrorMessage;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                //return ValidationResult.Success;
                return new ValidationResult("Required");
            }
            if (!DateTime.TryParse(value.ToString(), out DateTime date))
            {
                return new ValidationResult(
                    FormatErrorMessage(validationContext.DisplayName));
            }
            if (date > DateTime.Now)
            {
                return new ValidationResult("Date must be now or in the past");
            }
            return ValidationResult.Success;
        }
    }
}
