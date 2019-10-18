using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleApp1
{
    class Program
    {
        private static CloudTable employees;

        static void Main(string[] args)
        {
            // Setup the connection
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DB Endpoint");
            
            // Init the table client
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();

            // Get a handler to the Employees table
            employees = tableClient.GetTableReference("Employees");

            // Add the data to the table
            var employeeEntity = new EmployeeEntity("John", "Doe");
            TableOperation insertop = TableOperation.Insert(employeeEntity);

            try
            {
                InsertData(insertop).Wait();
            } catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }
        }

        private static async Task InsertData(TableOperation insertop)
        {
            await employees.ExecuteAsync(insertop);

            throw new NotImplementedException();
        }
    }

    public class EmployeeEntity : TableEntity
    {
        public EmployeeEntity(string firstName, string lastName)
        {
            this.PartitionKey = "staff";
            this.RowKey = firstName + " " + lastName ;
        }
    }
}
