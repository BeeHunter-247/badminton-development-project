namespace Badminton.Web.Interfaces
{
    public interface IFileRepository
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
    }
}
