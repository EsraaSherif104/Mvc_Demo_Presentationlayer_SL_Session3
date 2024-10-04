using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace DemoPL.Helpers
{
    public static class DocumentSetting
    {
        //fun upload
        public static string UploadFile(IFormFile file,string FolderName)
        {
            //1-get located folder path
            // string FolderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\Files\\" + FolderName;
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",FolderName);

            //2.get  file name and make it unique
            string FileName =$"{Guid.NewGuid()}{file.FileName}";
            //3-get file path [folder path +filename]
             string FilePath=Path.Combine(FolderPath,FileName);
            //4-save file as streams
          using  var fs=new FileStream(FilePath, FileMode.Create); 
            file.CopyTo(fs);

            //5-return fille name
            return FileName;


        }

        //delete
        public static void DeleteFile(string FileNmae,string FolderName)
        {
            //1-get file path
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileNmae);

                //2-check if file exists or
                if(File.Exists(FilePath))
                {
                //if exists remove
                File.Delete(FilePath);

                }
        }

    }
}
