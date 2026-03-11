using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

var connectionString = "mongodb://sa:password@localhost:27017/?authSource=admin";

var client = new MongoClient(connectionString);

var database = client.GetDatabase("testdb");

Console.WriteLine("Connection successful!");

// collection
var collection = database.GetCollection<Customer>("customers");

// insert
var customer = new Customer
{
    Name = "John Doe",
    Age = 30,
    Email = "john@example.com"
};

collection.InsertOne(customer);

Console.WriteLine("Customer inserted");

// read
var customers = collection.Find(_ => true).ToList();

foreach (var c in customers)
{
    Console.WriteLine($"Name: {c.Name}, Age: {c.Age}, Email: {c.Email}");
}

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";

    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Email { get; set; } = "";
}