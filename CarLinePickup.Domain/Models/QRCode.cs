using CarLinePickup.Domain.Models.Interfaces;
using System;

namespace CarLinePickup.Domain.Models
{
    public class QRCode : ModelBase
    {
        public byte[] Code { get; set; }
    }
}
