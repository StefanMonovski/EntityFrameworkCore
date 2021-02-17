using Microsoft.Data.SqlClient;
using System;

namespace _01.InitialSetup
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection createDatabaseConnection = new SqlConnection("Server=.;Database=master;Integrated Security=true");            
            using (createDatabaseConnection)
            {
                createDatabaseConnection.Open();
                SqlCommand createDatabaseCommand = new SqlCommand(@"CREATE DATABASE MinionsDB;", createDatabaseConnection);
                using (createDatabaseCommand)
                {
                    createDatabaseCommand.ExecuteNonQuery();
                }                
            }

            SqlConnection createTablesConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");            
            using (createTablesConnection)
            {
                createTablesConnection.Open();
                SqlCommand createTablesCommand = new SqlCommand(
                    @"CREATE TABLE Countries (
                        Id INT IDENTITY,
                        CONSTRAINT PK_CountryId PRIMARY KEY(Id),
                        Name VARCHAR(30) NOT NULL,
                        );

                    CREATE TABLE Towns (
                        Id INT IDENTITY,
                        CONSTRAINT PK_TownId PRIMARY KEY(Id),
                        Name VARCHAR(30) NOT NULL,
                        CountryCode INT NOT NULL,
                        CONSTRAINT FK_TownCountryCode FOREIGN KEY(CountryCode) REFERENCES Countries(Id),           
                        );

                    CREATE TABLE Minions (
                        Id INT IDENTITY,
                        CONSTRAINT PK_MinionId PRIMARY KEY(Id),
                        Name VARCHAR(30) NOT NULL,
                        Age INT NOT NULL,
                        TownId INT NOT NULL,
                        CONSTRAINT FK_MinionTownId FOREIGN KEY(TownId) REFERENCES Towns(Id),                
                        );

                    CREATE TABLE EvilnessFactors (
                        Id INT IDENTITY,
                        CONSTRAINT PK_EvilnessFactorId PRIMARY KEY(Id),
                        Name VARCHAR(30) NOT NULL,                
                        );

                    CREATE TABLE Villains (
                        Id INT IDENTITY,
                        CONSTRAINT PK_VillainId PRIMARY KEY(Id),
                        Name VARCHAR(30) NOT NULL,
                        EvilnessFactorId INT NOT NULL,
                        CONSTRAINT FK_VillainEvilnessFactorId FOREIGN KEY(EvilnessFactorId) REFERENCES EvilnessFactors(Id),               
                        );

                    CREATE TABLE MinionsVillains (
                        MinionId INT NOT NULL,
                        CONSTRAINT FK_MinionVillainMinionId FOREIGN KEY(MinionId) REFERENCES Minions(Id),
                        VillainId INT NOT NULL,
                        CONSTRAINT FK_MinionVillainVillainId FOREIGN KEY(VillainId) REFERENCES Villains(Id),
                        CONSTRAINT PK_MinionVillainId PRIMARY KEY(MinionId, VillainId),                
                        );",
                    createTablesConnection);
                using (createTablesCommand)
                {
                    createTablesCommand.ExecuteNonQuery();
                }                
            }

            SqlConnection insertTablesConnection = new SqlConnection("Server=.;Database=MinionsDB;Integrated Security=true");            
            using (insertTablesConnection)
            {
                insertTablesConnection.Open();
                SqlCommand insertTablesCommand = new SqlCommand(
                    @"INSERT INTO Countries (Name)
                        VALUES
                            ('Bulgaria'),
                            ('England'),
                            ('Cyprus'),
                            ('Germany'),
                            ('Norway');

                    INSERT INTO Towns (Name, CountryCode)
                        VALUES
                            ('Plovdiv', 1),
                            ('Varna', 1),
                            ('Burgas', 1),
                            ('Sofia', 1),
                            ('London', 2),
                            ('Southampton', 2),
                            ('Bath', 2),
                            ('Liverpool', 2),
                            ('Berlin', 3),
                            ('Frankfurt', 3),
                            ('Oslo', 4);

                    INSERT INTO Minions (Name, Age, TownId)
                        VALUES
                            ('Bob', 42, 3),
                            ('Kevin', 1, 1),
                            ('Bob ', 32, 6),
                            ('Simon', 45, 3),
                            ('Cathleen', 11, 2),
                            ('Carry ', 50, 10),
                            ('Becky', 125, 5),
                            ('Mars', 21, 1),
                            ('Misho', 5, 10),
                            ('Zoe', 125, 5),
                            ('Json', 21, 1);

                    INSERT INTO EvilnessFactors (Name)
                        VALUES
                            ('Super good'),
                            ('Good'),
                            ('Bad'),
                            ('Evil'),
                            ('Super evil');

                    INSERT INTO Villains (Name, EvilnessFactorId)
                        VALUES
                            ('Gru', 2),
                            ('Victor', 1),
                            ('Jilly', 3),
                            ('Miro', 4),
                            ('Rosen', 5),
                            ('Dimityr', 1),
                            ('Dobromir', 2);

                    INSERT INTO MinionsVillains (MinionId, VillainId)
                        VALUES
                            (4, 2),
                            (1, 1),
                            (5, 7),
                            (3, 5),
                            (2, 6),
                            (11, 5),
                            (8, 4),
                            (9, 7),
                            (7, 1),
                            (1, 3),
                            (7, 3),
                            (5, 3),
                            (4, 3),
                            (1, 2),
                            (2, 1),
                            (2, 7);",
                    insertTablesConnection);
                using (insertTablesCommand)
                {
                    insertTablesCommand.ExecuteNonQuery();
                }
            }
        }
    }
}