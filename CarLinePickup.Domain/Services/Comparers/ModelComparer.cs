using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CarLinePickup.Domain.Models.VPIC;

namespace CarLinePickup.Domain.Services.Comparers
{
    public class ModelComparer : IEqualityComparer<Model>
    {
        public bool Equals(Model model1, Model model2)
        {
            if (model1.Id == model2.Id)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode([DisallowNull] Model obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
