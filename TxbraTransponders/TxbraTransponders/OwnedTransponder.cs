using System.ComponentModel.DataAnnotations;

namespace TxBraTransponders
{
    public class OwnedTransponder
    {
        [Key]
        public string TransponderId { get; set; }
        public string LicensePhone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}