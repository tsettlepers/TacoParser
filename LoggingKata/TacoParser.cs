using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        ILog thisLogger;
        int errCounter = 0;

        public TacoParser() 
        { 
            thisLogger = new TacoLogger(); 
        }
        public TacoParser(ILog activeLogger) 
        {  
            thisLogger = activeLogger; 
        }

        //WHAT IS GAINED BY MAKING ITRACKABLE AN INTERFACE INSTEAD OF A SIMPLE CLASS?
        
        public ITrackable Parse(string line, bool withLogging=true)
        {
            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            if (errCounter > 5)
            {
                if (withLogging) thisLogger.LogFatal("Error count exceeded.");
                throw new Exception("Error count exceeded.");
            }
            else if (cells.Length != 3)                                      // If your array.Length is less than 3, something went wrong
            {
                // Log that and return null
                // Do not fail if one record parsing fails, return null
                ++errCounter;
                if (withLogging) thisLogger.LogInfo("Invalid column layout.");
                return null; // TODO Implement
            }
            else
            {
                try
                {
                    double lat = double.Parse(cells[0]);                // grab the latitude from your array at index 0
                    double lon = double.Parse(cells[1]);                // grab the longitude from your array at index 1
                    string nm = cells[2];                               // grab the name from your array at index 2
                    // Your going to need to parse your string as a `double` which is similar to parsing a string as an `int`
                    // You'll need to create a TacoBell class that conforms to ITrackable
                    // Then, you'll need an instance of the TacoBell class with the name and point set correctly
                    // Then, return the instance of your TacoBell class since it conforms to ITrackable
                    if (withLogging) thisLogger.LogInfo("Successfully parsed.");
                    return new TacoHell() { Name = nm, Location = new Point() { Latitude = lat, Longitude = lon } };
                }
                catch (Exception e)
                {
                    ++errCounter;
                    if (withLogging) thisLogger.LogError($"Failed to parse ({line})",e);
                    return null;
                }
            }
        }
    }

    public class TacoHell : ITrackable
    {
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}