using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using System.Collections;
using System.Collections.ObjectModel;

namespace DataAccessLayer.Repositories {
    public class BestuurderRepository: IBestuurderRepository {
        private string connectionString;
        public BestuurderRepository(string connectionString) {
            this.connectionString = connectionString;
        }

        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool ExistsBestuurder(int id, string rijksreg = "")
        {
            string query = string.Empty;
            if (!string.IsNullOrWhiteSpace(rijksreg))
            {
                query = "select count(*) from dbo.bestuurders where rijksregisternummer =@rijksregisternummer"; ;
            } else query = "select count(*) from dbo.bestuurders where id =@id";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@rijksregisternummer", rijksreg);
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
            query += " select scope_identity()";
            SqlConnection connection = getConnection();

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            using(SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                try
                {
                    
                    command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.NVarChar));
                    SqlParameter _postcode = new SqlParameter("@postcode", bestuurder.Postcode == null ? DBNull.Value : bestuurder.Postcode);
                    _postcode.IsNullable = true;
                    _postcode.Direction = ParameterDirection.Input;
                    _postcode.SqlDbType = SqlDbType.Int;
                    command.Parameters.Add(_postcode);
                    SqlParameter _gemeente = new SqlParameter("@gemeente", bestuurder.Gemeente == string.Empty ? DBNull.Value : bestuurder.Gemeente);
                    _gemeente.Direction = ParameterDirection.Input;
                    _gemeente.SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.Add(_gemeente);
                    SqlParameter _straat = new SqlParameter("@straat", bestuurder.Straat == string.Empty ? DBNull.Value : bestuurder.Straat);
                    _straat.Direction = ParameterDirection.Input;
                    _straat.SqlDbType = SqlDbType.NVarChar;                  
                    command.Parameters.Add(_straat);
                    SqlParameter _huisnummer = new SqlParameter("@huisnummer", bestuurder.Huisnummer == string.Empty ? DBNull.Value : bestuurder.Huisnummer);
                    _huisnummer.Direction = ParameterDirection.Input;
                    _huisnummer.SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.Add(_huisnummer);
                    command.Parameters.Add(new SqlParameter("@geboortedatum", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@rijksregisternummer", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@rijbewijs", SqlDbType.NVarChar));

                    command.Parameters["@naam"].Value = bestuurder.Naam;
                    command.Parameters["@voornaam"].Value = bestuurder.Voornaam;
                    command.Parameters["@geboortedatum"].Value = bestuurder.GeboorteDatum;
                    command.Parameters["@rijksregisternummer"].Value = bestuurder.RijksregisterNummer;
                    command.Parameters["@rijbewijs"].Value = bestuurder.Rijbewijs;

                    bestuurder.Id = Convert.ToInt32(command.ExecuteScalar());
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
                finally
                {
                    connection.Close();                        
                }

            }
        }
        public void DeleteBestuurder(int id)
        {
            if (ExistsBestuurder(id))
            {
                string query = "delete from dbo.bestuurders where id=@id";
                SqlConnection connection = getConnection();
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                        command.Parameters["@id"].Value = id;
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
            else throw new Exception("Bestuurder id bestaat niet.");
        }

        public void UpdateBestuurder(Bestuurder bestuurder, int id)
        {
            if (ExistsBestuurder(id))
            {
                string query = "update dbo.bestuurders set naam=@naam, voornaam = @voornaam, postcode = @postcode, gemeente = @gemeente, straat = @straat, huisnummer = @huisnummer, geboortedatum = @geboortedatum, rijksregisternummer = @rijksregisternummer, rijbewijs = @rijbewijs where id=@id";
                SqlConnection connection = getConnection();
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    try
                    {
                        connection.BeginTransaction();
                        command.Parameters.AddWithValue("@id", (int)id);
                        command.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@voornaam", SqlDbType.NVarChar));
                        SqlParameter _postcode = new SqlParameter("@postcode", bestuurder.Postcode == null ? DBNull.Value : bestuurder.Postcode);
                        _postcode.IsNullable = true;
                        _postcode.Direction = ParameterDirection.Input;
                        _postcode.SqlDbType = SqlDbType.Int;
                        command.Parameters.Add(_postcode);
                        SqlParameter _gemeente = new SqlParameter("@gemeente", bestuurder.Gemeente == string.Empty ? DBNull.Value : bestuurder.Gemeente);
                        _gemeente.Direction = ParameterDirection.Input;
                        _gemeente.SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.Add(_gemeente);
                        SqlParameter _straat = new SqlParameter("@straat", bestuurder.Straat == string.Empty ? DBNull.Value : bestuurder.Straat);
                        _straat.Direction = ParameterDirection.Input;
                        _straat.SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.Add(_straat);
                        SqlParameter _huisnummer = new SqlParameter("@huisnummer", bestuurder.Huisnummer == string.Empty ? DBNull.Value : bestuurder.Huisnummer);
                        _huisnummer.Direction = ParameterDirection.Input;
                        _huisnummer.SqlDbType = SqlDbType.NVarChar;
                        command.Parameters.Add(_huisnummer);
                        command.Parameters.Add(new SqlParameter("@geboortedatum", SqlDbType.DateTime));
                        command.Parameters.Add(new SqlParameter("@rijksregisternummer", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@rijbewijs", SqlDbType.NVarChar));

                        command.Parameters["@naam"].Value = bestuurder.Naam;
                        command.Parameters["@voornaam"].Value = bestuurder.Voornaam;
                        command.Parameters["@geboortedatum"].Value = bestuurder.GeboorteDatum;
                        command.Parameters["@rijksregisternummer"].Value = bestuurder.RijksregisterNummer;
                        command.Parameters["@rijbewijs"].Value = bestuurder.Rijbewijs;

                        command.ExecuteScalar();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else throw new Exception("Bestuurder id bestaat niet.");
        }

        public ObservableCollection<Bestuurder> FetchBestuurders(string naam, string voornaam, string geboortedatum)
        {
            ObservableCollection<Bestuurder> bestuurders = new ObservableCollection<Bestuurder>();

            string query = "";
            string queryNaam = "select top (50) * from bestuurders b left join voertuigen v on v.bestuurderid = b.id left join tankkaarten t on t.Bestuurderid = b.id where naam like @naam";
            string queryVoornaam = "select top (50) * from bestuurders b left join voertuigen v on v.bestuurderid = b.id left join tankkaarten t on t.Bestuurderid = b.id where voornaam like @voornaam";
            string queryGeboorteDatum = "select top(50) * from bestuurders b left join voertuigen v on v.bestuurderid = b.id left join tankkaarten t on t.Bestuurderid = b.id where geboortedatum like @geboortedatum";
            string queryWithVoornaam = " and voornaam like @voornaam";
            string queryWithGeboortedatum = " and geboortedatum like @geboortedatum";

            SqlConnection connection = getConnection();

            if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(voornaam) && !string.IsNullOrWhiteSpace(geboortedatum))
            {
                query = queryNaam + queryWithVoornaam + queryWithGeboortedatum;
            }
            else if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(voornaam))
            {
                query = queryNaam + queryWithVoornaam;
            }
            else if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(geboortedatum))
            {
                query = queryNaam + queryWithGeboortedatum;
            }
            else if (!string.IsNullOrWhiteSpace(voornaam) && !string.IsNullOrWhiteSpace(geboortedatum))
            {
                query = queryVoornaam + queryWithGeboortedatum;
            }
            else if (!string.IsNullOrWhiteSpace(naam))
            {
                query = queryNaam;
            }
            else if (!string.IsNullOrWhiteSpace(voornaam))
            {
                query = queryVoornaam;
            }
            else if (geboortedatum != null)
            {
                query = queryGeboorteDatum;
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("naam", naam);
                    command.Parameters.AddWithValue("voornaam", voornaam);
                    command.Parameters.AddWithValue("geboortedatum", geboortedatum);
                    IDataReader reader = command.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            Bestuurder bs = new Bestuurder(reader["naam"].ToString(), reader["voornaam"].ToString(), (DateTime)reader["geboortedatum"], reader["rijksregisternummer"].ToString(), reader["rijbewijs"].ToString(), reader["gemeente"].ToString(), reader["straat"].ToString(), reader["huisnummer"].ToString(), reader.GetNullableInt("postcode"));
                            bs.Id = (int)reader["id"];
                            bs.Voertuig = new Voertuig(reader.GetNullableString("merk"), reader.GetNullableString("model"), reader.GetNullableString("chassisnummer"), reader.GetNullableString("nummerplaat"), reader.GetNullableString("brandstof"), reader.GetNullableString("typewagen"), reader.GetNullableString("kleur"), reader.GetNullableInt("aantaldeuren"), bs.Id);
                            bs.Tankkaart = new Tankkaart(reader.GetNullableDateTime("geldigheidsdatum"), reader.GetNullableString("pincode"), reader.GetNullableString("brandstof"), bs.Id, reader.GetNullableInt("kaartnummer"));
                            bestuurders.Add(bs);
                        }
                    } while (reader.NextResult());
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return bestuurders;
        }
    }   
}
