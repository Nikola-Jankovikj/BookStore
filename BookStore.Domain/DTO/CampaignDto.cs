using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.DTO
{
    public class CampaignDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double MoneyRaised { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Guid CharityOrganizationId { get; set; }
        public CharityOrganizationDto? CharityOrganization { get; set; }
        public virtual ICollection<DonationDto>? Donations { get; set; }

        public double FullName
        {
            get
            {
                return Donations != null && Donations.Any() ? Donations.Select(x => x.Amount).Sum() : 0;
            }
        }
    }
}
