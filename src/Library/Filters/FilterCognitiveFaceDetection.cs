using System;
using Ucu.Poo.Cognitive;

namespace CompAndDel.Filters
{
    public class FilterCognitiveFaceDetection : IFilter
    {
        private CognitiveFace cognitiveFace;

        public FilterCognitiveFaceDetection(CognitiveFace cognitiveFace)
        {
            this.cognitiveFace = cognitiveFace;
        }

        public IPicture Filter(IPicture image)
        {
            string imagePath = @"PathToImageToLoad.jpg";
            cognitiveFace.Recognize(imagePath);

            return image;
        }
    }
}