using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _03.MinionNames
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection selectMinionNamesConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (selectMinionNamesConnection)
            {
                selectMinionNamesConnection.Open();
                SqlCommand selectMinionNamesCommand = new SqlCommand(
                    @"SELECT Name
                        FROM Villains
                        WHERE Id = @Id;

                    SELECT ROW_NUMBER() OVER (ORDER BY m.Name) AS RowNum, m.Name, m.Age
                        FROM MinionsVillains AS mv
                        JOIN Minions AS m ON mv.MinionId = m.Id
                        WHERE mv.VillainId = @Id
                        ORDER BY m.Name;",
                    selectMinionNamesConnection);

                int villainId = int.Parse(Console.ReadLine());
                SqlParameter villainIdParameter = new SqlParameter("@Id", villainId);
                selectMinionNamesCommand.Parameters.Add(villainIdParameter);

                using (selectMinionNamesCommand)
                {
                    SqlDataReader reader = selectMinionNamesCommand.ExecuteReader();
                    using (reader)
                    {
                        if (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            Console.WriteLine($"Villain: {name}");

                            reader.NextResult();

                            bool invalidMinions = true;
                            while (reader.Read())
                            {
                                invalidMinions = false;
                                long rowNumber = (long)reader["RowNum"];
                                string minionName = (string)reader["Name"];
                                int age = (int)reader["Age"];
                                Console.WriteLine($"{rowNumber}. {minionName} {age}");
                            }
                            if (invalidMinions)
                            {
                                Console.WriteLine("(No minions)");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                        }      
                    }
                }
            }
        }
    }
}