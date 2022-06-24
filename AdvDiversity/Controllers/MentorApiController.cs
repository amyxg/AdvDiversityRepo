using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.Mentors;
using Sabio.Models.Requests.Mentors;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/mentors")]
    [ApiController]
    public class MentorApiController : BaseApiController
    {
        private IMentorService _service = null;
        private IAuthenticationService<int> _authService = null;
        public MentorApiController(IMentorService service
            , ILogger<MentorApiController> logger
            , IAuthenticationService<int> authorService) : base(logger)
        {
            _service = service;
            _authService = authorService;

        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> AddMentor(MentorAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                int id = _service.AddMentor(model, userId);

                ItemResponse<int> response = new ItemResponse<int> { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                base.Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }

            return result;
        }
        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<Mentors>> GetById(int id)
        {

            int iCode = 200;
            BaseResponse response = null;

            try
            {
                Mentors mentor = _service.GetMentorById(id);

                if (mentor == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("No Records Found");
                }
                else
                {
                    response = new ItemResponse<Mentors>() { Item = mentor };
                }
            }
            catch (SqlException sqlEx)
            {
                iCode = 500;
                response = new ErrorResponse(sqlEx.Message);
            }
            catch (ArgumentException argEx)
            {
                iCode = 500;
                response = new ErrorResponse(argEx.Message);
            }

            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(iCode, response);

        }
        [HttpPut("{id:int}")]
        public ActionResult<ItemResponse<SuccessResponse>> Update(MentorUpdateRequest model)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _service.UpdateMentor(model, userId);

                response = new SuccessResponse();
            }

            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(iCode, response);
        }
        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<Mentors>>> GetMentorsByPage(int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<Mentors> pagedList = _service.GetMentorsByPage(pageIndex, pageSize);

                if (pagedList == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Mentors>> response = new ItemResponse<Paged<Mentors>>();
                    response.Item = pagedList;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }

            return result;
        }
        [HttpDelete("{id:int}")]
        public ActionResult<ItemResponse<SuccessResponse>> DeleteMentor(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                _service.DeleteMentor(id);

                response = new SuccessResponse();
            }

            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(iCode, response);

        }
        [HttpGet("mentees/{id:int}")]
        public ActionResult<ItemsResponse<UserProfile>> GetMenteesById(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                List<UserProfile> profileList = _service.GetMenteesById(id);
                if (profileList == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("No Records Found");
                }
                else
                {
                    response = new ItemsResponse<UserProfile> { Items = profileList };
                }

            }
            catch (Exception ex)
            {
                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(iCode, response);

        }
    }
}
