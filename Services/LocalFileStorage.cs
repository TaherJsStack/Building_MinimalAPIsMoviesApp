
namespace Building_MinimalAPIsMoviesApp.Services
{
    public class LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor) : IFileStorage
    {
        public Task Delete(string? route, string container)
        {
            if (string.IsNullOrEmpty(route)) 
            { 
                return Task.CompletedTask;
            }

            var filename = Path.GetFileName(route);
            var fileDirectory = Path.Combine(env.WebRootPath, container, filename);

            if (File.Exists(fileDirectory)) 
            { 
                File.Delete(fileDirectory);
            }
            return Task.CompletedTask;
        }

        public async Task<string> Store(string container, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder)) 
            {
                Directory.CreateDirectory(folder);
            }

            string route = Path.Combine(folder, fileName);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                await File.WriteAllBytesAsync(route, content);
            }

            var scheme = httpContextAccessor.HttpContext!.Request.Scheme;
            var host = httpContextAccessor?.HttpContext?.Request.Host;
            var url = $"{scheme}://{host}";
            var urlFile = Path.Combine(url, container, fileName).Replace("\\", "/");

            return urlFile;

        }
    }
}
