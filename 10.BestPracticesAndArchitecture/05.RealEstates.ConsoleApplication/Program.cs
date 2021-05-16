using _01.RealEstates.Data;
using _03.RealEstates.Services;
using _03.RealEstates.Services.Dto;
using _05.RealEstates.Importer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace _05.RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new RealEstatesDbContext();
            var propertyServices = new PropertyServices(context);
            var districtServices = new DistrictServices(context);
            var tagServices = new TagServices(context);

            string command = null;
            while (command != "Exit")
            {
                Console.Clear();
                Console.WriteLine("Type one of the following commands:");
                Console.WriteLine("Reset - reset database");
                Console.WriteLine("Add - add property");
                Console.WriteLine("Price range - select properties by price range");
                Console.WriteLine("Size range - select properties by size range");
                Console.WriteLine("District - select properties by district");
                Console.WriteLine("Average price - select average property price by district");
                Console.WriteLine("Low floor - select properties with floor level 3 or lower");
                Console.WriteLine("High floor - select properties with floor level 10 or higher");
                Console.WriteLine("Cheap - select properties with price lower than 150000$");
                Console.WriteLine("Expensive - select properties with price higher than 400000$");
                Console.WriteLine("Small size - select properties with size smaller than 70 square meters");
                Console.WriteLine("Big size - select properties with size bigger than 120 square meters");
                Console.WriteLine("Old - select properties with year built before 1950");
                Console.WriteLine("New - select properties with year built after 2000");
                Console.WriteLine("Exit - close application");

                command = Console.ReadLine();
                switch (command)
                {
                    case "Reset": 
                        Console.WriteLine("Resetting...");
                        ResetDatabase(context);
                        Console.WriteLine("Successfully reset database and import datasets");
                        break;
                    case "Add":
                        Console.WriteLine("Enter size:");
                        int size = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter yard size:");
                        int yardSize = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter floor:");
                        int floor = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter total floors:");
                        int totalFloors = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter district:");
                        string district = Console.ReadLine();
                        Console.WriteLine("Enter year:");
                        int year = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter type:");
                        string type = Console.ReadLine();
                        Console.WriteLine("Enter building type:");
                        string buildingType = Console.ReadLine();
                        Console.WriteLine("Enter price:");
                        decimal price = decimal.Parse(Console.ReadLine());
                        AddProperty(propertyServices, size, yardSize, floor, totalFloors, district, year, type, buildingType, price);
                        break;
                    case "Price range":
                        Console.WriteLine("Enter minimum price:");
                        decimal minimumPrice = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter maximum price:");
                        decimal maximumPrice = decimal.Parse(Console.ReadLine());
                        var propertiesByPriceRange = SelectPropertiesBetweenPriceRange(propertyServices, minimumPrice, maximumPrice);
                        PrintExportedProperties(propertiesByPriceRange);
                        break;
                    case "Size range":
                        Console.WriteLine("Enter minimum size:");
                        int minimumSize = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter maximum size:");
                        int maximumSize = int.Parse(Console.ReadLine());
                        var propertiesBySizeRange = SelectPropertiesBetweenSizeRange(propertyServices, minimumSize, maximumSize);
                        PrintExportedProperties(propertiesBySizeRange);
                        break;
                    case "District":
                        Console.WriteLine("Enter district name:");
                        string districtName = Console.ReadLine();
                        var propertiesByDistrict = SelectPropertiesByDistrict(districtServices, districtName);
                        PrintExportedProperties(propertiesByDistrict);
                        break;
                    case "Average price":
                        Console.WriteLine("Average property price by district:");
                        var averageDistrictPrices = SelectDistrictsByAveragePrice(districtServices);
                        PrintExportedDistricts(averageDistrictPrices);
                        break;
                    case "Low floor":
                        var lowFloorProperties = SelectLowFloorProperties(tagServices);
                        PrintExportedProperties(lowFloorProperties);
                        break;
                    case "High floor":
                        var highFloorProperties = SelectHighFloorProperties(tagServices);
                        PrintExportedProperties(highFloorProperties);
                        break;
                    case "Cheap":
                        var cheapProperties = SelectCheapProperties(tagServices);
                        PrintExportedProperties(cheapProperties);
                        break;
                    case "Expensive":
                        var expensiveProperties = SelectExpensiveProperties(tagServices);
                        PrintExportedProperties(expensiveProperties);
                        break;
                    case "Small size":
                        var smallSizeProperties = SelectSmallSizeProperties(tagServices);
                        PrintExportedProperties(smallSizeProperties);
                        break;
                    case "Big size":
                        var bigSizeProperties = SelectBigSizeProperties(tagServices);
                        PrintExportedProperties(bigSizeProperties);
                        break;
                    case "Old":
                        var oldProperties = SelectOldProperties(tagServices);
                        PrintExportedProperties(oldProperties);
                        break;
                    case "New":
                        var newProperties = SelectNewProperties(tagServices);
                        PrintExportedProperties(newProperties);
                        break;
                    case "Exit":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }

                Console.WriteLine("Press key to continue...");
                Console.ReadKey();
            }
        }

        private static void ResetDatabase(RealEstatesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            var importer = new DatasetsImporter(context);
            importer.ImportDatasets();
        }

        private static void AddProperty(PropertyServices services, int size, int yardSize, int floor, int totalFloors, string district, int year, string type, string buildingType, decimal price)
        {
            services.AddProperty(size, yardSize, floor, totalFloors, district, year, type, buildingType, price);
        }

        private static List<ExportPropertyDto> SelectPropertiesBetweenPriceRange(PropertyServices services, decimal minimumPrice, decimal maximumPrice)
        {
            var properties = services.SelectPropertiesBetweenPriceRange(minimumPrice, maximumPrice);
            return properties;
        }

        private static List<ExportPropertyDto> SelectPropertiesBetweenSizeRange(PropertyServices services, int minimumSize, int maximumSize)
        {
            var properties = services.SelectPropertiesBetweenSizeRange(minimumSize, maximumSize);
            return properties;
        }

        private static List<ExportPropertyDto> SelectPropertiesByDistrict(DistrictServices services, string district)
        {
            var properties = services.SelectPropertiesByDistrict(district);
            return properties;
        }

        private static List<ExportDistrictDto> SelectDistrictsByAveragePrice(DistrictServices services)
        {
            var districts = services.SelectDistrictsByAveragePrice();
            return districts;
        }

        private static List<ExportPropertyDto> SelectLowFloorProperties(TagServices services)
        {
            var properties = services.SelectLowFloorProperties();
            return properties;
        }

        private static List<ExportPropertyDto> SelectHighFloorProperties(TagServices services)
        {
            var properties = services.SelectHighFloorProperties();
            return properties;
        }

        private static List<ExportPropertyDto> SelectCheapProperties(TagServices services)
        {
            var properties = services.SelectCheapProperties();
            return properties;
        }

        private static List<ExportPropertyDto> SelectExpensiveProperties(TagServices services)
        {
            var properties = services.SelectExpensiveProperties();
            return properties;
        }

        private static List<ExportPropertyDto> SelectSmallSizeProperties(TagServices services)
        {
            var properties = services.SelectSmallSizeProperties();
            return properties;
        }

        private static List<ExportPropertyDto> SelectBigSizeProperties(TagServices services)
        {
            var properties = services.SelectBigSizeProperties();
            return properties;
        }

        private static List<ExportPropertyDto> SelectOldProperties(TagServices services)
        {
            var properties = services.SelectOldProperties();
            return properties;
        }

        private static List<ExportPropertyDto> SelectNewProperties(TagServices services)
        {
            var properties = services.SelectNewProperties();
            return properties;
        }

        private static void PrintExportedProperties(List<ExportPropertyDto> properties)
        {
            foreach (var property in properties)
            {
                Console.WriteLine(property.ToString());
            }
        }

        private static void PrintExportedDistricts(List<ExportDistrictDto> districts)
        {
            foreach (var district in districts)
            {
                Console.WriteLine(district.ToString());
            }
        }
    }
}
