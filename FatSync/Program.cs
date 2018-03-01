using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscUtils;
using DiscUtils.Complete;

namespace FatSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var target = Path.GetFullPath(args[0]);
            var source = Path.GetFullPath(args[1]);

            SetupHelper.SetupComplete();

            var volMgr = new VolumeManager();
            volMgr.AddDisk(VirtualDisk.OpenDisk(target, FileAccess.ReadWrite));
            var volInfo = volMgr.GetLogicalVolumes()[0];

            var fsInfo = FileSystemManager.DetectFileSystems(volInfo)[0];

            using (var fs = fsInfo.Open(volInfo, new FileSystemParameters()))
            {
                foreach (var file in Directory.GetFiles(source))
                {
                    var buf = File.ReadAllBytes(file);

                    var fileInfo = new FileInfo(file);
                    if (fs.FileExists(fileInfo.Name)) fs.DeleteFile(fileInfo.Name);

                    var x = (float)buf.Length / 512f;
                    var chunckCount = (byte)(x % 2 == 0 ? (byte)x : (byte)x + 1);

                    //index
                    using (var index = fs.OpenFile(fileInfo.Name, FileMode.CreateNew))
                    {
                        index.WriteByte(chunckCount);
                    }

                    //create chuncks directory
                    fs.CreateDirectory(fileInfo.Name.Replace(".", ""));
                    
                    //write all the chuncks
                    for (int i = 0; i < chunckCount; i++)
                    {
                        var chunckFile = Path.Combine(fileInfo.Name.Replace(".", ""), i.ToString());
                        if (fs.FileExists(chunckFile)) fs.DeleteFile(chunckFile);
                        using (var chunck = fs.OpenFile(chunckFile, FileMode.CreateNew))
                        {
                            if (i == chunckCount - 1)
                            {
                                chunck.Write(buf, i * 512, buf.Length - ((chunckCount - 1) * 512));
                            }
                            else
                            {
                                chunck.Write(buf, i * 512, 512);
                            }
                        }
                    }
                }
            }
        }
    }
}