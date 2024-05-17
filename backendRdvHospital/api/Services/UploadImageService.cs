using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;

namespace api.Services
{
    public class UploadImageService : IImageService
    {
        public string UploadImage(string role,IFormFile file)
        {
            //extentions
            List<string> ValidExtentions = [".jpg",".jpeg",".png"];
            string extention =Path.GetExtension(file.FileName);
            if(!ValidExtentions.Contains(extention)){
                throw new ArgumentException($"Extention is Not Valid ({string.Join(',',ValidExtentions)})")  ;
            }
            //file size
            long size = file.Length;
            if(size> 5 * 1024 * 1024){
                throw new ArgumentException("Max size can be 5mb");
            }
            //changing Name
            string fileName = Guid.NewGuid().ToString() +extention;
            string path ="";
            if(role.Equals("Doctor")){
                 path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Uploads\\Doctors");
            }
            if(role.Equals("Patient")){
                 path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Uploads\\Patients");
            }
            using FileStream stream = new FileStream(Path.Combine(path,fileName),FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }
    }

}