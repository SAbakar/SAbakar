using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Principal.Divers.FileWriter
{
    public interface IFileHandler
    {
        Task<IActionResult> UploadFile(FichierModel fichierModel);        
    }

    public class FileHandler : IFileHandler
    {
        private readonly IFileWriter _imageWriter;
        public FileHandler(IFileWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<IActionResult> UploadFile(FichierModel fichierModel)
        {
            var result = "";
            if (fichierModel.IsImage)
            {
                await _imageWriter.UploadImage(fichierModel);
                return new ObjectResult(result);
            }
            await _imageWriter.UploadOtherFile(fichierModel);
            return new ObjectResult(result);
        }        
    }
}
