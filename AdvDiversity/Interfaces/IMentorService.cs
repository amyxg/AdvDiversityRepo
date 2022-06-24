using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Mentors;
using Sabio.Models.Requests.Mentors;
using System.Collections.Generic;

namespace Sabio.Services.Interfaces
{
    public interface IMentorService
    {
        int AddMentor(MentorAddRequest model, int userId);
        void DeleteMentor(int id);
        Mentors GetMentorById(int id);
        Paged<Mentors> GetMentorsByPage(int pageIndex, int pageSize);
        void UpdateMentor(MentorUpdateRequest model, int userId);
        List<UserProfile> GetMenteesById(int id);

    }
}