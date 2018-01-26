using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosGL.System.Imaging.Formats
{
    public interface IImage
    {
        Image Read(byte[] bytes);
    }
}
