using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _07.PrintAllMinionNames
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection printMinionNamesConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (printMinionNamesConnection)
            {
                printMinionNamesConnection.Open();

                List<string> minionNames = new List<string>();
                SqlCommand selectMinionNamesCommand = new SqlCommand(
                    @"SELECT Name
                        FROM Minions;",
                    printMinionNamesConnection);
                using (selectMinionNamesCommand)
                {
                    SqlDataReader reader = selectMinionNamesCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        minionNames.Add((string)reader["Name"]);
                    }
                }

                for (int i = 0; i < minionNames.Count / 2; i++)
                {
                    Console.WriteLine(minionNames[0 + i]);
                    Console.WriteLine(minionNames[minionNames.Count - 1 - i]);
                }
                if (minionNames.Count % 2 != 0)
                {
                    Console.WriteLine(minionNames[minionNames.Count / 2]);
                }
            }      
        }
    }
}