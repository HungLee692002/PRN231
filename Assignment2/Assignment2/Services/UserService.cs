using Assignment2.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Assignment2.Services
{
    public class UserService
    {
        public async Task<RequestResponse<User>> UserLogin(User user)
        {
            RequestResponse<User>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync("http://localhost:5008/api/User/GetUserLogin", user))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                User u = JsonConvert.DeserializeObject<User>(data);

                                response = new RequestResponse<User>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<User>
                            {
                                Success = false,
                                Message = await res.Content.ReadAsStringAsync()
                            };
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RequestResponse<User>> UserRegister(User user)
        {
            RequestResponse<User>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync("http://localhost:5008/api/User/AddUser", user))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                User u = JsonConvert.DeserializeObject<User>(data);

                                response = new RequestResponse<User>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<User>
                            {
                                Success = false,
                                Message = await res.Content.ReadAsStringAsync()
                            };
                        }
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
