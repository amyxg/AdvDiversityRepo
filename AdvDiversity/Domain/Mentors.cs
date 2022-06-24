using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Mentors
{
    public class Mentors
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SiteUrl { get; set; }
        public string Phone { get; set; }
        public bool isApproved { get; set; }

    }
}
