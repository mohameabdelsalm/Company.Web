using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string FolderName)
        {
          //--Get Folder Path
          var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files" , FolderName);

           //Get File Name
           var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            //Combain FolderPath + FilePath
            var filePath = Path.Combine(folderPath, fileName);

            //Save File
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }
    }
}
