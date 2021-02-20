using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _08.IncreaseMinionAge
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection increaseMinionAgeConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (increaseMinionAgeConnection)
            {
                increaseMinionAgeConnection.Open();

                List<int> minionIds = Console.ReadLine().Split().Select(int.Parse).ToList();
                foreach (int item in minionIds)
                {
                    SqlParameter minionIdParameter = new SqlParameter("@id", item);
                    SqlCommand increaseMinionAgeCommand = new SqlCommand(
                        @"UPDATE Minions
                            SET
                                Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)),
                                Age += 1
                                WHERE Id = @id;",
                        increaseMinionAgeConnection);
                    increaseMinionAgeCommand.Parameters.Add(minionIdParameter);
                    using (increaseMinionAgeCommand)
                    {
                        increaseMinionAgeCommand.ExecuteNonQuery();
                        increaseMinionAgeCommand.Parameters.Clear();
                    }
                }

                SqlCommand selectMinionsCommand = new SqlCommand(
                    @"SELECT Name, Age
                        FROM Minions;",
                    increaseMinionAgeConnection);
                using (selectMinionsCommand)
                {
                    SqlDataReader reader = selectMinionsCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        int age = (int)reader["Age"];
                        Console.WriteLine($"{name} {age}");
                    }
                }
            }
        }
    }
}