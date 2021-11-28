using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Repositories {
    public class TankkaartRepository: ITankkaartRepository {
        private string connectionString;
        public TankkaartRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public bool BestaatTankkaart(int id)
        {
            string query = "select count(*) from dbo.tankkaarten where id =@id";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
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

        public void CreateTankkaart(Tankkaart tankkaart) {

            string query = "insert into dbo.bestuurders (kaartnummer, geldigheidsdatum, pincode, brandstof, bestuurderid) values(@kaartnummer, @geldigheidsdatum, @pincode, @pincode, @brandstof, @bestuurderid)";
            query += " select scope_identity()";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                //TODO: Use transactions
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@bestuurderid", tankkaart.bestuurderId);
                    command.Parameters.AddWithValue("@kaartnummer", tankkaart.KaartNummer);
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

        public void DeleteTankkaart(int kaartnummer) {
            if (BestaatTankkaart(kaartnummer))
            {
                string query = "delete from dbo.tankkaarten where kaartnummer=@kaartnummer";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("kaartnummer", kaartnummer);
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
            else throw new Exception("Bestuurder id bestaat niet.");
        }

        public void UpdateTankkaart(int kaartnummer) { // Nog uit te werken
            if (BestaatTankkaart(kaartnummer))
            {
                string query = "update dbo.taankkaarten set geldigheidsdatum = @geldigheidsdatum, pincode = @pincode, brandstof = @brandstof, bestuurderid = @bestuurderid, where kaartnummer=@kaartnummer";
                query += " select scope_identity()";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@kaartnummer", (int)kaartnummer);

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
            else throw new Exception("Tankkaart id bestaat niet.");
        }

        public Tankkaart ToonDetails(int kaartnummer) {
            if (BestaatTankkaart(kaartnummer))
            {
                SqlConnection connection = getConnection();
                Tankkaart t;

                string query = "select ...";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@kaartnummer", kaartnummer); 
                        IDataReader reader = command.ExecuteReader();
                        reader.Read();
                        t = new Tankkaart((int)reader["kaartnummer"], (DateTime)reader["geldigheidsdatum"], (string)reader["pincode"], (string)reader["brandstof"], (int)reader["bestuurderId"]);
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
                return t;
            }
            else throw new Exception();
        }
    }
}

