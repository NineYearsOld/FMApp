using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.StaticData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.Repositories {
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
        public bool ExistsTankkaart(int? id)
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

                    command.Parameters.AddWithValue("@geldigheidsdatum", tankkaart.GeldigheidsDatum);
                    SqlParameter pincode = new SqlParameter("@pincode", tankkaart.Pincode == null ? DBNull.Value : tankkaart.Pincode);
                    pincode.SqlDbType = SqlDbType.NVarChar;
                    pincode.Direction = ParameterDirection.Input;
                    command.Parameters.Add(pincode);
                    SqlParameter brandstof = new SqlParameter("@brandstof", tankkaart.Brandstoffen == null ? DBNull.Value : tankkaart.Brandstoffen);
                    brandstof.SqlDbType = SqlDbType.NVarChar;
                    brandstof.Direction = ParameterDirection.Input;
                    command.Parameters.Add(brandstof);
                    SqlParameter bestuurderId = new SqlParameter("@bestuurderid", tankkaart.BestuurderId == null ? DBNull.Value : tankkaart.BestuurderId);
                    bestuurderId.SqlDbType = SqlDbType.Int;
                    bestuurderId.Direction = ParameterDirection.Input;
                    command.Parameters.Add(bestuurderId);

                    tankkaart.KaartNummer = Convert.ToInt32(command.ExecuteScalar());
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

        public void UpdateTankkaart(Tankkaart tankkaart) { // Nog uit te werken
            if (ExistsTankkaart(tankkaart.KaartNummer))
            {
                string query = "update dbo.tankkaarten set geldigheidsdatum = @geldigheidsdatum, pincode = @pincode, brandstof = @brandstof, bestuurderid = @bestuurderid where kaartnummer=@kaartnummer";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@kaartnummer", tankkaart.KaartNummer);
                        command.Parameters.AddWithValue("@geldigheidsdatum", tankkaart.GeldigheidsDatum);
                        SqlParameter pincode = new SqlParameter("@pincode", tankkaart.Pincode == null ? DBNull.Value : tankkaart.Pincode);
                        pincode.SqlDbType = SqlDbType.NVarChar;
                        pincode.Direction = ParameterDirection.Input;
                        command.Parameters.Add(pincode);
                        SqlParameter brandstof = new SqlParameter("@brandstof", tankkaart.Brandstoffen == null ? DBNull.Value : tankkaart.Brandstoffen);
                        brandstof.SqlDbType = SqlDbType.NVarChar;
                        brandstof.Direction = ParameterDirection.Input;
                        command.Parameters.Add(brandstof);
                        SqlParameter bestuurderId = new SqlParameter("@bestuurderid", tankkaart.BestuurderId == null ? DBNull.Value : tankkaart.BestuurderId);
                        bestuurderId.SqlDbType = SqlDbType.Int;
                        bestuurderId.Direction = ParameterDirection.Input;
                        command.Parameters.Add(bestuurderId);


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
    }
}

