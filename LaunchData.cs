using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.Sql;
using System.Data.SqlClient;


namespace Code_Behind_CSV_Reader
{
    class LaunchData 
    {



        // this function calls the open file function that returns a string with all the data
        // the func then parses the data and saves it to a list.
        //launch data has a couple instances of info 
        //launch data 2 has only one instance of info



        public void openReadData()
        {

            List<string> data = new List<string>();

            string filename = @"C:\Users\PC1\source\repos\Code_Behind_CSV_Reader\data.txt";
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






        public string gageNames(string dataLine)
        {

            string gageName = "";
            // this bool makes sure that the for loop ignores the numbers in the name of the gages P4 in
            //case



            // iterates the string looking for the float 
            for (int i = 0; i < dataLine.Length; i++)
            {


                if (dataLine.Contains("Time") == true)
                {
                    gageName = "Time";

                }
                else if (dataLine.Contains("PT LOX") == true)
                {
                    gageName = "PT LOX";
                }
                else if (dataLine.Contains("P4- High Pressure Tank") == true)
                {
                    gageName = "High Pressure Tank";
                }

                

            }
            return gageName;
        }

        // this function takes a string that contains the gages name with value. 
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


        /*func will save the list of gage values to sql table 
         I have to manually enter a launch ID or else this function wont work. im not sure how to work around 
        that 
         */

        //returns a string with the launch info from the file
        //launch data has a couple instances of info 
        //launch data 2 has only one instance of info
        public string openLaunchData() 
        {

            //this has the file path from my pc 
            string text = System.IO.File.ReadAllText(@"C:\Users\PC1\source\repos\Code_Behind_CSV_Reader\bin\Debug\LaunchData.txt");

            return text;
        }
        
        
        public void saveToTable(float id, float Time, float LoxTank, float HighPressureTank, float FuelTank) 
        {
            try
            {

                SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\PC1\source\repos\Code_Behind_CSV_Reader\LaunchDB.mdf;Integrated Security=True");
                conn.Open();

                try
                {
                    SqlCommand cmdId = new SqlCommand("SELECT MAX (id) +1 FROM LaunchData", conn);
                    id += (int)cmdId.ExecuteScalar();

                }
                catch (Exception e) { Console.WriteLine(id); }

                //this code is a work around for the auto generator in sql fig how it works 
                //SqlCommand cmdId = new SqlCommand("SELECT MAX (id) +1 FROM LaunchData", conn); 
                //int newID = (int)cmdId.ExecuteScalar();

                //this inserts into table dont change 
                SqlCommand cmd = new SqlCommand("INSERT INTO [LaunchData] VALUES('" + id + "','" + Time + "','" + LoxTank + "','" + HighPressureTank + "','" + FuelTank + "')", conn);

                cmd.ExecuteNonQuery();
                //Console.WriteLine("Inserting Data Successfully");
                conn.Close();


            }
            catch (Exception e) { Console.WriteLine(e); }

        }

        public void saveToSQL()
        {

            string launchData = openLaunchData();
            //Console.WriteLine(launchData);

            float Time = -1;
            float LoxTank = -1;
            float HighPressureTank = -1;
            float FuelTank = -1;
            int id = 0;

            //each loop will save into sql a complete instance of all the gage info
            using (StringReader reader = new StringReader(launchData))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {


                    if (line.Contains("Time"))
                    {
                        Time = gageValue(line);
                    }
                    else if (line.Contains("LOX Tank"))
                    { 
                        LoxTank = gageValue(line);
                    }
                    else if (line.Contains("High Pressure Tank"))
                    {
                        HighPressureTank = gageValue(line);
                    }
                    else if (line.Contains("Fuel Tank"))
                    {
                       FuelTank = gageValue(line);
                       saveToTable(id, Time, LoxTank, HighPressureTank, FuelTank);

                        //i put these here as a check if the data isnt inserted correctly it will show -1
                        Time = -1;
                        LoxTank = -1;
                        HighPressureTank = -1;
                        FuelTank = -1;
                    }
                }


            }
            

            Console.WriteLine(  "i hate sql " + (Time+ LoxTank+ HighPressureTank+ FuelTank));
        }



    }//end of class

}//end of namespace
