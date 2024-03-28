using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/images")]
public class ImagesController : ControllerBase
{
    private readonly string ImagesFolderPath = "./images/";

    [HttpGet("{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        try
        {
            var imagePath = Path.Combine(ImagesFolderPath, imageName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var image = System.IO.File.OpenRead(imagePath);

            return File(image, "image/webp");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya seçilmedi.");
            }

            if (Path.GetExtension(file.FileName).ToLower() != ".webp")
            {
                return BadRequest("Lütfen .webp uzantılı bir resim dosyası yükleyin.");
            }

            var uniqueFileName = GetUniqueFileName(file.FileName);
            var filePath = Path.Combine(ImagesFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { FileName = uniqueFileName });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Resim yükleme hatası: {ex.Message}");
        }
    }

    [HttpDelete("{imageName}")]
    public IActionResult DeleteImage(string imageName)
    {
        try
        {
            var imagePath = Path.Combine(ImagesFolderPath, imageName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Silinecek resim dosyası bulunamadı.");
            }

            System.IO.File.Delete(imagePath);

            return Ok("Resim başarıyla silindi. Silinen dosya adı: " + imagePath);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Resim silme hatası: {ex.Message}");
        }
    }

    private string GetUniqueFileName(string fileName)
    {
        string uniqueName = $"img_{DateTime.Now.Ticks}";
        string extension = Path.GetExtension(fileName);
        string guidPart = Guid.NewGuid().ToString();

        string uniqFileName = $"{uniqueName}_{guidPart}{extension}";
        return uniqFileName;
    }
}
