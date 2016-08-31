/*
 * Copyright (c) 2016, Brent Rubell 

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace httpclartest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NSFW Tagger, powered by Clarifai.com");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            Console.WriteLine("Potentially NSFW URL: {Added for debug}");
            // string remoteUri = Console.ReadLine();
            string vikingDude = "https://i.imgur.com/lnzsgv9.jpg";
            string nudeWoman = "https://i.imgur.com/aJXq544.jpg";
            string token = "YOUR_API_TOKEN"
            string modelURL = "http://api.clarifai.com/v1/tag/?model=nsfw-v0.1&url=" + nudeWoman + "&access_token=" + token;
            // make a webclient 
            WebClient myWebClient = new WebClient();
            // download & save in a databuffer)
            byte[] myDataBuffer = myWebClient.DownloadData(modelURL);
            // full data buffer -> string
            string download = Encoding.ASCII.GetString(myDataBuffer);
            // DEBUG 
           // Console.WriteLine(download);

            ClarNSFW(download);
         
        }
        public static void ClarNSFW (string download)
        {

            int iop = download.IndexOf("classes");
            int iof = download.IndexOf("docid_str");
            string strn = download.Substring(iop, iof - iop);

            //Enable for dbg 
            Console.WriteLine("Parsed Text: ");
            Console.WriteLine(" ");
            Console.WriteLine(strn);


            int iof2 = download.IndexOf("nsfw");
            int iop2 = download.IndexOf("sfw");
            // dbg 
            // Console.WriteLine("{0}, {1}", iof2, iop2);


            string[] numbers = Regex.Split(download, @"\D+");

            int beforeDeciOne = int.Parse(numbers[1]);
            int beforeDeciTwo = int.Parse(numbers[3]);
            double afterDeciOne = double.Parse(numbers[2]);
            double afterDeciTwo = double.Parse(numbers[4]);

            if (iof2 > iop2)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Image has been deemed SAFE FOR WORK");
                Console.WriteLine("     probability: 0 . {0} %", afterDeciTwo);
            }
            else if (iop2 > iof2)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Image has been deemed NOT SAFE FOR WORK");
                Console.WriteLine("     probability: 0 . {0} %", afterDeciOne);
            }
            else
            {
                Console.WriteLine("Couldnt tell...");
            }
        }
    }
}
    

