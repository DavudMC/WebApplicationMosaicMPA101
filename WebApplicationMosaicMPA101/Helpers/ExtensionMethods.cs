
namespace WebApplicationMosaicMPA101.Helpers
{
    public static class ExtensionMethods
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool CheckSize(this IFormFile file, int mb)
        {
            return file.Length < mb * 1024 * 1024;
        }
        public static async Task<string> FileUploadAsync(this IFormFile file, string folderPath)
        {
            string uniquefileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf(".")); //dasdsuaidsauindsauisadidsiaImagegdgfdgfdddddddd.jpg
            string path = Path.Combine(folderPath, uniquefileName);
            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);
            return uniquefileName;
        }
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
