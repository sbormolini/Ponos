using Microsoft.Azure.Cosmos;

namespace Bos.Todo.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // CosmosDB info
        var accountEndpoint = builder.Configuration.GetSection("CosmosDB").GetValue<string>("AccountEndpoint");
        var databaseName = builder.Configuration.GetSection("CosmosDB").GetValue<string>("DatabaseName");
        var containerName = builder.Configuration.GetSection("CosmosDB").GetValue<string>("ContainerName");
        var key = builder.Configuration.GetSection("CosmosDB").GetValue<string>("Key");

        // Add services to the container.
        builder.Services.AddSingleton<ICosmosDbService>(
            InitializeCosmosClientInstanceAsync(accountEndpoint, databaseName, containerName, key).GetAwaiter().GetResult());

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // cors
        //policy.WithOrigins("http://localhost:5166", "https://localhost:7265")
        app.UseCors(policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// Creates a Cosmos DB database and a container with the specified partition key. 
    /// </summary>
    /// <returns></returns>
    private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(
        string accountEndpoint, 
        string databaseName, 
        string containerName, 
        string key)
    {
        //// for local dev
        //CosmosClientOptions cosmosClientOptions = new CosmosClientOptions()
        //{
        //    HttpClientFactory = () =>
        //    {
        //        HttpMessageHandler httpMessageHandler = new HttpClientHandler()
        //        {
        //            ServerCertificateCustomValidationCallback = (req, cert, chain, errors) => true
        //        };
        //        return new HttpClient(httpMessageHandler);
        //    },
        //    ConnectionMode = ConnectionMode.Gateway
        //};

        //CosmosClient client = new(accountEndpoint, key, cosmosClientOptions);
        CosmosClient client = new(accountEndpoint, key);
        CosmosDbService cosmosDbService = new(client, databaseName, containerName);
        DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

        return cosmosDbService;
    }
}