using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AnyEntityClient
{
    public class WebApiHelper
    {
        private readonly HttpClient client = new HttpClient();

        public WebApiHelper()
        {
            client.BaseAddress = new Uri("http://localhost:7265/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "wcSPGhcjd5efL2VYj77NOA3Mg2s_RLKD19O_hQJ9Mztocy0m_1m8CJkDAOSpVLIycSntQeY45_xTN5DHfZzcysWlXhxiT4T1YafaKGfvbfnR_kCybPIF7qgQm5RR9jbbQ52V7BR5_gUWCAHRe5xcDjLf0_eTJASrC80mIaHWxoeXIzOqF4p3z5-mL8HL3pl8Pew4ky4PiLmVUDSWuHTOGC3ACfqRiTR2kmGkInq6DpEiefvGILp3Mgcrje4VFCaRr9Sb33S3-56szBCA4oEk_NxY2TTETNQ-_KVVqu4D5oM");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void ShowAnyEntity(AnyEntity entity)
        {
            Console.WriteLine($"Id: {entity.Id}\tDescription: {entity.Description}");
        }

        public async Task<AnyEntity[]> GetAllEntitiesAsync()
        {
            AnyEntity[] entities = null;
            HttpResponseMessage response = await client.GetAsync("api/AnyEntities");
            if (response.IsSuccessStatusCode)
            {
                entities = await response.Content.ReadAsAsync<AnyEntity[]>();
            }
            return entities;
        }

        public async Task<Uri> CreateAnyEntityAsync(AnyEntity entity)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/AnyEntities", entity);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public async Task<AnyEntity> GetAnyEntityAsync(string path)
        {
            AnyEntity entity = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                entity = await response.Content.ReadAsAsync<AnyEntity>();
            }
            return entity;
        }

        public async Task<AnyEntity> UpdateAnyEntityAsync(AnyEntity entity)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/AnyEntities/{entity.Id}", entity);
            response.EnsureSuccessStatusCode();
            entity = await response.Content.ReadAsAsync<AnyEntity>();
            return entity;
        }

        public async Task<HttpStatusCode> DeleteAnyEntityAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/AnyEntities/{id}");
            return response.StatusCode;
        }
    }
}
