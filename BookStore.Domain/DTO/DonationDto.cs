using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Domain.DTO
{
    public class DonationDto
    {
        public double Amount { get; set; }
        public DateOnly DonationDate { get; set; }
        public Guid CampaignId { get; set; }
        public CampaignDto? Campaign { get; set; }
        public string? DonatorId { get; set; }
        //public Donator? Donator { get; set; }
    }
}
