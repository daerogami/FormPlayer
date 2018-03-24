using System;
using System.Collections.Generic;
using System.IO;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Quantum_Gate_Tests
{
    [TestClass]
    public class ExecutionSandbox
    {
        private static string content_directory = @"E:\";
        private static ICollection<string> output = new List<string>();

        [TestMethod]
        public void ValidationSandbox()
        {
            DirSearch(content_directory);
            using (File.Create(@".\content.txt"))
            {
            }
            foreach (var filePath in output)
            {
                using (var file = File.AppendText(@".\content.txt"))
                {
                    file.WriteLine(filePath);
                }
            }
            Assert.IsTrue(true);
        }

        private static void DirSearch(string sDir)
        {
            try
            {
                foreach (var d in Directory.GetDirectories(sDir))
                {
                    foreach (var f in Directory.GetFiles(d))
                    {
                        output.Add(f.Replace(content_directory, ""));
                    }
                    DirSearch(d);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        public void ImageProcessorSandbox()
        {
            var file = @"E:\CONTENT\CURSOR1.BMP";
            var size = new Size(150, 0);
            var photoBytes = File.ReadAllBytes(file);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 100 };
            using (var inStream = new MemoryStream(photoBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (var imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                            .Resize(size)
                            .Format(format)
                            .Save(outStream);
                    }
                    // Do something with the stream.
                    var fileStream = File.Create(@".\CURSOR1.BMP");
                }
            }
        }

    }
}