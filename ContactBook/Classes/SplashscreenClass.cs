using System;
using System.IO;

namespace ContactBook.Classes;

public class SplashscreenClass()
{
    public void Display(string filename)
    {
        if (filename == null || filename.Length==0)
        {
            filename = "./splashscreen.txt";
        }

        filename = Path.GetFullPath(filename).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        DisplayFile(filename);
    }

    public void DisplayFile(string filename)
    {
        if(File.Exists(filename))
        {
            String? line;

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(filename);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);

                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                // Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("File Not Found: "+filename);
        }
    }
}