using Consume_API_ASP_NET_CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Consume_API_ASP_NET_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public string apiBaseUrl;
        public HttpClientController(IConfiguration configuration)
        {
            this.configuration = configuration;

            apiBaseUrl = configuration.GetValue<string>("WebAPIBaseUrl");
        }

        [HttpPost("LoginwithJSON")]
        public IActionResult Login(modelLogin login)
        {
            try
            {
                //Consume API With Http Client Method Post
                var httpClient = new HttpClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync(apiBaseUrl + "api/User/Login", content).Result;
                var readResult = result.Content.ReadAsStringAsync().Result;
                var convertResult = JsonConvert.DeserializeObject<ResponseModel>(readResult);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { Token = convertResult.Data.Token } });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code),new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data });
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }
        
        [HttpPost("LoginWithFromData")]
        public IActionResult LoginWithFromData([FromForm] string username, [FromForm] string password)
        {
            try
            {

                //Consume API With Http Client Method Post
                var httpClient = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();
                content.Add(new StringContent(username), "username");
                content.Add(new StringContent(password), "password");

                var result = httpClient.PostAsync(apiBaseUrl + "api/User/LoginWithFromData", content).Result;
                var readResult = result.Content.ReadAsStringAsync().Result;
                var convertResult = JsonConvert.DeserializeObject<ResponseModel>(readResult);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { Token = convertResult.Data.Token } });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code),new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data });
                }
            }
            catch (Exception e)
            {
                string data = null;
                return StatusCode(500, new { Code = "500", Status = "False", Message = e.Message, Data = data });
            }
        }
        
        [HttpPost("LoginWithParam")]
        public IActionResult LoginWithParam(string username,string password)
        {
            try
            {

                //Consume API With Http Client Method Post
                var httpClient = new HttpClient();
                HttpContent content = null;
                var result = httpClient.PostAsync(apiBaseUrl + "api/User/LoginWithParam?username="+username+"&password="+password, content).Result;
                var readResult = result.Content.ReadAsStringAsync().Result;
                var convertResult = JsonConvert.DeserializeObject<ResponseModel>(readResult);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = new { Token = convertResult.Data.Token } });
                }
                else
                {
                    string data = null;
                    return StatusCode(int.Parse(convertResult.Code),new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = data });
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

                //Consume API With Http Client Method Get
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", tokenBearer);
                var result = httpClient.GetAsync(apiBaseUrl + "api/User/GetUser").Result;
                var readResult = result.Content.ReadAsStringAsync().Result;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelGet>(readResult);
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

                //Consume API With Http Client Method Get
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", tokenBearer);
                var result = httpClient.GetAsync(apiBaseUrl + "api/User/GetUserByUsername/" + username).Result;
                var readResult = result.Content.ReadAsStringAsync().Result;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelGetByUsername>(readResult);
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
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", tokenBearer);
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var result = httpClient.PutAsync(apiBaseUrl + "api/User/ChangePassword", content).Result;
                var readResult = result.Content.ReadAsStringAsync().Result;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelChangePassword>(readResult);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = convertResult.Data });
                }
                else if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", tokenBearer);
                var result = httpClient.DeleteAsync(apiBaseUrl + "api/User/DeleteUser/" + username).Result;
                var readResult = result.Content.ReadAsStringAsync().Result;
                var convertResult = JsonConvert.DeserializeObject<ResponseModelDelete>(readResult);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(new { Code = convertResult.Code, Status = convertResult.Status, Message = convertResult.Message, Data = convertResult.Data  });
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
