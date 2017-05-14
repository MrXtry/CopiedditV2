using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string Name { get; set; }

        public Byte[] Data { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ContentType { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
