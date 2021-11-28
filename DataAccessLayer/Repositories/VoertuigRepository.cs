using BusinessLayer.Entities;
using BusinessLayer.StaticData;
using BusinessLayer.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BusinessLayer.Repositories {
    public class VoertuigRepository: IVoertuigRepository {
        private string connectionString;
        public VoertuigRepository(string connectionString) {
            this.connectionString = connectionString;
        }
        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public bool BestaatVoertuig(string id)
        {
            string query = "select count(*) from dbo.voertuigen where chassisnummer =@chassisnummer";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@chassisnummer", id);
                    int qr = (int)command.ExecuteScalar();
                    if (qr > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                catch (Exception)
                {

                    throw;
                }

                finally
                {
                    connection.Close();
                }

            }
        }


        public void CreateVoertuig(Voertuig voertuig) {

            string query = "insert into dbo.voertuigen (chassisnummer, merk, model, nummerplaat, brandstof, typewagen, kleur, aantaldeuren, bestuurderid) values(@chassisnummer, @merk, @model, @nummerplaat, @brandstof, @typewagen, @kleur, @aantaldeuren, @bestuurderid)";
            query += " select scope_identity()";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                //TODO: Use transactions
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@bestuurderid", voertuig.ChassisNummer);
                    command.Parameters.AddWithValue("@kaartnummer", voertuig.BestuurderId);
                    command.ExecuteScalar();
                }
                catch (Exception)
                {

                }
                finally
                {
                    connection.Close();

                }

            }
        }

        public void DeleteVoertuig(string chassisnummer) {
            if (BestaatVoertuig(chassisnummer))
            {
                string query = "delete from dbo.tankkaarten where kaartnummer=@kaartnummer";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("kaartnummer", chassisnummer);
                        command.ExecuteScalar();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
            else throw new Exception("Chassisnummer bestaat niet.");

        }

        public void UpdateVoertuig(string chassisnummer) { // Nog uit te werken
            if (BestaatVoertuig(chassisnummer))
            {
                string query = "update dbo.voertuigen set merk = @merk, model = @model, nummerplaat = @nummerplaat, brandstof = @brandstof, typewagen = @typewagen, kleur = @kleur, aantaldeuren = @aantaldeuren, bestuurderId = bestuurderId where chassisnummer=@chassisnummer";
                query += " select scope_identity()";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@chassisnummer", chassisnummer);

                        command.ExecuteScalar();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else throw new Exception("Chassisnummer bestaat niet.");
        }

        public Voertuig ToonDetails(string chassisnummer) {
            if (BestaatVoertuig(chassisnummer))
            {
                SqlConnection connection = getConnection();
                Voertuig v;

                string query = "select ...";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@chassisnummer", chassisnummer);
                        IDataReader reader = command.ExecuteReader();
                        reader.Read();
                        v = new Voertuig((string)reader["merk"], (string)reader["model"], (string)reader["chassisnummer"], (string)reader["nummerplaat"], (Brandstoffen)reader["brandstof"], (WagenTypes)reader["typewagen"], (string)reader["kleur"], (int)reader["aantaldeuren"], (int)reader["bestuurderid"]);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
                return v;
            }
            else throw new Exception();
        }
    }
}
