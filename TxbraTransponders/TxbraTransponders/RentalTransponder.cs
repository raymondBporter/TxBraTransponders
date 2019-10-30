using System.ComponentModel.DataAnnotations;

namespace TxBraTransponders
{
    public class RentalTransponder
    {
        [Key]
        public string TransponderId { get; set; }
        public int SortId { get; set; }
        public string StickerId { get; set; }
        public string Owner { get; set; }
    }
}