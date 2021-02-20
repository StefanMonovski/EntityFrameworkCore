using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.AddMinion
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection addMinionConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");
            using (addMinionConnection)
            {
                addMinionConnection.Open();
                List<string> input = Console.ReadLine().Split().ToList();
                string minionName = input[1];
                int age = int.Parse(input[2]);
                string townName = input[3];
                input = Console.ReadLine().Split().ToList();
                string villainName = input[1];

                SqlParameter minionNameParameter = new SqlParameter("@minionName", minionName);
                SqlParameter minionAgeParameter = new SqlParameter("@minionAge", age);
                SqlParameter townNameParameter = new SqlParameter("@townName", townName);
                SqlParameter villainNameParameter = new SqlParameter("@villainName", villainName);

                SqlCommand alterTownsCommand = new SqlCommand(
                    @"ALTER TABLE Towns
                        ALTER COLUMN CountryCode INT NULL;",
                    addMinionConnection);
                using (alterTownsCommand)
                {
                    alterTownsCommand.ExecuteNonQuery();
                }
                
                int townId = GetTownId(townNameParameter, addMinionConnection);               
                if (townId == 0)
                {
                    InsertTown(townName, townNameParameter, addMinionConnection);
                    townId = GetTownId(townNameParameter, addMinionConnection);
                }

                int villainId = GetVillainId(villainNameParameter, addMinionConnection);
                if (villainId == 0)
                {
                    InsertVillain(villainName, villainNameParameter, addMinionConnection);
                    villainId = GetVillainId(villainNameParameter, addMinionConnection);
                }

                int minionId = GetMinionId(minionNameParameter, addMinionConnection);
                if (minionId == 0)
                {
                    SqlParameter minionTownIdParameter = new SqlParameter("@minionTownId", townId);
                    InsertMinion(minionNameParameter, minionAgeParameter, minionTownIdParameter, addMinionConnection);
                    minionId = GetMinionId(minionNameParameter, addMinionConnection);
                }                          

                SqlParameter minionIdParameter = new SqlParameter("@minionId", minionId);
                SqlParameter villainIdParameter = new SqlParameter("@villainId", villainId);
                InsertMinionVillainId(minionName, villainName, minionIdParameter, villainIdParameter, addMinionConnection);                
            }
        }

        public static int GetTownId(SqlParameter townNameParameter, SqlConnection connection)
        {
            int townId;
            SqlCommand selectTownIdCommand = new SqlCommand(
                @"SELECT Id
                    FROM Towns
                    WHERE Name = @townName",
                connection);
            
            selectTownIdCommand.Parameters.Add(townNameParameter);

            using (selectTownIdCommand)
            {
                townId = Convert.ToInt32(selectTownIdCommand.ExecuteScalar());
                selectTownIdCommand.Parameters.Clear();
            }
            return townId;
        }

        public static void InsertTown(string townName, SqlParameter townNameParameter, SqlConnection connection)
        {
            SqlCommand insertTownCommand = new SqlCommand(
                @"INSERT INTO Towns (Name)
                    VALUES
                        (@townName)",
                connection);
            insertTownCommand.Parameters.Add(townNameParameter);

            using (insertTownCommand)
            {
                int result = insertTownCommand.ExecuteNonQuery();
                if (result == 1)
                {
                    Console.WriteLine($"Town {townName} was added to the database.");
                }
                insertTownCommand.Parameters.Clear();
            }
        }

        public static int GetVillainId(SqlParameter villainNameParameter, SqlConnection connection)
        {
            int villainId;
            SqlCommand selectVillainIdCommand = new SqlCommand(
                @"SELECT Id
                    FROM Villains
                    WHERE Name = @villainName",
                connection);

            selectVillainIdCommand.Parameters.Add(villainNameParameter);

            using (selectVillainIdCommand)
            {
                villainId = Convert.ToInt32(selectVillainIdCommand.ExecuteScalar());
                selectVillainIdCommand.Parameters.Clear();
            }
            return villainId;
        }

        public static void InsertVillain(string villainName, SqlParameter villainNameParameter, SqlConnection connection)
        {
            SqlCommand insertVillainCommand = new SqlCommand(
                @"INSERT INTO Villains (Name, EvilnessFactorId)
                    VALUES
                        (@villainName, 4)",
                connection);
            insertVillainCommand.Parameters.Add(villainNameParameter);

            using (insertVillainCommand)
            {
                int result = insertVillainCommand.ExecuteNonQuery();
                if (result == 1)
                {
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }
                insertVillainCommand.Parameters.Clear();
            }
        }

        public static void InsertMinion(SqlParameter minionNameParameter, SqlParameter minionAgeParameter, SqlParameter minionTownIdParameter, SqlConnection connection)
        {
            SqlCommand insertMinionCommand = new SqlCommand(
                @"INSERT INTO Minions (Name, Age, TownId)
                    VALUES
                        (@minionName, @minionAge, @minionTownId)",
                connection);

            insertMinionCommand.Parameters.Add(minionNameParameter);
            insertMinionCommand.Parameters.Add(minionAgeParameter);
            insertMinionCommand.Parameters.Add(minionTownIdParameter);

            using (insertMinionCommand)
            {
                insertMinionCommand.ExecuteNonQuery();
                insertMinionCommand.Parameters.Clear();
            }
        }

        public static int GetMinionId(SqlParameter minionNameParameter, SqlConnection connection)
        {
            int minionId;
            SqlCommand selectMinionIdCommand = new SqlCommand(
                @"SELECT Id
                    FROM Minions
                    WHERE Name = @minionName",
                connection);

            selectMinionIdCommand.Parameters.Add(minionNameParameter);

            using (selectMinionIdCommand)
            {
                minionId = Convert.ToInt32(selectMinionIdCommand.ExecuteScalar());
                selectMinionIdCommand.Parameters.Clear();
            }
            return minionId;
        }

        public static void InsertMinionVillainId(string minionName, string villainName, SqlParameter minionIdParameter, SqlParameter villainIdParameter, SqlConnection connection)
        {
            SqlCommand insertMinionVillainCommand = new SqlCommand(
                @"INSERT INTO MinionsVillains (MinionId, VillainId)
                    VALUES
                        (@minionId, @villainId)",
                connection);

            insertMinionVillainCommand.Parameters.Add(minionIdParameter);
            insertMinionVillainCommand.Parameters.Add(villainIdParameter);

            using (insertMinionVillainCommand)
            {
                int result = insertMinionVillainCommand.ExecuteNonQuery();
                if (result == 1)
                {
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }
                insertMinionVillainCommand.Parameters.Clear();
            }
        }
    }
}