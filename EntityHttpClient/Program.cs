using AnyEntityClient;
using System;
using System.Threading.Tasks;

namespace EntityHttpClient
{
    class Program
    {
        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            WebApiHelper webApiHelper = new WebApiHelper();

            try
            {
                //Create AnyEntity
                AnyEntity entity = new AnyEntity
                {
                    Id = "100",
                    Description = "HttpClient Entity"
                };

                var url = await webApiHelper.CreateAnyEntityAsync(entity);
                Console.WriteLine($"Создание {url}");

                //Get AnyEntity
                entity = await webApiHelper.GetAnyEntityAsync(url.PathAndQuery);
                webApiHelper.ShowAnyEntity(entity);

                //Update AnyEntity
                Console.WriteLine("Обновление");
                entity.Description = "Updating HttpClient Entity";
                await webApiHelper.UpdateAnyEntityAsync(entity);

                //Get the updated product
                entity = await webApiHelper.GetAnyEntityAsync(url.PathAndQuery);
                webApiHelper.ShowAnyEntity(entity);

                //Delete AnyEntity
                var statusCode = await webApiHelper.DeleteAnyEntityAsync(entity.Id);
                Console.WriteLine($"Удаление (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}