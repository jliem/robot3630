//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Ccr.Core;
using Microsoft.Dss.Core;
using Microsoft.Dss.Core.Attributes;
using Microsoft.Dss.ServiceModel.Dssp;
using Microsoft.Dss.ServiceModel.DsspServiceBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using W3C.Soap;
using camera = CoroWare.Robotics.Services.CoroBotCamera.Proxy;
using System.Drawing;
using System.IO;
using Microsoft.Ccr.Adapters.WinForms;
using System.Drawing.Imaging;


namespace Robotics.CoroBot.ImageProcessor
{
    
    
    /// <summary>
    /// Implementation class for ImageProcessor
    /// </summary>
    [DisplayName("ImageProcessor")]
    [Description("The ImageProcessor Service")]
    [Contract(Contract.Identifier)]
    public class ImageProcessorService : DsspServiceBase
    {
        
        /// <summary>
        /// _state
        /// </summary>
        private ImageProcessorState _state = new ImageProcessorState();
        private ImageForm form = null;
        private ImageProcessorResult results;
        private int picNum = 0;
        private Get get;

        
        /// <summary>
        /// _main Port
        /// </summary>
        [ServicePort("/imageprocessor", AllowMultipleInstances=false)]
        private ImageProcessorOperations _mainPort = new ImageProcessorOperations();

        [Partner("Camera", Contract = camera.Contract.Identifier, CreationPolicy = PartnerCreationPolicy.UseExisting, Optional = false)]
        private camera.CoroBotCameraOperations _cameraPort = new camera.CoroBotCameraOperations();
        
        /// <summary>
        /// Default Service Constructor
        /// </summary>
        public ImageProcessorService(DsspServiceCreationPort creationPort) : 
                base(creationPort)
        {
        }
        
        /// <summary>
        /// Service Start
        /// </summary>
        protected override void Start()
        {
			base.Start();

            WinFormsServicePort.Post(new RunForm(CreateForm));
        }

        private System.Windows.Forms.Form CreateForm()
        {
            form = new ImageForm(_mainPort);
            return form;
        }

        private void CameraHandler(camera.CoroBotCameraState state)
        {
            MemoryStream stream = new MemoryStream(state.imageData);
            Bitmap image = (Bitmap)Bitmap.FromStream(stream);
            //BitmapData rawData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            Bitmap display = new Bitmap(image.Width, image.Height);
            //BitmapData displayData = display.LockBits(new Rectangle(0, 0, display.Width, display.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int[, ,] data = HSVSlow(image, display);
            //int[,,] data = LowPassAndHSV(rawData, displayData);
            //List<Point> floorHeight = FindFloor(data, displayData);
            results = FindFolders(data, display, 240, 240);
            //display.UnlockBits(displayData);
            string text = results.Folders.Count.ToString();
            foreach (Folder f in results.Folders)
            {
                text += "||X:" + f.X + ", Area: " + f.Area + ", Color: " + f.Color;
            }
            UpdateFormText(text);
            UpdateFormImage(display);
        }

        private void GetImageFromFile()
        {

            String number = "" + picNum;
            if (picNum < 10)
            {
                number = "0" + picNum;
            }
            String filename = "C:\\robotpics\\image" + number + ".bmp";
            picNum++;
            Bitmap image = new Bitmap(filename);
            //UpdateFormImage(image);
            Bitmap display = new Bitmap(image.Width, image.Height);
            int[, ,] data = HSVSlow(image, display);
            ImageProcessorResult results = FindFolders(data, display, 340, 300);
            //ImageProcessorResult results = FindFolders(data, display, 210, 210);
            string text = results.Folders.Count.ToString();
            foreach (Folder f in results.Folders)
            {
                text += "||X:" + f.X + ", Area: " + f.Area + ", Color: " + f.Color;
            }
            UpdateFormText(text);
            UpdateFormImage(display);
        }

        private List<Point> FindFloor(int[, ,] data, BitmapData display)
        {
            List<Point> points = new List<Point>();
            for (int x = 0; x < data.GetLength(0); x += 40)
            {
                int prevH = data[x, data.GetLength(1) - 1, 0];
                int prevS = data[x, data.GetLength(1) - 1, 1];
                int curH = 0;
                int curS = 0;
                for (int y = data.GetLength(1) - 2; y > 0; y--)
                {
                    curH = data[x, y, 0];
                    curS = data[x, y, 1];
                    if (curS < 25 || curS > 150)
                    {
                        points.Add(new Point(x, y));
                        SetData(display, x, y, 255, 0, 0);
                        SetData(display, x, y - 1, 255, 0, 0);
                        SetData(display, x, y - 2, 255, 0, 0);
                        SetData(display, x, y - 3, 255, 0, 0);
                        
                        break;
                    }
                    else
                    {
                        prevH = curH;
                        prevS = curS;
                    }
                }
            }
            return points;
        }

        private bool isFolder(int[] hsv)
        {
            //Green Profile
            if (hsv[0] >= 150 && hsv[0] <= 190
                && hsv[1] >= (int)(.75 * 255) && hsv[1] <= (int)(1 * 255)
                && hsv[2] >= (int)(.2 * 255) && hsv[2] <= (int)(.60 * 255))
            {
                return true;
            }
            //Red Profile
            if (hsv[0] >= 320 && hsv[0] <= 360
                && hsv[1] >= (int)(.60 * 255) && hsv[1] <= (int)(1 * 255)
                && hsv[2] >= (int)(.40 * 255) && hsv[2] <= (int)(1 * 255))
            {
                return true;
            }            
            //Red Cabinet
            if (hsv[0] >= 0 && hsv[0] <= 10
                && hsv[1] >= (int)(.90 * 255) && hsv[1] <= (int)(1 * 255)
                && hsv[2] >= (int)(.30 * 255) && hsv[2] <= (int)(.70 * 255))
            {
                return true;
            }
            //Yellow Profile
            if (hsv[0] >= 50 && hsv[0] <= 75
                && hsv[1] >= (int)(.30 * 255) && hsv[1] <= (int)(.70 * 255)
                && hsv[2] >= (int)(.65 * 255) && hsv[2] <= 255)
            {
                return true;
            }
            //if (hsv[1] >= 100 && hsv[2] >= 20)
            //{
            //    return true;
            //}
            return false;
        }

        private ImageProcessorResult FindFolders(int[, ,] data, Bitmap display, int y0, int y1)
        {
            results = new ImageProcessorResult();
            List<int> potentialFolders = new List<int>();
            //find potential folder seed points
            for (int x = 0; x < data.GetLength(0); x++)
            {
                int y = y0 + (x * (y1 - y0) / (data.GetLength(0)));
                //if (data[x,y,1] > 100)
                int[] hsv = new int[3];
                hsv[0] = data[x, y, 0];
                hsv[1] = data[x, y, 1];
                hsv[2] = data[x, y, 2];
                if(isFolder(hsv))
                {
                    int newX = x;
                    while (newX < data.GetLength(0) && isFolder(hsv))
                    {
                        hsv[0] = data[newX, y, 0];
                        hsv[1] = data[newX, y, 1];
                        hsv[2] = data[newX, y, 2];
                        newX++;
                        y = y0 + (x * (y1 - y0) / (data.GetLength(0))); ;
                    }
                    newX--;
                    if(newX > x)
                    {
                        potentialFolders.Add(x);
                        potentialFolders.Add(newX);
                    }
                    x = newX;
                }
            }
            //expand each seed and gather data
            for (int i = 0; i < potentialFolders.Count; i += 2)
            {
                int x0 = potentialFolders[i];
                int x1 = potentialFolders[i+1];
                int startX = (x0 + x1)/2;
                int startY = y0 + (startX * (y1 - y0) / (data.GetLength(0)));
                int[] tops = new int[x1 - x0 + 1];
                int[] bottoms = new int[x1 - x0 + 1];
                for (int j = x0; j <= x1; j++)
                {
                    tops[j - x0] = -1;
                    bottoms[j - x0] = -1;
                }
                int avgH = 0;
                int avgX = 0;
                int avgY = 0;
                int count = 0;
                int minX = startX;
                int maxX = startX;
                int minY = startY;
                int maxY = startY;
                LinkedList<Point> points = new LinkedList<Point>();
                points.AddFirst(new LinkedListNode<Point>(new Point(startX, startY)));
                bool[,] visited = new bool[x1 - x0 + 1, 480];
                while (points.Count > 0)
                {
                    Point cur = points.First.Value;
                    points.RemoveFirst();
                    if(visited[cur.X - x0, cur.Y])
                    {
                        continue;
                    }
                    visited[cur.X - x0, cur.Y] = true;
                    //SetData(display, cur.X, cur.Y, 0, 255, 0);
                    display.SetPixel(cur.X, cur.Y, Color.FromArgb(0, 0, 255));
                    count++;
                    avgH += data[cur.X, cur.Y, 0];
                    avgX += cur.X;
                    avgY += cur.Y;
                    if (tops[cur.X - x0] == -1)
                    {
                        tops[cur.X - x0] = cur.Y;
                        bottoms[cur.X - x0] = cur.Y;
                    }
                    else
                    {
                        //remember y=0 is top of image
                        if (tops[cur.X - x0] > cur.Y) tops[cur.X - x0] = cur.Y;
                        if (bottoms[cur.X - x0] < cur.Y) bottoms[cur.X - x0] = cur.Y;
                    }
                    if (cur.X < minX) minX = cur.X;
                    if (cur.X > maxX) maxX = cur.X;
                    if (cur.Y < minY) minY = cur.Y;
                    if (cur.Y > maxY) maxY = cur.Y;
                    AddAdjacentPoints(points, data, cur.X, cur.Y, x0, x1, avgH/count);
                }
                tops = Trim(tops);
                bottoms = Trim(bottoms);
                /*for (int j = 0; j < tops.Length; j++)
                {
                    LogInfo("t,b: " + tops[j] + "," + bottoms[j]);
                }*/
                double topCorr = GetCorrelation(tops);
                double bottomCorr = GetCorrelation(bottoms);
                double height = GetAverage(bottoms) - GetAverage(tops);
                //LogInfo("TopCorr: " + topCorr + "  BottomCorr: " + bottomCorr + "  Height: " + height);
                /*if (topCorr > .8 && bottomCorr > .8 && height > 15)
                {
                    string color = TestAverageH(avgH/count);
                    if (color != "None")
                    {
                        Folder f = new Folder();
                        f.Color = color;
                        f.Area = count;
                        f.X = avgX/count;
                        results.Folders.Add(f);
                    }
                }*/
                int difX = maxX - minX;
                int difY = maxY - minY;
                if (count > 525)
                {
                    float ratio = ((float)difY) / difX;
                    if (ratio > .7 && ratio < 5)
                    {
                        string color = TestAverageH(avgH / count);
                        Folder f = new Folder();
                        f.Color = color;
                        f.Area = count;
                        f.X = avgX / count;
                        results.Folders.Add(f);
                    }
                }
            }
            
            return results;
        }

        private string TestAverageH(int avgH)
        {
            string result = "None";
            int bestDif = 40;
            int difR = AngleDif(avgH, 358);
            int difG = AngleDif(avgH, 160);
            int difY = AngleDif(avgH, 63);
            LogInfo("avgH: " + avgH);
            LogInfo("Difs: " + difR + " " + difG + " " + difY);
            if (avgH < 10)
            {
                result = "RedCabinet";
                bestDif = AngleDif(avgH, 358);
            }
            else if (AngleDif(avgH, 358) < bestDif)
            {
                result = "Red";
                bestDif = AngleDif(avgH, 358);
            }
            if (AngleDif(avgH, 160) < bestDif)
            {
                result = "Green";
                bestDif = AngleDif(avgH, 160);
            }
            if (AngleDif(avgH, 63) < bestDif)
            {
                result = "Yellow";
                bestDif = AngleDif(avgH, 63);
            }
            return result;
        }

        private int AngleDif(int angle1, int angle2)
        {
            int dif1 = angle1 - angle2;
            int dif2 = angle2 - angle1;
            if (dif1 < 0)
            {
                dif1 += 360;
            }
            if (dif2 < 0)
            {
                dif2 += 360;
            }
            return Math.Min(dif1, dif2);
        }

        private int[] Trim(int[] data)
        {
            int start = 0;
            int end = data.Length - 1;
            while (data[start] == -1)
            {
                start++;
            }
            while (data[end] == -1)
            {
                end--;
            }
            int[] result = new int[end - start + 1];
            for (int i = start; i <= end; i++)
            {
                result[i - start] = data[i];
            }
            return result;
        }

        private double GetAverage(int[] data)
        {
            int len = data.Length;
            if (len == 0) return 0;

            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum / len;
        }

        private double GetVariance(int[] data)
        {
            int len = data.Length;
            double avg = GetAverage(data);
            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += Math.Pow((data[i] - avg), 2);
            }
            return sum / len;
        }

        private double GetStdev(int[] data)
        {
            return Math.Sqrt(GetVariance(data));
        }

        private double GetCorrelation(int[] data)
        {
            int[] xVals = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                xVals[i] = i;
            }

            double avgX = GetAverage(xVals);
            double stdevX = GetStdev(xVals);
            double avgY = GetAverage(data);
            double stdevY = GetStdev(data);
            //LogInfo("avgx,devx,avgy,devy: " + avgX + " " + stdevX + " " + avgY + " " + stdevY);
            if (stdevY < 2) return 1;
            int len = data.Length;

            double covXY = 0;
            for (int i = 0; i < len; i++)
            {
                covXY += (xVals[i] - avgX) * (data[i] - avgY);
            }
            covXY /= len;
            return covXY / (stdevX * stdevY);
        }

        private void AddAdjacentPoints(LinkedList<Point> points, int[, ,] data, int curX, int curY, int xMin, int xMax, int avgH)
        {
            int curH = data[curX,curY,0];
            int curS = data[curX,curY,1];
            int curV = data[curX,curY,2];

            if (curX > xMin)
            {
                if (TestSimilarity(curH, curS, curV, data[curX - 1, curY, 0], data[curX - 1, curY, 1], data[curX - 1, curY, 2], avgH))
                {
                    points.AddFirst(new LinkedListNode<Point>(new Point(curX - 1, curY)));
                }
                if (curY > 0)
                {
                    if (TestSimilarity(curH, curS, curV, data[curX - 1, curY - 1, 0], data[curX - 1, curY - 1, 1], data[curX - 1, curY - 1, 2], avgH))
                    {
                        points.AddFirst(new LinkedListNode<Point>(new Point(curX - 1, curY - 1)));
                    }
                }
                if (curY < data.GetLength(1) - 1)
                {
                    if (TestSimilarity(curH, curS, curV, data[curX - 1, curY + 1, 0], data[curX - 1, curY + 1, 1], data[curX - 1, curY + 1, 2], avgH))
                    {
                        points.AddFirst(new LinkedListNode<Point>(new Point(curX - 1, curY + 1)));
                    }
                }
            }
            if (curX < xMax)
            {
                if (TestSimilarity(curH, curS, curV, data[curX + 1, curY, 0], data[curX + 1, curY, 1], data[curX + 1, curY, 2], avgH))
                {
                    points.AddFirst(new LinkedListNode<Point>(new Point(curX + 1, curY)));
                }
                if (curY > 0)
                {
                    if (TestSimilarity(curH, curS, curV, data[curX + 1, curY - 1, 0], data[curX + 1, curY - 1, 1], data[curX + 1, curY - 1, 2], avgH))
                    {
                        points.AddFirst(new LinkedListNode<Point>(new Point(curX + 1, curY - 1)));
                    }
                }
                if (curY < data.GetLength(1) - 1)
                {
                    if (TestSimilarity(curH, curS, curV, data[curX + 1, curY + 1, 0], data[curX + 1, curY + 1, 1], data[curX + 1, curY + 1, 2], avgH))
                    {
                        points.AddFirst(new LinkedListNode<Point>(new Point(curX + 1, curY + 1)));
                    }
                }
            }
            if (curY > 0)
            {
                if (TestSimilarity(curH, curS, curV, data[curX, curY - 1, 0], data[curX, curY - 1, 1], data[curX, curY - 1, 2], avgH))
                {
                    points.AddFirst(new LinkedListNode<Point>(new Point(curX, curY - 1)));
                }
            }
            if (curY < data.GetLength(1) - 1)
            {
                if (TestSimilarity(curH, curS, curV, data[curX, curY + 1, 0], data[curX, curY + 1, 1], data[curX, curY + 1, 2], avgH))
                {
                    points.AddFirst(new LinkedListNode<Point>(new Point(curX, curY + 1)));
                }
            }
        }

        private bool TestSimilarity(int h0, int s0, int v0, int h1, int s1, int v1, int avgH)
        {
            bool result = true;
            if (Math.Abs(h0 - h1) > 7) result = false;
            if (Math.Abs(h0 - avgH) > 6) result = false;
            if (Math.Abs(s0 - s1) > 10) result = false;
            if (Math.Abs(v0 - v1) > 10) result = false;
            return result;
        }

        private int[, ,] HSVSlow(Bitmap data, Bitmap display)
        {
            int[, ,] result = new int[display.Width, display.Height, 3];
            for (int y = 0; y < data.Height; y++)
            {
                for (int x = 0; x < data.Width; x++)
                {
                    Color pixel = data.GetPixel(x, y);
                    int b = pixel.B;
                    int g = pixel.G;
                    int r = pixel.R;
                    int h = CalculateH(r, g, b);
                    int s = CalculateS(r, g, b);
                    int v = CalculateV(r, g, b);
                    //if (x % 20 == 0)  LogInfo("HSV: " + h + " " + s + " " + v);
                    display.SetPixel(x, y, Color.FromArgb(r, g, b));
                    result[x, y, 0] = h;
                    result[x, y, 1] = s;
                    result[x, y, 2] = v;
                }
            }
            return result;
        }

        private int[, ,] LowPassAndHSV(BitmapData data, BitmapData display)
        {
            int[,,] result = new int[display.Width, display.Height, 3];

            #region
            //calculate pixel offsets to average
            /*int size = 1;
            int[,] offsets = new int[(2*size + 1)*(2*size+1), 2];
            int index = 0;
            for (int i = 0; i < (2*size+1); i++)
            {
                for (int j = 0; j < (2 * size + 1); j++)
                {
                    offsets[index, 0] = j - size;
                    offsets[index, 1] = i - size;
                    index++;
                }
            }*/
            #endregion

            //loop through pixels averaging appropriate ones, then convert to HSV
            unsafe
            {
                byte* imgPtr = (byte*)(data.Scan0);
                //LogInfo("Width: " + data.Width + " , Height: " + data.Height + ", Stride: " + data.Stride);
                for (int y = 0; y < data.Height; y++)
                {
                    for (int x = 0; x < data.Width; x++)
                    {
                        #region
                        /*int rTotal = 0;
                        int gTotal = 0;
                        int bTotal = 0;
                        int count = 0;
                        for (int i = 0; i < offsets.GetLength(0); i++)
                        {
                            int tx = x + offsets[i, 0];
                            int ty = y + offsets[i, 1];
                            if (tx >= 0 && tx < display.Width && ty >= 0 && ty < display.Height)
                            {
                                rTotal += *imgPtr;
                                imgPtr++;
                                gTotal += *imgPtr;
                                imgPtr++;
                                bTotal += *imgPtr;
                                imgPtr++;
                                count++;
                            }
                        }*/
                        #endregion
                        LogInfo(x + ", " + y);
                        int b = *imgPtr;
                        LogInfo("B:" + b);
                        imgPtr++;
                        int g = *imgPtr;
                        LogInfo("G:" + g);
                        imgPtr++;
                        int r = *imgPtr;
                        LogInfo("R:" + r);
                        imgPtr++;
                        //if(x % 20 == 0) LogInfo("RGB: " + r + " " + g + " " + b);
                        int h = CalculateH(r, g, b);
                        int s = CalculateS(r, g, b);
                        int v = CalculateV(r, g, b);
                        //if (x % 20 == 0)  LogInfo("HSV: " + h + " " + s + " " + v);
                        SetData(display, x, y, r, g, b);
                        result[x, y, 0] = h;
                        result[x, y, 1] = s;
                        result[x, y, 2] = v;
                    }
                }
            }
            //LogInfo("Finished loop.");
            return result;
        }

        private unsafe void SetData(BitmapData data, int x, int y, int r, int g, int b)
        {
            byte* ptr = (byte*)data.Scan0;
            ptr += y * data.Stride;
            ptr += 3 * x;
            *ptr = (byte)b;
            ptr++;
            *ptr = (byte)g;
            ptr++;
            *ptr = (byte)r;
        }

        private unsafe int CalculateV(int r, int g, int b)
        {
            int max = r;
            if (max < g) max = g;
            if (max < b) max = b;
            return max;
        }

        private unsafe int CalculateS(int r, int g, int b)
        {
            int max = r;
            if (max < g) max = g;
            if (max < b) max = b;

            int min = r;
            if (min > g) min = g;
            if (min > b) min = b;

            if (max == 0) return 0;
            else return (255 - ((255 * min) / max));
        }

        private unsafe int CalculateH(int r, int g, int b)
        {
            int max = r;
            if (max < g) max = g;
            if (max < b) max = b;

            int min = r;
            if (min > g) min = g;
            if (min > b) min = b;

            if (max == min) return 0;
            if (max == r && g >= b) return ((60 * (g - b)) / (max - min));
            if (max == r && g < b) return ((60 * (g - b)) / (max - min) + 360);
            if (max == g) return ((60 * (b - r)) / (max - min) + 120);
            else return ((60 * (r - g)) / (max - min) + 240);
        }

        private void UpdateFormImage(Bitmap image)
        {
            WinFormsServicePort.Post(new FormInvoke(delegate()
            {
                form.UpdateImage(image);
            }));
        }

        private void UpdateFormText(string text)
        {
            WinFormsServicePort.Post(new FormInvoke(delegate()
            {
                form.UpdateText(text);
            }));
        }

        private void GetImage()
        {
            Activate(Arbiter.Choice(
                _cameraPort.Get(),
                CameraHandler,
                delegate(Fault f) { LogError(f); }
            ));
        }
        
        [ServiceHandler(ServiceHandlerBehavior.Exclusive)]
        public virtual IEnumerator<ITask> GetHandler(Get g)
        {
            results = null;
            get = g;
            GetImage(); //From Corobot Camera
            //GetImageFromFile(); //From File
            Activate(Arbiter.Receive(false, TimeoutPort(1000), TimerHandler));
            yield break;
        }

        public void TimerHandler(DateTime t)
        {
            if (results == null)
            {
                Activate(Arbiter.Receive(false, TimeoutPort(1000), TimerHandler));
            }
            else
            {
                get.ResponsePort.Post(results);
            }
        }
    }
}