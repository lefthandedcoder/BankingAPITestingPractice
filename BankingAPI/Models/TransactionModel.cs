using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models
{
    public class TransactionModel
    {
        public int UserId { get; set; }

        [Required]
        [TwoDecimalPlaces]
        public decimal Amount { get; set; }
    }
}
