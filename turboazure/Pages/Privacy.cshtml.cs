using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace turbo_azure.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly DbDemoContext _context;
        public PrivacyModel(ILogger<PrivacyModel> logger, DbDemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }
       public async Task<IActionResult> OnPost(IFormFile myFile,string brand, string model,string year)
        {
            
            
            if (myFile == null || myFile.Length == 0)
                return BadRequest("No file selected");

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=azurestorageturboaz;AccountKey=VKgcOuA4sDC56jDm3tT7nlkB49c3MTUrRJyHhtECYnCkMiWv+URce1fHxXShococT/9zZq0EJapy+AStzwWkDA==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            string containerName = "containerturboaz";
           

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(myFile.FileName);

            using (var stream = myFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            Car car = new() { Brand = brand,Model = model,ImageUrl = blobClient.Uri.ToString().Replace(" ", "%"),Year=year}; 
            _context.Cars.Add(car);
            _context.SaveChanges();
            return RedirectToPage("Index"); 
        }
    }
}