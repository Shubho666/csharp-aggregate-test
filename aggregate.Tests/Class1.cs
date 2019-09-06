using System;
using System.IO;

using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace aggregate.Tests
{
    public static class Class1
    {
        [Fact]
        public static void ispresent()
        {
            aggregate_gdp.Program.aggregator();
            bool flag = false;
            if (File.Exists("../../../../actual-output.json")) flag = true;
            Assert.True(flag);


        }

        [Fact]
        public static void isnonempty()
        {
            //aggregate_gdp.Program.aggregator();
            //var reader = new StreamReader(File.OpenRead("../../../../actual-output.json"));
            //List<string> searchList = new List<string>();
            //while (!reader.EndOfStream)
            //{
            //    var line = reader.ReadLine();
            //    searchList.Add(line);
            //}
            //bool flag = false;
            //if (searchList.Count > 0) flag = true;
            //Assert.True(flag);

            Assert.True((new FileInfo("../../../../actual-output.json")).Length != 0);
        }

        [Fact]
        public static void FileCompare()
        {
            //var reader = new StreamReader(File.OpenRead("../../../../expected-output.json"));
            //List<string> searchList = new List<string>();
            //while (!reader.EndOfStream)
            //{
            //    var line = reader.ReadLine();
            //    searchList.Add(line);
            //}
            //return searchList;
            //List<string> searchList1 = new List<string>();
            //while (!reader1.EndOfStream)
            //{
            //    var line = reader1.ReadLine();
            //    searchList1.Add(line);
            //}
            // var reader1 = new StreamReader(File.OpenRead("../../../../actual-output.json"));


            var reader2 = JsonConvert.DeserializeObject<JToken>(File.ReadAllText(@"../../../../expected-output.json"));
            

            aggregate_gdp.Program.aggregator();
            var reader1 = JsonConvert.DeserializeObject<JToken>(File.ReadAllText(@"../../../../actual-output.json"));
            
            Assert.True(JToken.DeepEquals(reader1, reader2));

        }

    }
}
