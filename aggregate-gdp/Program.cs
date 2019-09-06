using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace aggregate_gdp
{
    public class Program
    {
        public static void Main(string[] args)
        {

        }
        public static void aggregator()
        {
            var data = LoadCsvFile("../../../../datafile.csv");
            int row = data.Count; int col = (data[0].Split(",", StringSplitOptions.RemoveEmptyEntries)).Length;
            
            string[,] datArray = new string[row,col];
            int r1 = 0, c1 = 0;
            foreach (var i in data)
            {
                String[] spearator = { ",", "\"" };
                String[] strlist = i.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string j in strlist)
                {
                    datArray[r1, c1] = j; c1 += 1;
                }
                r1 += 1; c1 = 0;
            }
            

            string coun = "Continent,Country,Africa,Algeria,Africa,Angola,Africa,Benin,Africa,Botswana,Africa,Burkina,Africa,Burundi,Africa,Cameroon,Africa,Cape Verde,Africa,Central African Republic,Africa,Chad,Africa,Comoros,Africa,Congo,Africa,Democratic Republic of,Africa,Djibouti,Africa,Egypt,Africa,Equatorial Guinea,Africa,Eritrea,Africa,Ethiopia,Africa,Gabon,Africa,Gambia,Africa,Ghana,Africa,Guinea,Africa,Guinea-Bissau,Africa,Ivory Coast,Africa,Kenya,Africa,Lesotho,Africa,Liberia,Africa,Libya,Africa,Madagascar,Africa,Malawi,Africa,Mali,Africa,Mauritania,Africa,Mauritius,Africa,Morocco,Africa,Mozambique,Africa,Namibia,Africa,Niger,Africa,Nigeria,Africa,Rwanda,Africa,Sao Tome and Principe,Africa,Senegal,Africa,Seychelles,Africa,Sierra Leone,Africa,Somalia,Africa,South Africa,Africa,South Sudan,Africa,Sudan,Africa,Swaziland,Africa,Tanzania,Africa,Togo,Africa,Tunisia,Africa,Uganda,Africa,Zambia,Africa,Zimbabwe,Asia,Afghanistan,Asia,Bahrain,Asia,Bangladesh,Asia,Bhutan,Asia,Brunei,Asia,Burma (Myanmar),Asia,Cambodia,Asia,China,Asia,East Timor,Asia,India,Asia,Indonesia,Asia,Iran,Asia,Iraq,Asia,Israel,Asia,Japan,Asia,Jordan,Asia,Kazakhstan,Asia,Republic of Korea,Asia, South Kore,Asia,Kuwait,Asia,Kyrgyzstan,Asia,Laos,Asia,Lebanon,Asia,Malaysia,Asia,Maldives,Asia,Mongolia,Asia,Nepal,Asia,Oman,Asia,Pakistan,Asia,Philippines,Asia,Qatar,Asia,Russia,Asia,Saudi Arabia,Asia,Singapore,Asia,Sri Lanka,Asia,Syria,Asia,Tajikistan,Asia,Thailand,Asia,Turkey,Asia,Turkmenistan,Asia,United Arab Emirates,Asia,Uzbekistan,Asia,Vietnam,Asia,Yemen,Europe,Albania,Europe,Andorra,Europe,Armenia,Europe,Austria,Europe,Azerbaijan,Europe,Belarus,Europe,Belgium,Europe,Bosnia and Herzegovina,Europe,Bulgaria,Europe,Croatia,Europe,Cyprus,Europe,CZ,Europe,Denmark,Europe,Estonia,Europe,Finland,Europe,France,Europe,Georgia,Europe,Germany,Europe,Greece,Europe,Hungary,Europe,Iceland,Europe,Ireland,Europe,Italy,Europe,Latvia,Europe,Liechtenstein,Europe,Lithuania,Europe,Luxembourg,Europe,Macedonia,Europe,Malta,Europe,Moldova,Europe,Monaco,Europe,Montenegro,Europe,Netherlands,Europe,Norway,Europe,Poland,Europe,Portugal,Europe,Romania,Europe,San Marino,Europe,Serbia,Europe,Slovakia,Europe,Slovenia,Europe,Spain,Europe,Sweden,Europe,Switzerland,Europe,Ukraine,Europe,United Kingdom,Europe,Vatican City,North America,Antigua and Barbuda,North America,USA,North America,Bahamas,North America,Barbados,North America,Belize,North America,Canada,North America,Costa Rica,North America,Cuba,North America,Dominica,North America,Dominican Republic,North America,El Salvador,North America,Grenada,North America,Guatemala,North America,Haiti,North America,Honduras,North America,Jamaica,North America,Mexico,North America,Nicaragua,North America,Panama,North America,Saint Kitts and Nevis,North America,Saint Lucia,North America,Saint Vincent and the Grenadines,North America,Trinidad and Tobago,North America,US,Oceania,Australia,Oceania,Fiji,Oceania,Kiribati,Oceania,Marshall Islands,Oceania,Micronesia,Oceania,Nauru,Oceania,New Zealand,Oceania,Palau,Oceania,Papua New Guinea,Oceania,Samoa,Oceania,Solomon Islands,Oceania,Tonga,Oceania,Tuvalu,Oceania,Vanuatu,South America,Argentina,South America,Bolivia,South America,Brazil,South America,Chile,South America,Colombia,South America,Ecuador,South America,Guyana,South America,Paraguay,South America,Peru,South America,Suriname,South America,Uruguay,South America,Venezuela";
            string[] coun1= coun.Split(",", StringSplitOptions.RemoveEmptyEntries);
            //foreach (var i in coun1) Console.WriteLine(i);

            var gdpPos = 7;
            var populationPos = 4;
            string[] conti = { "South America", "Oceania", "North America", "Asia", "Europe", "Africa" };
            double[,] valueslist = new double[2, 6];
            for(int i=1;i<row;i++)
            {
                int h=-1;int finallistLoc = -1;
                for(int j=0;j<coun1.Length;j++)
                {
                    if(coun1[j]==datArray[i,0])
                    {
                        h = j - 1;break;
                    }
                }
                for(int j=0;j<6;j++)
                {
                    if (h>-1 && conti[j] == coun1[h]) finallistLoc = j;
                }

                if (finallistLoc > -1)
                {
                    valueslist[0,finallistLoc] += Double.Parse(datArray[i,gdpPos]);
                    valueslist[1,finallistLoc] += Double.Parse(datArray[i,populationPos]);
                }

            }

            List<aggregate> eList = new List<aggregate>();
            aggregate e;

            for(int i =0;i<6;i++)
            {
                e = new aggregate();
                e.GDP_2012 = valueslist[0, i];
                e.POPULATION_2012 = valueslist[1, i];
                eList.Add(e);
            }

            Dictionary<string, aggregate> My_dict1 =new Dictionary<string, aggregate>();
            
            for(int i=0;i<6;i++)
            {
                My_dict1.Add(conti[i], eList[i]);
            }
            string ans1 = JsonConvert.SerializeObject(My_dict1, Formatting.Indented);
            System.IO.File.WriteAllText("../../../../actual-output.json", ans1);








        }

        public static List<string> LoadCsvFile(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> searchList = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                searchList.Add(line);
            }
            return searchList;
        }



    }
    public class aggregate
    {

        public double GDP_2012;
        public double POPULATION_2012;

    }
}
