using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace TwitterClone.Models
{
    public class UploadImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload file!")]
        public IFormFile ImageToUpload { get; set; }

    }
}