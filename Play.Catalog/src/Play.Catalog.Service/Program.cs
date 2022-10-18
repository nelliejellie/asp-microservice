using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>{
    option.SuppressAsyncSuffixInActionNames = false;
});
// add a serializer to change guid to string in mongodb
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

// add a serializer to change date to string in mongodb
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

// serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>(); 

// get database that will be currently used
builder.Services.AddSingleton(serviceProvider => 
{
    var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
    var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
    return mongoClient.GetDatabase(builder.Configuration.GetSection("ServiceSetting")["ServiceName"]);
});

builder.Services.AddSingleton<IGenericRepository<Item>>(serviceProvider =>
{
    var database = serviceProvider.GetService<IMongoDatabase>();
    return new ItemsRepository<Item>(database, "items");
});


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
