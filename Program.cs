using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab3_1_exceptions_
{

    class Program
    {
        static string[] OpenFile(string filename)
        {
            try
            {
                string[] numbers = new string[2];
                using (StreamReader lines = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
                {
                    numbers[0] = lines.ReadLine();
                    numbers[1] = lines.ReadLine();
                }
                
                return numbers;
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not found: "+filename);
            }

        }
        static double Average()
        {
            using (StreamWriter noFileWriter = new StreamWriter("no_file.txt"),
            badDataWriter = new StreamWriter("bad_data.txt"), overflowWriter = new StreamWriter("overflow.txt"))
            {
                int sum = 0;
                int count = 0;
              

                for (int i = 10; i < 30; i++)
                {
                    string filename = i.ToString() + ".txt";
                    try
                    {
                        string[] numbers = OpenFile(filename);
                        try
                        {
                            int number1 = int.Parse(numbers[0]);
                            int number2 = int.Parse(numbers[1]);
                             checked
                             {
                                    sum += number1 * number2;
                                    count++;
                             }  
                        }
                        catch (OverflowException)
                        {
                            overflowWriter.WriteLine(filename);
                            Console.WriteLine("The product exceeds the maximum int value in the file: " + filename);
                        }
                        catch (Exception)
                        {
                            badDataWriter.WriteLine(filename);
                            Console.WriteLine("The data is not correct in the file: " + filename);
                        }  
                        
                    }
                    catch (Exception ex)
                    {
                        noFileWriter.WriteLine(filename);
                        Console.WriteLine(ex.Message);
                    }
                }
                
                try
                {
                    return sum / count;
                }
                catch(Exception)
                {
                    throw new Exception("An error occurred when finding the arithmetic average");
                }

            }
        }
       
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Average is equal to: " + Average());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
           
        }
    }
}
    



    



