using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Appointments
{
    public class AppointmentAddRequest
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int MentorId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int MenteeId { get; set; }

        [Required]
        public DateTime ApptDateTime { get; set; }

        [Required]
        [Range(1, 3)]
        public int ApptTypeId { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string AppointmentUrl { get; set; }

        [Required]
        [Range(1, 3)]
        public int ApptStatusId { get; set; }

        [Required]
        public bool IsFirstMeeting { get; set; }

    }
}

