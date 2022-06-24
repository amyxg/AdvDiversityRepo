using Sabio.Models;
using Sabio.Models.Domain.Appointment;
using Sabio.Models.Requests.Appointments;
using System.Collections.Generic;

namespace Sabio.Services
{
    public interface IAppointmentService
    {
        void Delete(int id);
        Paged<Appointment> GetAll(int pageIndex, int pageSize);
        Paged<Appointment> GetAppts(int pageIndex, int pageSize, int userId);
        int MenteeCreate(AppointmentAddRequest model, int userId);
        int MentorCreate(AppointmentAddRequest model, int userId);
        void Update(AppointmentUpdateRequest model);
        void CreateRelation(UserRelationAddRequest model, int userId);
        List<UserRelation> GetRelation(int userId);
        void DeleteRelation(int menteeId, int mentorId);
    }
}