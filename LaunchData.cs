﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Code_Behind_CSV_Reader
{
    class LaunchData 
    {

        // this function calls the open file function that returns a string with all the data
        // the func then parses the data and saves it to a list.



        public void openReadData()
        {

            List<string> data = new List<string>();

            string filename = @"C:\Users\PC1\source\repos\Code_Behind_CSV_Reader\data2.txt";
            using (StreamReader reader = File.OpenText(filename))
            {

                string line = String.Empty;
                while ((line = reader.ReadLine()) != null)
                {

                    data.Add(line);

                }

                // call function to parse and save data to file in bin/debug
                parseData(data);


            }
        }

        //takes a list<string> data and checks each element to see if it contains the gage we need
        // if it does it saves it to a new list<string> then calls saveToFile that list 
        // to a file in bin/debug
        public void parseData(List<string> data) 
        {
            List<string> parseData = new List<string>();

            for (int i =0; i < data.Count;i++)
            {
                

                if (data[i].Contains("Time") == true)
                {
                    parseData.Add(data[i]);

                }
                else if (data[i].Contains("PT LOX") == true)
                {
                    parseData.Add(data[i]);

                }
                else if (data[i].Contains("P4- High Pressure Tank") == true)
                {
                    parseData.Add(data[i]);

                }
                else if (data[i].Contains("PT Fuel Tank") == true)
                {
                    parseData.Add(data[i]);

                }

            }


            /* 
             * foreach (string item in parseData) 
            {
                Console.WriteLine(item);
            }
            */


            saveToFile(parseData);
        }

        public void saveToFile(List<string> parseData) 
        {
            // saved in bin/debug idk how to change the save location
            System.IO.File.WriteAllLines("LaunchData.txt", parseData);

        }

        public void saveToSQL() 
        {
           
        }

        // this fucn takes a string that contains the gages name with value. 
        // it gets the float number as a string, converts and returns it as a float
        public float gageValue(string dataLine)
        {
            float value = 0;
            string gageValues = "";
            // this bool makes sure that the for loop ignores the numbers in the name of the gages P4 in
            //case
            bool dataValues = false;


            // iterates the string looking for the float 
            for (int i =0; i < dataLine.Length;i++)
            {
                if (dataLine[i]==':') { dataValues = true; }
                if (dataValues) 
                {

                    if (dataLine[i] >= '0' && dataLine[i] <= '9' || dataLine[i] == '.')
                    {
                        //adds the string char numbers to one string
                        gageValues += dataLine[i];

                    }
                }
                
            }

            //converts the string number into a float
            value = float.Parse(gageValues, CultureInfo.InvariantCulture.NumberFormat);
            
            return value;
        }

       
    }
}