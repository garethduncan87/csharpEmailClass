using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace csharpEmailClass
{
    /// <summary>
    /// Email attachment object. Suitable for needing a List of attachments.
    /// </summary>
    public class EmailAttachment
    {
        public MemoryStream AttachmentMemoryStream;
        public FileInfo AttachmentFileInfo;
    }
}