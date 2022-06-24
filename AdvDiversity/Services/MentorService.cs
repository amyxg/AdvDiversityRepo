using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Mentors;
using Sabio.Models.Requests.Mentors;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class MentorService : IMentorService
    {
        IDataProvider _data = null;
        public MentorService(IDataProvider data)
        { _data = data; }


        public int AddMentor(MentorAddRequest model, int userId)
        {
            string procName = "[dbo].[Mentors_Insert]";

            int id = 0;

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col, userId);


                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                col.Add(idOut);

            }, returnParameters: delegate (SqlParameterCollection returnCol)
            {
                object rawIdobject = returnCol["@Id"].Value;

                int.TryParse(rawIdobject.ToString(), out id);
            });

            return id;
        }
        public Mentors GetMentorById(int id)
        {
            Mentors mentor = null;

            string procName = "[dbo].[Mentors_Select_ById]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int index = 0;
                mentor = SingleMentorMapper(reader, ref index);
            });
            return mentor;
        }
        public void UpdateMentor(MentorUpdateRequest model, int userId)
        {
            string procName = "[dbo].[Mentors_Update]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", model.Id);

                AddCommonParams(model, col, userId);

            }, returnParameters: null);

        }
        public Paged<Mentors> GetMentorsByPage(int pageIndex, int pageSize)
        {
            string procName = "[dbo].[Mentors_SelectAllPaginated]";
            Paged<Mentors> pagedList = null;
            List<Mentors> mentorList = null;
            int totalCount = 0;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection col)

            {
                col.AddWithValue("@PageIndex", pageIndex);
                col.AddWithValue("@PageSize", pageSize);

            }, delegate (IDataReader reader, short set)
            {
                int index = 0;

                Mentors mentor = SingleMentorMapper(reader, ref index);

                totalCount = reader.GetSafeInt32(index++);


                if (mentorList == null)
                {
                    mentorList = new List<Mentors>();
                }

                mentorList.Add(mentor);
            });

            if (mentorList != null)
            {
                pagedList = new Paged<Mentors>(mentorList, pageIndex, pageSize, totalCount);
            }

            return pagedList;
        }
        public void DeleteMentor(int id)
        {
            string procName = "[dbo].[Mentors_Delete_ById]";

            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@Id", id);
            });
        }
        private static void AddCommonParams(MentorAddRequest model, SqlParameterCollection col, int userId)
        {
            col.AddWithValue("@UserId", userId);
            col.AddWithValue("@Summary", model.Summary);
            col.AddWithValue("@Description", model.Description);
            col.AddWithValue("@SiteUrl", model.SiteUrl);
            col.AddWithValue("@Phone", model.Phone);
            col.AddWithValue("@IsApproved", model.IsApproved);
        }
        private static Mentors SingleMentorMapper(IDataReader reader, ref int index)
        {
            Mentors mentor = new Mentors();
            mentor.Id = reader.GetSafeInt32(index++);
            mentor.UserId = reader.GetSafeInt32(index++);
            mentor.Summary = reader.GetSafeString(index++);
            mentor.Description = reader.GetSafeString(index++);
            mentor.DateCreated = reader.GetSafeDateTime(index++);
            mentor.DateModified = reader.GetSafeDateTime(index++);
            mentor.SiteUrl = reader.GetSafeString(index++);
            mentor.Phone = reader.GetSafeString(index++);
            mentor.isApproved = reader.GetSafeBool(index++);

            return mentor;
        }
        public List<UserProfile> GetMenteesById(int id)
        {
            string procName = "[dbo].[MentorMatches_SelectById]";
            List<UserProfile> profileList = null;

            _data.ExecuteCmd(procName,
                inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@LoggedInMentorId", id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int startingIndex = 0;
                    UserProfile profile = SingleUserMentorMapper(reader, ref startingIndex);

                    if (profileList == null)
                    {
                        profileList = new List<UserProfile>();
                    }

                    profileList.Add(profile);

                }
                );

            return profileList;
        }
        private static UserProfile SingleUserMentorMapper(IDataReader reader, ref int startingIndex)
        {
            UserProfile profile = new UserProfile();
            profile.Id = reader.GetInt32(startingIndex++);
            profile.UserId = reader.GetInt32(startingIndex++);
            profile.FirstName = reader.GetSafeString(startingIndex++);
            profile.LastName = reader.GetSafeString(startingIndex++);
            profile.Mi = reader.GetSafeString(startingIndex++);
            profile.AvatarUrl = reader.GetSafeString(startingIndex++);
            return profile;
        }
    }
}
