using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;

namespace DataAccessLayer.Repositories {
    public class BestuurderRepository: IBestuurderRepository {
        private string connectionString;
        public BestuurderRepository(string connectionString) {
            this.connectionString = connectionString;
        }

        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool BestaatBestuurder(int id)
        {
            string query = "select count(*) from dbo.bestuurders where id =@id";
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

        public void CreateBestuurder(Bestuurder bestuurder) {

            string query = "insert into dbo.bestuurders (naam, voornaam, postcode, gemeente, straat, huisnummer, geboortedatum, rijksregisternummer, rijbewijs) values(@naam, @voornaam, @postcode, @gemeente, @straat, @huisnummer, @geboortedatum, @rijksregisternummer, @rijbewijs)";
            SqlConnection connection = getConnection();
            using(SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.NVarChar));
                    SqlParameter _postcode = new SqlParameter("@postcode", SqlDbType.Int);
                    _postcode.IsNullable = true;
                    command.Parameters.Add(_postcode);
                    SqlParameter _gemeente = new SqlParameter("@gemeente", SqlDbType.NVarChar);
                    _gemeente.IsNullable = true;
                    command.Parameters.Add(_gemeente);
                    SqlParameter _straat = new SqlParameter("@straat", SqlDbType.NVarChar);
                    _straat.IsNullable = true;
                    command.Parameters.Add(_straat);
                    SqlParameter _huisnummer = new SqlParameter("@huisnummer", SqlDbType.NVarChar);
                    _huisnummer.IsNullable = true;
                    command.Parameters.Add(_huisnummer);
                    command.Parameters.Add(new SqlParameter("@geboortedatum", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@rijksregisternummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@rijbewijs", SqlDbType.NVarChar));

                    command.Parameters["@naam"].Value = bestuurder.Naam;
                    command.Parameters["@voornaam"].Value = bestuurder.Voornaam;
                    command.Parameters["@postcode"].Value = bestuurder.Postcode;
                    command.Parameters["@gemeente"].Value = bestuurder.Gemeente;
                    command.Parameters["@straat"].Value = bestuurder.Straat;
                    command.Parameters["@huisnummer"].Value = bestuurder.Huisnummer;
                    command.Parameters["@geboortedatum"].Value = bestuurder.GeboorteDatum;
                    command.Parameters["@rijksregisternummer"].Value = bestuurder.RijksregisterNummer;
                    command.Parameters["@rijbewijs"].Value = bestuurder.Rijbewijs;

                    command.ExecuteNonQuery();
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
        public void DeleteBestuurder(int id)
        {
            string query = "delete from dbo.bestuurders where id=@id";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters["@id"].Value = id;
                    command.ExecuteNonQuery();
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

        public void UpdateBestuurder(Bestuurder bestuurder)
        {
            string query = "update from dbo.bestuurders set naam=@naam, where id=@id";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters["@id"].Value = bestuurder.Id;
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters["@naam"].Value = bestuurder.Naam;
                    command.ExecuteNonQuery();
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

        public Bestuurder ToonDetails(int id)
        {
            string query = "select * from dbo.bestuurders where id=@id";
            SqlConnection connection = getConnection();
            Bestuurder bestuurder;
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    IDataReader reader = command.ExecuteReader();
                    reader.Read();
                    bestuurder = new Bestuurder((string)reader["naam"], (string)reader["voornaam"], (DateTime)reader["geboortedatum"], (string)reader["rijksregisternummer"], (string)reader["rijbewijs"]);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return bestuurder;
        }
    }
}
