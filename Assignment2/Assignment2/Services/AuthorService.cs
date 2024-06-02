using Assignment2.Models;
using Newtonsoft.Json;

namespace Assignment2.Services
{
    public class AuthorService
    {
        public async Task<RequestResponse<Author>> GetAuthor()
        {
            RequestResponse<Author>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync("http://localhost:5008/api/Author/GetAuthors"))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                List<Author> u = JsonConvert.DeserializeObject<List<Author>>(data);

                                response = new RequestResponse<Author>
                                {
                                    items = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Author>
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

        public async Task<RequestResponse<Author>> GetAuthorById(int id)
        {
            RequestResponse<Author>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync($"http://localhost:5008/api/Author/GetAuthorById/{id}"))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                Author u = JsonConvert.DeserializeObject<Author>(data);

                                response = new RequestResponse<Author>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Author>
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

        public async Task<RequestResponse<Author>> AddAuthor(Author author)
        {
            RequestResponse<Author>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync("http://localhost:5008/api/Author/AddAuthor", author))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                Author u = JsonConvert.DeserializeObject<Author>(data);

                                response = new RequestResponse<Author>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Author>
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

        public async Task<RequestResponse<Author>> DeleteAuthor(int id)
        {
            RequestResponse<Author>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.DeleteAsync($"http://localhost:5008/api/Author/DeleteAuthor/{id}"))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                response = new RequestResponse<Author>
                                {
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Author>
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

        public async Task<RequestResponse<Author>> UpdateAuthor(Author author)
        {
            RequestResponse<Author>? response = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PutAsJsonAsync("http://localhost:5008/api/Author/UpdateAuthor", author))
                    {
                        if (res.IsSuccessStatusCode)
                        {
                            using (HttpContent content = res.Content)
                            {
                                string data = await content.ReadAsStringAsync();

                                Author u = JsonConvert.DeserializeObject<Author>(data);

                                response = new RequestResponse<Author>
                                {
                                    item = u,
                                    Success = true
                                };
                            }
                        }
                        else
                        {
                            response = new RequestResponse<Author>
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
