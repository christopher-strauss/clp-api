using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CarLinePickup.Domain.Models.VPIC;

namespace CarLinePickup.Domain.Services.Comparers
{
    public class MakeComparer : IEqualityComparer<Make>
    {
        public bool Equals(Make make1, Make make2)
        {
            if (make1.Id == make2.Id)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Make obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
