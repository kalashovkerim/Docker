using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace turbo_azure.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DbDemoContext _context;
        public IndexModel(ILogger<IndexModel> logger, DbDemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=azurestorageturboaz;AccountKey=VKgcOuA4sDC56jDm3tT7nlkB49c3MTUrRJyHhtECYnCkMiWv+URce1fHxXShococT/9zZq0EJapy+AStzwWkDA==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            string containerName = "containerturboaz";
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = containerClient.GetBlobs();
           

            List<Car> cars = _context.Cars.ToList();
            ViewData["BlobURLs"] = cars;
        }

        
    }
}