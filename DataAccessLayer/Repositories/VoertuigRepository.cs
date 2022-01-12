using BusinessLayer.Entities;
using BusinessLayer.StaticData;
using BusinessLayer.Interfaces;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.ObjectModel;

namespace DataAccessLayer.Repositories {
    public class VoertuigRepository : IVoertuigRepository {
        private string connectionString;
        public VoertuigRepository(string connectionString) {
            this.connectionString = connectionString;
        }

        public SqlConnection getConnection() {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }

        public bool ExistsVoertuig(string id) {
            string query = "select count(*) from dbo.voertuigen where chassisnummer=@chassisnummer";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection)) {
                try {
                    connection.Open();
                    command.Parameters.AddWithValue("@chassisnummer", id);
                    int qr = (int)command.ExecuteScalar();
                    if (qr > 0) {
                        return true;
                    } else return false;
                } catch (Exception) {

                    throw;
                } finally {
                    connection.Close();
                }

            }
        }

        public void CreateVoertuig(Voertuig voertuig) {

            string query = "insert into dbo.voertuigen (chassisnummer, merk, model, nummerplaat, brandstof, typewagen, kleur, aantaldeuren, bestuurderid) values(@chassisnummer, @merk, @model, @nummerplaat, @brandstof, @typewagen, @kleur, @aantaldeuren, @bestuurderid)";
            query += " select scope_identity()";
            SqlConnection connection = getConnection();
            using (SqlCommand command = new SqlCommand(query, connection)) {
                //TODO: Use transactions
                try {
                    connection.Open();

                    command.Parameters.AddWithValue("@chassisnummer", voertuig.ChassisNummer);
                    SqlParameter parambid = new SqlParameter("@bestuurderid", voertuig.BestuurderId == null? DBNull.Value : voertuig.BestuurderId);
                    parambid.Direction = ParameterDirection.Input;
                    parambid.SqlDbType = SqlDbType.Int;
                    command.Parameters.Add(parambid);

                    command.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@brandstof", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@typewagen", SqlDbType.NVarChar));
                    SqlParameter paramkleur = new SqlParameter("@kleur", voertuig.Kleur == null ? DBNull.Value : voertuig.Kleur);
                    paramkleur.Direction = ParameterDirection.Input;
                    paramkleur.SqlDbType = SqlDbType.NVarChar;
                    command.Parameters.Add(paramkleur);
                    SqlParameter paramdeuren = new SqlParameter("@aantaldeuren", voertuig.AantalDeuren == null ? DBNull.Value : voertuig.AantalDeuren);
                    paramdeuren.Direction = ParameterDirection.Input;
                    paramdeuren.SqlDbType = SqlDbType.Int;
                    command.Parameters.Add(paramdeuren);

                    command.Parameters["@merk"].Value = voertuig.Merk;
                    command.Parameters["@model"].Value = voertuig.Model;
                    command.Parameters["@nummerplaat"].Value = voertuig.Nummerplaat;
                    command.Parameters["@brandstof"].Value = voertuig.Brandstoffen;
                    command.Parameters["@typewagen"].Value = voertuig.TypeWagen;
                    command.Parameters["@kleur"].Value = voertuig.Kleur;
                    command.Parameters["@aantaldeuren"].Value = voertuig.AantalDeuren;

                    voertuig.ChassisNummer = command.ExecuteScalar().ToString();
                } catch (Exception) {
                    throw;
                } finally {
                    connection.Close();

                }

            }
        }

        public void DeleteVoertuig(string chassisnummer) {
            if (ExistsVoertuig(chassisnummer)) {
                string query = "delete from dbo.voertuigen where chassisnummer=@chassisnummer";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection)) {
                    try {
                        connection.Open();
                        command.Parameters.AddWithValue("chassisnummer", chassisnummer);
                        command.ExecuteScalar();
                    } catch (Exception) {

                        throw;
                    } finally {
                        connection.Close();
                    }

                }
            } else throw new Exception("Chassisnummer bestaat niet.");

        }

        public void UpdateVoertuig(Voertuig voertuig, string chassisnummer) { // Nog uit te werken
            if (ExistsVoertuig(chassisnummer)) {
                string query = "update dbo.voertuigen set merk = @merk, model = @model, nummerplaat = @nummerplaat, brandstof = @brandstof, typewagen = @typewagen, kleur = @kleur, aantaldeuren = @aantaldeuren, bestuurderId = bestuurderId where chassisnummer=@chassisnummer";
                SqlConnection connection = getConnection();
                using (SqlCommand command = new SqlCommand(query, connection)) {
                    try {
                        connection.Open();

                        command.Parameters.AddWithValue("@chassisnummer", voertuig.ChassisNummer);
                        command.Parameters.AddWithValue("@bestuurderid", voertuig.BestuurderId);

                        command.Parameters.Add(new SqlParameter("@merk", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@model", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@nummerplaat", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@brandstof", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@typewagen", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@kleur", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@aantaldeuren", SqlDbType.Int));

                        command.Parameters["@merk"].Value = voertuig.Merk;
                        command.Parameters["@model"].Value = voertuig.Model;
                        command.Parameters["@nummerplaat"].Value = voertuig.Nummerplaat;
                        command.Parameters["@brandstof"].Value = voertuig.Brandstoffen;
                        command.Parameters["@typewagen"].Value = voertuig.TypeWagen;
                        command.Parameters["@kleur"].Value = voertuig.Kleur;
                        command.Parameters["@aantaldeuren"].Value = voertuig.AantalDeuren;

                        command.ExecuteScalar();
                    } catch (Exception) {

                        throw;
                    } finally {
                        connection.Close();
                    }
                }
            } else throw new Exception("Chassisnummer bestaat niet.");
        }

        public Voertuig ToonDetails(string chassisnummer) {
            if (ExistsVoertuig(chassisnummer)) {
                SqlConnection connection = getConnection();
                Voertuig v;

                string query = "select chassisnummer, merk, model, nummerplaat, brandstof, typewagen, kleur, aantaldeuren, bestuurderid where chassisnummer=@chassisnummer";
                using (SqlCommand command = new SqlCommand(query, connection)) {
                    try {
                        connection.Open();
                        command.Parameters.AddWithValue("@chassisnummer", chassisnummer);
                        IDataReader reader = command.ExecuteReader();
                        reader.Read();
                        v = new Voertuig((string)reader["merk"], (string)reader["model"], (string)reader["chassisnummer"], (string)reader["nummerplaat"], (string)reader["brandstof"], (string)reader["typewagen"], (string)reader["kleur"], (int)reader["aantaldeuren"], (int)reader["bestuurderid"]);
                    } catch (Exception) {

                        throw;
                    } finally {
                        connection.Close();
                    }

                }
                return v;
            } else throw new Exception();
        }

        public ObservableCollection<Voertuig> GetVoertuigen(string merk, string model, string nummerplaat) {
            ObservableCollection<Voertuig> voertuigen = new ObservableCollection<Voertuig>();

            string query;
            string queryMerk = "select top (50) * from dbo.voertuigen where merk like @merk";
            string queryModel = "select top (50) * from dbo.voertuigen where model like @model";
            string queryNummerplaat = "select top (50) * from dbo.voertuigen where nummerplaat like @nummerplaat";
            string queryWithModel = " and model like @model";
            string queryWithNummerplaat = " and nummerplaat like @ nummerplaat";

            if (!string.IsNullOrWhiteSpace(merk) && !string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(nummerplaat)) {
                query = queryMerk + queryWithModel + queryWithNummerplaat;
            } else if (!string.IsNullOrWhiteSpace(merk) && !string.IsNullOrWhiteSpace(model)) {
                query = queryMerk + queryWithModel;
            } else if (!string.IsNullOrWhiteSpace(merk) && !string.IsNullOrWhiteSpace(nummerplaat)) {
                query = queryMerk + queryWithNummerplaat;
            } else if (!string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(nummerplaat)) {
                query = queryModel + queryWithNummerplaat;
            } else if (!string.IsNullOrWhiteSpace(merk)) {
                query = queryMerk;
            } else if (!string.IsNullOrWhiteSpace(model)) {
                query = queryModel;
            } else if (!string.IsNullOrWhiteSpace(nummerplaat)) {
                query = queryNummerplaat;
            } else {
                return voertuigen;
            }

            SqlConnection conn = getConnection();
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("merk", merk);
            command.Parameters.AddWithValue("model", model);
            command.Parameters.AddWithValue("nummerplaat", nummerplaat);

            try {
                using (command) {
                    conn.Open();
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        Voertuig v = new Voertuig(reader["merk"].ToString(), reader["model"].ToString(), reader["chassisnummer"].ToString(), reader["nummerplaat"].ToString(), reader["brandstof"].ToString(), reader["typewagen"].ToString(), reader.GetNullableString("kleur"), reader.GetNullableInt("aantaldeuren"), reader.GetNullableInt("bestuurderid"));
                        voertuigen.Add(v);
                    }
                }
            } catch {

            } finally {
                conn.Close();
            }

            return voertuigen;
        }
    }
}
