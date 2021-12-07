﻿using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using System.Collections;

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

        public bool ExistsBestuurder(int id)
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
            query += " select scope_identity()";
            SqlConnection connection = getConnection();
            using(SqlCommand command = new SqlCommand(query, connection))
            {
                //TODO: Use transactions
                try
                {
                    connection.Open();
                    
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
            if (ExistsBestuurder(id))
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
            else throw new Exception("Bestuurder id bestaat niet.");
        }

        public void UpdateBestuurder(Bestuurder bestuurder, int id)
        {
            if (ExistsBestuurder(id))
            {
                string query = "update dbo.bestuurders set naam=@naam, voornaam = @voornaam, postcode = @postcode, gemeente = @gemeente, straat = @straat, huisnummer = @huisnummer, geboortedatum = @geboortedatum, rijksregisternummer = @rijksregisternummer, rijbewijs = @rijbewijs where id=@id";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
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

        public List<Bestuurder> FetchBestuurders(string naam, string voornaam)
        {
            List<Bestuurder> bestuurders = new List<Bestuurder>();

            string query = "";
            string queryNaam = "select * from dbo.bestuurders where naam like @naam";
            string queryVoornaam = "select * from dbo.bestuurders where voornaam like @voornaam";
            string queryWithVoornaam = " and voornaam like @voornaam";

            SqlConnection connection = getConnection();

            if (!string.IsNullOrWhiteSpace(naam) && !string.IsNullOrWhiteSpace(voornaam))
            {
                query = queryNaam + queryWithVoornaam;
            }
            else if (!string.IsNullOrWhiteSpace(naam))
            {
                query = queryNaam;
            }
            else if (!string.IsNullOrWhiteSpace(voornaam))
            {
                query = queryVoornaam;
            }

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("naam", naam);
                    command.Parameters.AddWithValue("voornaam", voornaam);
                    IDataReader reader = command.ExecuteReader();
                    do
                    {
                        while (reader.Read())
                        {
                            Bestuurder bs = new Bestuurder(reader["naam"].ToString(), reader["voornaam"].ToString(), (DateTime)reader["geboortedatum"], reader["rijksregisternummer"].ToString(), reader["rijbewijs"].ToString(), reader["gemeente"].ToString(), reader["straat"].ToString(), reader["huisnummer"].ToString(), reader.GetNullableInt("postcode"));
                            bs.Id = (int)reader["id"];
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

        public Bestuurder ToonDetails(int id)
        {
            if (ExistsBestuurder(id))
            {
                string query = "select naam, voornaam, geboortedatum, rijksregisternummer, rijbewijs, gemeente, straat, huisnummer, postcode from dbo.bestuurders where id=@id";
                SqlConnection connection = getConnection();
                Bestuurder bestuurder;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@id", (int)id);
                        IDataReader reader = command.ExecuteReader();
                        reader.Read();
                        bestuurder = new Bestuurder((string)reader["naam"], (string)reader["voornaam"], (DateTime)reader["geboortedatum"], (string)reader["rijksregisternummer"], (string)reader["rijbewijs"], reader.GetNullableString("gemeente"), reader.GetNullableString("straat"), reader.GetNullableString("huisnummer"), reader.GetNullableInt("postcode"));
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return bestuurder;
            }
            else throw new Exception("Bestuurder id bestaat niet");
        }
    }   
}
