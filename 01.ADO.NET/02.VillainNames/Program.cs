using Microsoft.Data.SqlClient;
using System;

namespace _02.VillainNames
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection selectVillainNamesConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (selectVillainNamesConnection)
            {
                selectVillainNamesConnection.Open();
                SqlCommand selectVillainNamesCommand = new SqlCommand(
                    @"SELECT V.Name, COUNT(MV.VillainId) AS MinionsCount
                        FROM Villains AS V
                        JOIN MinionsVillains AS MV ON V.Id = MV.VillainId
                        GROUP BY V.Id, V.Name
                        HAVING COUNT(MV.VillainId) > 3
                        ORDER BY COUNT(MV.VillainId);",
                    selectVillainNamesConnection);
                using (selectVillainNamesCommand)
                {
                    SqlDataReader reader = selectVillainNamesCommand.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            int minionsCount = (int)reader["MinionsCount"];
                            Console.WriteLine($"{name} - {minionsCount}");
                        }
                    }
                }
            }
        }
    }
}