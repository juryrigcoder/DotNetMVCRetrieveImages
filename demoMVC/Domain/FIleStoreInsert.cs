using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;
using System.Linq;

namespace demoMVC.Domain
{
    public class FIleStoreInsert
    {
        public static string Gallery
        {
            get { return "$/Gallery/"; }
        }

        public static void insertImages()
        {
            using (var db = new LiteDatabase(DbConnection.FileStorageLocation))
            {
                var path = @"C:\Users\Learning Clip\Documents\DotNetMVCInsertImagedb\demoMVC\Images\";
                var filePaths = Directory.GetFiles(path, "*.jpg");
                List<string> list = new List<string>();
                list.AddRange(filePaths);
                foreach (var file in list)
                {
                    var ext = Path.GetExtension(file);
                    var filename = Guid.NewGuid();
                    db.FileStorage.Upload(Gallery + filename + ext, file);
                }
            }
        }

        public static List<string> ImageList(string DIrectoryName)
        {
            var listed = new List<string>();
            using (var db = new LiteDatabase(DbConnection.FileStorageLocation))
            {
                var files = db.FileStorage.Find(DIrectoryName);
                listed.AddRange(files.Select(str => str.Id));
            }
            return listed;
        }

        public static Dictionary<byte[], string> StreamToSingleFile(string fileName)
        {
            using (var db = new LiteDatabase(DbConnection.FileStorageLocation))
            {
                var imageName = db.FileStorage.FindById(fileName);
                using (var imageBytes = new MemoryStream())
                {
                    imageName.CopyTo(imageBytes);
                    var imageResult = new Dictionary<byte[], string>() {{imageBytes.ToArray(), imageName.MimeType}};
                    return imageResult;
                }
            }
        }

}
}