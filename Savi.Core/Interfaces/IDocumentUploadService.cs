using Microsoft.AspNetCore.Http;
using Savi.Data.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savi.Core.Interfaces
{
    public interface IDocumentUploadService
    {
        Task<DocumentUploadResult> UploadFileAsync(string documentContent);
        Task<DocumentUploadResult> UploadImageAsync(IFormFile image);
    }
}
