using System;
using System.Linq;
using System.Xml.Linq;

namespace GpxGenerator
{
    public class Program
    {
        private const string rootGpxElement = "rte";
        private const string rootPointGpxElement = "rtept";
        private const string latitudeGpxElement = "lat";
        private const string longitudeGpxElement = "lon";
        private const double metersPerLatitudeDegree = 110574;
        private const double metersPerLongitudeDegree = 111320;
        private const double degreesToRadians = Math.PI / 180.0;

        public static void Main()
        {
            var (latitude, longitude, width, height, quadrantWidth) = GetUserInput();

            // Calculate the number of quadrants needed horizontally and vertically
            int numQuadrantsHorizontal = (int)Math.Ceiling(width / quadrantWidth);
            int numQuadrantsVertical = (int)Math.Ceiling(height / quadrantWidth);

            // Calculate the latitude and longitude increments for each quadrant
            double latitudeIncrement = quadrantWidth / metersPerLatitudeDegree;
            double longitudeIncrement = quadrantWidth / (metersPerLongitudeDegree * Math.Cos(latitude * degreesToRadians));

            XDocument doc = GenerateGpxDocument(latitude, longitude, numQuadrantsHorizontal, numQuadrantsVertical, latitudeIncrement, longitudeIncrement);

            doc.Save("grid.gpx");

            AddQuadrantNamesToDocument(doc, latitude, longitude, numQuadrantsHorizontal, numQuadrantsVertical, latitudeIncrement, longitudeIncrement);

            doc.Save("grid_with_names.gpx");

            Console.WriteLine("GPX files saved successfully.");
        }

        private static XDocument GenerateGpxDocument(double latitude, double longitude, int numQuadrantsHorizontal, int numQuadrantsVertical, double latitudeIncrement, double longitudeIncrement)
        { 
            // Create an XML document to store the GPX data
            return new XDocument(
                new XElement("gpx",
                    new XAttribute("version", "1.1"),
                    new XAttribute("creator", "UniversityRescueSquad"),
                    Enumerable.Range(0, numQuadrantsHorizontal + 1)
                        // Calculate the longitude for each vertical route
                        .Select(i => longitude + i * longitudeIncrement)
                        // Create a route element for each vertical route
                        .Select(longitude =>
                            new XElement(rootGpxElement,
                                // Create a route point element for the start of the route
                                new XElement(rootPointGpxElement,
                                    new XAttribute(latitudeGpxElement, latitude),
                                    new XAttribute(longitudeGpxElement, longitude)),
                                // Create a route point element for the end of the route
                                new XElement(rootPointGpxElement,
                                    new XAttribute(latitudeGpxElement, latitude + numQuadrantsVertical * latitudeIncrement),
                                    new XAttribute(longitudeGpxElement, longitude)))),
                    Enumerable.Range(0, numQuadrantsVertical + 1)
                        // Calculate the latitude for each horizontal route
                        .Select(i => latitude + i * latitudeIncrement)
                        // Create a route element for each horizontal route
                        .Select(latitude =>
                            new XElement(rootGpxElement,
                                // Create a route point element for the start of the route
                                new XElement(rootPointGpxElement,
                                    new XAttribute(latitudeGpxElement, latitude),
                                    new XAttribute(longitudeGpxElement, longitude)),
                                // Create a route point element for the end of the route
                                new XElement(rootPointGpxElement,
                                    new XAttribute(latitudeGpxElement, latitude),
                                    new XAttribute(longitudeGpxElement, longitude + numQuadrantsHorizontal * longitudeIncrement))))));
        }

        private static void AddQuadrantNamesToDocument(XDocument doc, double latitude, double longitude, int numQuadrantsHorizontal, int numQuadrantsVertical, double latitudeIncrement, double longitudeIncrement)
        {
            // Add the quadrants as waypoints to the GPX document
            XElement root = doc.Root;
            for (int i = 0; i < numQuadrantsHorizontal; i++)
            {
                for (int j = 0; j < numQuadrantsVertical; j++)
                {
                    double quadrantLongitude = longitude + i * longitudeIncrement;
                    double quadrantLatitude = latitude + j * latitudeIncrement;

                    XElement wpt = new XElement("wpt",
                        new XAttribute("lat", quadrantLatitude),
                        new XAttribute("lon", quadrantLongitude),
                        new XElement("name", $"{GetQuadrantVerticalNames(j)} {i}"));

                    root.Add(wpt);
                }
            }
        }

        private static (double latitude, double longitude, double width, double height, double quadrantWidth) GetUserInput()
        {
            Console.Write("Enter latitude in degrees and tens of degrees (eg. 42.123123): ");
            double latitude = double.Parse(Console.ReadLine());
            Console.Write("Enter longitude in degrees and tens of degrees (eg. 23.123123): ");
            double longitude = double.Parse(Console.ReadLine());
            Console.Write("Enter target area width in meters: ");
            double width = double.Parse(Console.ReadLine());
            Console.Write("Enter target area height in meters: ");
            double height = double.Parse(Console.ReadLine());
            Console.Write("Enter quadrant width in meters: ");
            double quadrantWidth = double.Parse(Console.ReadLine());

            return (latitude, longitude, width, height, quadrantWidth);
        }

        /// <summary>
        /// Returns name from the following sequence { A, B, C, ..., AA, AB, AC... }
        /// </summary>
        private static string GetQuadrantVerticalNames(int index)
        {
            int intFirstLetter = ((index) / 676) + 64;
            int intSecondLetter = ((index % 676) / 26) + 64;
            int intThirdLetter = (index % 26) + 65;

            char firstLetter = (intFirstLetter > 64) ? (char)intFirstLetter : ' ';
            char secondLetter = (intSecondLetter > 64) ? (char)intSecondLetter : ' ';
            char thirdLetter = (char)intThirdLetter;

            return string.Concat(firstLetter, secondLetter, thirdLetter).Trim();
        }
    }
}
