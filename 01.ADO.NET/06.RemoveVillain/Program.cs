using Microsoft.Data.SqlClient;
using System;

namespace _06.RemoveVillain
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection removeVillainConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (removeVillainConnection)
            {
                removeVillainConnection.Open();
                int villainId = int.Parse(Console.ReadLine());
                SqlParameter villainIdParameter = new SqlParameter("@villainId", villainId);

                SqlTransaction removeVillainTransaction = removeVillainConnection.BeginTransaction();
                using (removeVillainTransaction)
                {
                    try
                    {
                        string villainName;
                        SqlCommand selectVillainNameCommand = new SqlCommand(
                            @"SELECT Name
                                FROM Villains
                                WHERE Id = @villainId;",
                            removeVillainConnection, removeVillainTransaction);
                        selectVillainNameCommand.Parameters.Add(villainIdParameter);
                        using (selectVillainNameCommand)
                        {
                            villainName = (string)selectVillainNameCommand.ExecuteScalar();
                            selectVillainNameCommand.Parameters.Clear();
                        }

                        if (villainName == null)
                        {
                            Console.WriteLine("No such villain was found.");
                            removeVillainTransaction.Rollback();
                        }
                        else
                        {
                            int releasedMinions;
                            SqlCommand deleteMinionsVillainsIdCommand = new SqlCommand(
                                @"DELETE
                                    FROM MinionsVillains
                                    WHERE VillainId = @villainId;",
                                removeVillainConnection, removeVillainTransaction);
                            deleteMinionsVillainsIdCommand.Parameters.Add(villainIdParameter);
                            using (deleteMinionsVillainsIdCommand)
                            {
                                releasedMinions = deleteMinionsVillainsIdCommand.ExecuteNonQuery();
                                deleteMinionsVillainsIdCommand.Parameters.Clear();
                            }

                            SqlCommand deleteVillainCommand = new SqlCommand(
                                @"DELETE
                                    FROM Villains
                                    WHERE Id = @villainId;",
                                removeVillainConnection, removeVillainTransaction);
                            deleteVillainCommand.Parameters.Add(villainIdParameter);
                            using (deleteVillainCommand)
                            {
                                deleteVillainCommand.ExecuteNonQuery();
                                deleteVillainCommand.Parameters.Clear();
                            }

                            removeVillainTransaction.Commit();
                            Console.WriteLine($"{villainName} was deleted.");
                            Console.WriteLine($"{releasedMinions} minions were released.");
                        }                                                
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                        removeVillainTransaction.Rollback();
                    }
                }
            }
        }
    }
}