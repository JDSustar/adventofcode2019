using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace AdventOfCode2019
{
    public static class Day8
    {
        public static void ExecuteStarOne(string fileLocation = "PuzzleInput/Day8.txt")
        {
            string data = File.ReadAllText(fileLocation);

            var si = new SpaceImage(data, 25, 6);

            Logger.LogMessage(LogLevel.ANSWER, "8A: Check Digit: " + si.CalculateCheckDigit());
        }

        public static void ExecuteStarTwo(string fileLocation = "PuzzleInput/Day8.txt")
        {
            string data = File.ReadAllText(fileLocation);

            var si = new SpaceImage(data, 25, 6);

            Logger.LogMessage(LogLevel.ANSWER, "8B: Printed Message: ");
            Logger.LogMessage(LogLevel.ANSWER, si.PrintedImage);
        }
    }

    public class SpaceImage
    {
        public class Layer
        {
            public SpaceImage parent;

            public int[,] imageLayerData;

            public int UsedPixels => parent.Width * parent.Height;

            public Layer(SpaceImage parent, string data)
            {
                this.parent = parent;
                imageLayerData = new int[parent.Width, parent.Height];


                for (var h = 0; h < parent.Height; h++)
                {
                    for (var w = 0; w < parent.Width; w++)
                    {
                        imageLayerData[w, h] = int.Parse(data[0].ToString());
                        if (data.Length > 1) data = data.Substring(1);
                    }
                }
            }

            public Layer(SpaceImage parent)
            {
                this.parent = parent;
                imageLayerData = new int[parent.Width, parent.Height];
            }
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public string RawData { get; private set; }

        public string PrintedImage { get; private set; }

        public List<Layer> ImageLayers = new List<Layer>();

        public Layer FinalImage;

        public SpaceImage(string data, int width, int height)
        {
            RawData = data;
            Width = width;
            Height = height;

            BuildLayers();
            RenderImage();
        }

        private void BuildLayers()
        {
            string data = RawData;

            while (data.Length > 0)
            {
                ImageLayers.Add(new Layer(this, data));

                if (data.Length > ImageLayers.Last().UsedPixels)
                {
                    data = data.Substring(ImageLayers.Last().UsedPixels);
                }
                else
                {
                    data = "";
                }
            }
        }

        private void RenderImage()
        {
            FinalImage = new Layer(this);

            for (var w = 0; w < Width; w++)
            {
                for (var h = 0; h < Height; h++)
                {
                    int value = 2;

                    foreach (var il in ImageLayers)
                    {
                        int layerValue = il.imageLayerData[w, h];
                        if (layerValue != 2)
                        {
                            value = layerValue;
                            break;
                        }
                    }

                    FinalImage.imageLayerData[w, h] = value;
                }
            }

            StringBuilder sb = new StringBuilder();

            for (var h = 0; h < Height; h++)
            {
                for (var w = 0; w < Width; w++)
                {
                    if (FinalImage.imageLayerData[w, h] == 0)
                    {
                        sb.Append(" ");
                    }
                    else if (FinalImage.imageLayerData[w, h] == 1)
                    {
                        sb.Append("0");
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }

                sb.Append("\n");
            }

            PrintedImage = sb.ToString();
        }

        public Layer FindLayerWithLeast(int value)
        {
            SpaceImage.Layer checkLayer = null;
            int minZeros = Int32.MaxValue;

            foreach (var siImageLayer in this.ImageLayers)
            {
                int numZeros = 0;
                foreach (var i in siImageLayer.imageLayerData)
                {
                    if (i == value) numZeros++;
                }

                if (numZeros < minZeros)
                {
                    minZeros = numZeros;
                    checkLayer = siImageLayer;
                }
            }

            return checkLayer;
        }

        public int CalculateCheckDigit()
        {
            var checkLayer = this.FindLayerWithLeast(0);

            int numOnes = 0;
            int numTwos = 0;

            foreach (var i in checkLayer.imageLayerData)
            {
                if (i == 1) numOnes++;
                if (i == 2) numTwos++;
            }

            return numOnes * numTwos;
        }
    }
}
