using Microsoft.Data.SqlClient;
using System;

namespace _09.IncreaseAgeStoredProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection increaseAgeConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (increaseAgeConnection)
            {
                increaseAgeConnection.Open();

                SqlCommand createGetOlderProcedureCommand = new SqlCommand(
                    @"CREATE OR ALTER PROC usp_GetOlder(@id INT) AS
                        UPDATE Minions
                        SET
                            Age += 1
                            WHERE Id = @id",
                    increaseAgeConnection);
                using (createGetOlderProcedureCommand)
                {
                    createGetOlderProcedureCommand.ExecuteNonQuery();
                }

                int minionId = int.Parse(Console.ReadLine());
                SqlParameter minionIdParameter = new SqlParameter("@id", minionId);

                SqlCommand executeGetOlderProcedureCommand = new SqlCommand(
                    @"EXEC usp_GetOlder @id",
                    increaseAgeConnection);
                executeGetOlderProcedureCommand.Parameters.Add(minionIdParameter);
                using (executeGetOlderProcedureCommand)
                {
                    executeGetOlderProcedureCommand.ExecuteNonQuery();
                    executeGetOlderProcedureCommand.Parameters.Clear();
                }

                SqlCommand selectAffectedMinionCommand = new SqlCommand(
                    @"SELECT Name, Age
                        FROM Minions
                        WHERE Id = @Id",
                    increaseAgeConnection);
                selectAffectedMinionCommand.Parameters.Add(minionIdParameter);
                using (selectAffectedMinionCommand)
                {
                    SqlDataReader reader = selectAffectedMinionCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        int age = (int)reader["Age"];
                        Console.WriteLine($"{name} - {age} years old");
                    }
                }
            }
        }
    }
}