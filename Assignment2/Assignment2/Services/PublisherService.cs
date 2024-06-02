using Assignment2.Models;
using Newtonsoft.Json;

namespace Assignment2.Services
{
    public class PublisherService
    {
        public async Task<RequestResponse<Publisher>> GetPublishers()
        {
            RequestResponse<Publisher>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync("http://localhost:5008/api/Publisher/GetPublishers"))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                List<Publisher> u = JsonConvert.DeserializeObject<List<Publisher>>(data);

                                response = new RequestResponse<Publisher>
                                {
                                    items = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Publisher>
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

        public async Task<RequestResponse<Publisher>> GetPublisherById(int id)
        {
            RequestResponse<Publisher>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync($"http://localhost:5008/api/Publisher/GetPublisherById/{id}"))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                Publisher u = JsonConvert.DeserializeObject<Publisher>(data);

                                response = new RequestResponse<Publisher>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Publisher>
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

        public async Task<RequestResponse<Publisher>> AddPublisher(Publisher pub)
        {
            RequestResponse<Publisher>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync("http://localhost:5008/api/Publisher/AddPublisher", pub))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                Publisher u = JsonConvert.DeserializeObject<Publisher>(data);

                                response = new RequestResponse<Publisher>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Publisher>
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

        public async Task<RequestResponse<Publisher>> DeletePublisher(int id)
        {
            RequestResponse<Publisher>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.DeleteAsync($"http://localhost:5008/api/Publisher/DeletePublisher/{id}"))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                response = new RequestResponse<Publisher>
                                {
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Publisher>
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

        public async Task<RequestResponse<Publisher>> UpdatePublisher(Publisher author)
        {
            RequestResponse<Publisher>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PutAsJsonAsync("http://localhost:5008/api/Publisher/UpdatePublisher", author))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                Publisher u = JsonConvert.DeserializeObject<Publisher>(data);

                                response = new RequestResponse<Publisher>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Publisher>
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
