using Consume_API_ASP_NET_CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Consume_API_ASP_NET_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestSharpController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public string apiBaseUrl;
        public RestSharpController(IConfiguration configuration)
        {
            this.configuration = configuration;

            apiBaseUrl = configuration.GetValue<string>("WebAPIBaseUrl");
        }

        [HttpPost("LoginwithJSON")]
        public IActionResult Login(modelLogin login)
        {
            try
            {
                //Consume API With restSharp Method Post
                var client = new RestClient(apiBaseUrl + "api/User/Login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(login), ParameterType.RequestBody);
                IRestResponse result = client.Execute(request);
                string rawResponse = result.Content;
                var convertResult = JsonConvert.DeserializeObject<ResponseModel>(rawResponse);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { Token = convertResult.Data.Token } });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code), new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data });
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }

        //Not Running
        /*[HttpPost("LoginWithFromData")]
        public IActionResult LoginWithFromData([FromForm] string username, [FromForm] string password)
        {
            try
            {

                //Consume API With restSharp Method Post
                var client = new RestClient(apiBaseUrl + "api/User/Login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("username", username);
                request.AddParameter("password", password);
                IRestResponse result = client.Execute(request);
                string rawResponse = result.Content;
                var convertResult = JsonConvert.DeserializeObject<ResponseModel>(rawResponse);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { Token = convertResult.Data.Token } });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code), new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data });
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }*/

        [HttpPost("LoginWithParam")]
        public IActionResult LoginWithParam(string username, string password)
        {
            try
            {

                //Consume API With RestSharp Method Post
                var client = new RestClient(apiBaseUrl + "api/User/LoginWithParam?username=" + username + "&password=" + password);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                IRestResponse result = client.Execute(request);
                string rawResponse = result.Content;
                var convertResult = JsonConvert.DeserializeObject<ResponseModel>(rawResponse);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { Token = convertResult.Data.Token } });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code), new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data });
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetUser")]
        public ActionResult GetUser()
        {
            try
            {
                // Get Token Bearer
                var tokenBearer = Request.Headers["Authorization"].ToString();

                //Consume API With RestSharp Method Get
                var client = new RestClient(apiBaseUrl + "api/User/GetUser");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", tokenBearer);
                IRestResponse result = client.Execute(request);
                string rawResponse = result.Content;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelGet>(rawResponse);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { convertResult.Data } });
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    string data = null;
                    return StatusCode(401, new { Code = "401", Status = "False", Message = "Unauthorized", Data = data });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code), new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data }); ;
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetUserByUsername")]
        public ActionResult GetUserByUsername(string username)
        {
            try
            {
                // Get Token Bearer
                var tokenBearer = Request.Headers["Authorization"].ToString();

                //Consume API With RestSharp Method Get
                var client = new RestClient(apiBaseUrl + "api/User/GetUserByUsername/" + username);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("authorization", tokenBearer);
                IRestResponse result = client.Execute(request);
                string rawResponse = result.Content;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelGetByUsername>(rawResponse);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { convertResult.Data } });
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    string data = null;
                    return StatusCode(401, new { Code = "401", Status = "False", Message = "Unauthorized", Data = data });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code), new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data }); ;
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(modelChangePassword model)
        {
            try
            {
                // Get Token Bearer
                var tokenBearer = Request.Headers["Authorization"].ToString();

                //Consume API With Http Client Method Put
                var client = new RestClient(apiBaseUrl + "api/User/ChangePassword");
                client.Timeout = -1;
                var request = new RestRequest(Method.PUT);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(model), ParameterType.RequestBody);
                IRestResponse result = client.Execute(request);
                string rawResponse = result.Content;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelChangePassword>(rawResponse);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = convertResult.Data });
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    string data = null;
                    return StatusCode(401, new { Code = "401", Status = "False", Message = "Unauthorized", Data = data });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code), new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data });
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("DeleteUser")]
        public ActionResult DeleteUser(string username)
        {
            try
            {
                // Get Token Bearer
                var tokenBearer = Request.Headers["Authorization"].ToString();

                //Consume API With Http Client Method Delete
                var client = new RestClient(apiBaseUrl + "api/User/DeleteUser/" + username);
                client.Timeout = -1;
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("authorization", tokenBearer);
                IRestResponse result = client.Execute(request);
                string rawResponse = result.Content;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelDelete>(rawResponse);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = convertResult.Data });
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    string data = null;
                    return StatusCode(401, new { Code = "401", Status = "False", Message = "Unauthorized", Data = data });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code), new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data }); ;
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }
    }
}
