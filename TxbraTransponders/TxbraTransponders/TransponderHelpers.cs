using System.Collections.Generic;
using System.Linq;

namespace TxBraTransponders
{
    public static class TransponderHelpers
    {
        public static List<string> AvailableRentalPools(this List<RentalTransponder> rentalTransponders)
            => rentalTransponders.Select(t => t.Owner).ToHashSet().ToList();
    }
}