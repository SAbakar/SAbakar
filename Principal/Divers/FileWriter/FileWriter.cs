using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Principal.Divers.FileWriter
{
    public interface IFileWriter
    {
        Task<string> UploadImage(FichierModel fichierModel);
        Task<string> UploadOtherFile(FichierModel fichierModel);
    }

    public class FileWriter : IFileWriter
    {        
        public async Task<string> UploadImage(FichierModel fichierModel)
        {
            if (CheckIfImageFile(fichierModel.Fichier))
            {
                return await WriteFile(fichierModel, "images");
            }
            return "Invalid image file";
        }
        
        public async Task<string> UploadOtherFile(FichierModel fichierModel)
        {
            return await WriteFile(fichierModel,"documents");
        }
        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            //return true;
            return ImageWriterHelper.GetImageFormat(fileBytes) != ImageWriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="f"></param>
        /// <param name="dossierDestination"></param>
        /// <returns></returns>

        public async Task<string> WriteFile(FichierModel f,string dossierDestination)
        {
            string fileName;
            try
            {
                //var extension = "." + f.Fichier.FileName.Split('.')[f.Fichier.FileName.Split('.').Length - 1];
                //fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.
                fileName = f.NomFichier;// + extension; 
                string nomDossier = f.Dossier ?? "";

                nomDossier = nomDossier.Trim() == "" ? nomDossier : $"\\{nomDossier}\\";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploaded\\"+ dossierDestination + nomDossier);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filepath = Path.Combine(path,  fileName);

                

                using (var bits = new FileStream(filepath, FileMode.Create))
                {
                    await f.Fichier.CopyToAsync(bits);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return fileName;
        }        
    }

    public class Fichier
    {
        public string Dossier { get; set; }
        public string NomFichier { get; set; }
    }

    public class FichierModel
    {
        public IFormFile Fichier { get; set; }        
        public bool IsImage { get; set; }
        public string Dossier { get; set; }
        public string NomFichier { get; set; }
    }
}
