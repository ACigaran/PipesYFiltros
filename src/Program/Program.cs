using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using Ucu.Poo.Twitter;
using Ucu.Poo.Cognitive;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            // EJERCICIO 1
            PipeNull pipeNull = new PipeNull();
            PipeSerial pipeSerial2 = new PipeSerial(new FilterNegative(), pipeNull);
            PipeSerial pipeSerial1 = new PipeSerial(new FilterGreyscale(), pipeSerial2);
            
            PictureProvider provider = new PictureProvider();
            //IPicture picture = provider.GetPicture(@"luke.jpg");
            IPicture picture = provider.GetPicture(@"PathToImageToLoad.jpg");
            IPicture result = pipeSerial1.Send(picture);
            //provider.SavePicture(result, @"luke1.jpg");
            
            // EJERCICIO 2, GUARDAR LA IMAGEN
            provider.SavePicture(result, @"PathToImageToSave.jpg");
            
            // EJERCICIO 3, PUBLICAR EN TWITTER 
            var twitter = new TwitterImage();
            Console.WriteLine(twitter.PublishToTwitter("Pipes - Filters", @"PathToImageToSave.jpg"));
            
            // EJERCICIO 4, 
            CognitiveFace cognitiveFace = new CognitiveFace(markFaces: true, boxColor: System.Drawing.Color.Red); 
            cognitiveFace.Recognize(@"PathToImageToLoad.jpg");
            FilterCognitiveFaceDetection faceDetectionFilter = new FilterCognitiveFaceDetection(cognitiveFace); 

            PipeConditionalBranching conditionalPipe = new PipeConditionalBranching(faceDetectionFilter);

            PipeSerial facePipe = new PipeSerial(new FilterBlurConvolution(), pipeNull); 
            PipeSerial noFacePipe = new PipeSerial(new FilterNegative(), pipeNull);

            conditionalPipe.SetTrueBranch(facePipe);
            conditionalPipe.SetFalseBranch(noFacePipe);

            IPicture processedImage = conditionalPipe.Send(picture);
            provider.SavePicture(processedImage, @"PathToImageToSave.jpg");

            Console.WriteLine(twitter.PublishToTwitter("Pipes - Filters - Parte 4", @"PathToImageToSave.jpg"));
        }
    }
}
