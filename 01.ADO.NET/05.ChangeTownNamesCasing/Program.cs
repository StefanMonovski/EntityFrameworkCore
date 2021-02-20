using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace _05.ChangeTownNamesCasing
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection changeTownNamesConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (changeTownNamesConnection)
            {
                changeTownNamesConnection.Open();
                string countryName = Console.ReadLine();
                SqlParameter countryNameParameter = new SqlParameter("@countryName", countryName);

                int rowsAffected;
                SqlCommand changeTownNamesCommand = new SqlCommand(
                    @"UPDATE Towns
                      SET
                        Name = UPPER(Name)
                        WHERE CountryCode = (
                            SELECT C.Id
                                FROM Countries AS C
                                WHERE C.Name = @countryName
                            );",
                    changeTownNamesConnection);
                changeTownNamesCommand.Parameters.Add(countryNameParameter);
                using (changeTownNamesCommand)
                {
                    rowsAffected = changeTownNamesCommand.ExecuteNonQuery();
                    changeTownNamesCommand.Parameters.Clear();
                }

                if (rowsAffected == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    List<string> townNamesAffected = new List<string>();
                    SqlCommand selectChangedTownNamesCommand = new SqlCommand(
                        @"SELECT T.Name
                            FROM Towns AS T
                            JOIN Countries AS C ON C.Id = T.CountryCode
                            WHERE C.Name = @countryName;",
                        changeTownNamesConnection);
                    selectChangedTownNamesCommand.Parameters.Add(countryNameParameter);
                    using (selectChangedTownNamesCommand)
                    {
                        SqlDataReader reader = selectChangedTownNamesCommand.ExecuteReader();
                        using (reader)
                        {
                            while (reader.Read())
                            {
                                townNamesAffected.Add((string)reader["Name"]);
                            }
                        }
                        changeTownNamesCommand.Parameters.Clear();
                    }

                    Console.WriteLine($"{rowsAffected} town names were affected.");
                    Console.WriteLine($"[{string.Join(", ", townNamesAffected)}]");
                }
            }
        }
    }
}