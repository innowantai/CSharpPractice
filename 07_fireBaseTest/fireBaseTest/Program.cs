using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.IO;
namespace fireBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new
            { 
                Name = "123",
                Value = "456",

            });

            var request = WebRequest.Create("https://savetrying.firebaseio.com/.json"); 
            //request.Method = "POST";
            //request.ContentType = "application/json";
            //var buffer = Encoding.UTF8.GetBytes(json);
            //request.ContentLength = buffer.Length;
            //request.GetRequestStream().Write(buffer, 0, buffer.Length);
            var response = request.GetResponse();
            json = (new StreamReader(response.GetResponseStream())).ReadToEnd();
            Console.WriteLine(json); 
        }
    }
}
