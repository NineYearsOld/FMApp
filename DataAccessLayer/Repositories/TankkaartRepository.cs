using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.StaticData;
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
        public bool ExistsTankkaart(int id)
        {
            string query = "select count(*) from dbo.tankkaarten where kaartnummer=@kaartnummer";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@kaartnummer", id);
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

            string query = "insert into dbo.tankkaarten (geldigheidsdatum, pincode, brandstof, bestuurderid) values(@geldigheidsdatum, @pincode, @brandstof, @bestuurderid)";
            query += " select scope_identity()";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                //TODO: Use transactions
                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@bestuurderid", tankkaart.BestuurderId);

                    command.Parameters.Add(new SqlParameter ("@geldigheidsdatum", SqlDbType.Date));
                    command.Parameters.Add(new SqlParameter("@pincode", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@brandstof", SqlDbType.NVarChar));

                    command.Parameters["@geldigheidsdatum"].Value = tankkaart.GeldigheidsDatum;
                    command.Parameters["@pincode"].Value = tankkaart.Pincode;
                    command.Parameters["@brandstof"].Value = tankkaart.Brandstoffen;

                    tankkaart.KaartNummer = Convert.ToInt32(command.ExecuteScalar());
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
            if (ExistsTankkaart(kaartnummer))
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
            if (ExistsTankkaart(kaartnummer))
            {
                string query = "update dbo.taankkaarten set geldigheidsdatum = @geldigheidsdatum, pincode = @pincode, brandstof = @brandstof, bestuurderid = @bestuurderid, where kaartnummer=@kaartnummer";
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
            if (ExistsTankkaart(kaartnummer))
            {
                SqlConnection connection = getConnection();
                Tankkaart t;

                string query = "select kaartnummer, geldigheidsdatum, pincode, brandstof, bestuurderid from dbo.tankkaarten where kaartnummer=@kaartnummer";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@kaartnummer", kaartnummer); 
                        IDataReader reader = command.ExecuteReader();
                        reader.Read();
                        t = new Tankkaart((DateTime)reader["geldigheidsdatum"], (string)reader["pincode"], (string)reader["brandstof"], (int)reader["bestuurderId"], (int)reader["kaartnummer"]);
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

