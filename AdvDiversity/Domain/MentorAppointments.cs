using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Domain.Appointment
{
    public class MentorAppointments
    {
        public int Id { get; set; }

        public int MentorId { get; set; }

        public int MenteeId { get; set; }

        public DateTime ApptDateTime { get; set; }

        public int ApptTypeId { get; set; }

        public string ApptType { get; set; }

        public string Description { get; set; }

        public string AppointmentUrl { get; set; }

        public int ApptStatusId { get; set; }

        public string ApptStatus { get; set; }

        public bool IsFirstMeeting { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

    }
}
