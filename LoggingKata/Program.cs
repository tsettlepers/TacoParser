using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------
            
            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);
            logger.LogInfo($"Lines read: {lines.Length}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser(logger);

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            //var locations = lines.Select(parser.Parse).ToArray();
            var stores = lines.Select(x => parser.Parse(x,false)).ToArray();
            logger.LogInfo($"Lines parsed: {stores.Length}");

            #region INSTRUCTIONS
            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // Create a `double` variable to store the distance

            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            #endregion

            int firstTH = -1;
            int secondTH = -1;
            double maxDistance = 0;

            //Find the two locations that are the farthest apart
            for (int i = 0; i < stores.Length - 1; i++)
            {
                var firstStore = new GeoCoordinate(stores[i].Location.Latitude, stores[i].Location.Longitude);
                for (int j = i + 1; j < stores.Length; j++)
                {
                    var secondStore = new GeoCoordinate(stores[j].Location.Latitude, stores[j].Location.Longitude);
                    double thisDist = firstStore.GetDistanceTo(secondStore);
                    if (thisDist > maxDistance)
                    {
                        maxDistance = thisDist;
                        firstTH = i;
                        secondTH = j;
                    }
                }
            }
            logger.LogInfo($"The most distant stores are: {stores[firstTH].Name} and {stores[secondTH].Name} with a distance of {maxDistance*100/2.54/12/5280}miles");
        }
    }
}
