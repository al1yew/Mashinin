namespace Mashinin.Helpers
{
    public static class FileHelper
    {
        public async static Task<string> CreateAsync(this IFormFile file, IWebHostEnvironment env, params string[] folders)
        {
            string shortGuid = Guid.NewGuid().ToString("N").Substring(0, 6);
            string timePart = DateTime.Now.ToString("HHmmssfff");

            string fileName = shortGuid + "_" + timePart + "_" + file.FileName.Trim();

            string path = Path.Combine(env.WebRootPath);

            foreach (string folder in folders)
            {
                path = Path.Combine(path, folder);
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public static void DeleteFile(IWebHostEnvironment env, string fileName, params string[] folders)
        {
            string path = Path.Combine(env.WebRootPath);

            foreach (string folder in folders)
            {
                path = Path.Combine(path, folder);
            }

            path = Path.Combine(path, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
