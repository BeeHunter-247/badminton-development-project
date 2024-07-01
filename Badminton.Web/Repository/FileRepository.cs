using Badminton.Web.Interfaces;

namespace Badminton.Web.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly IWebHostEnvironment _env;

        public FileRepository(IWebHostEnvironment env)
        {
            _env = env;
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = _env.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var extension = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if(!allowedExtensions.Contains(extension))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + extension;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch(Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }
    }
}
