using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace KlasePodataka
{
    public class SQL
    {
        private string pstringconection;

        public SQL(string conectionstring)
        {
            pstringconection = conectionstring;
        }



        public SqlConnection kreirajKonekciju()
        {
            SqlConnection konekcija = new SqlConnection();
            konekcija.ConnectionString = pstringconection;
            konekcija.Open();
            return konekcija;
        }

        public DataTable vratiIzBaze(SqlConnection konekcija, String upit)
        {
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = upit;
            komanda.Connection = konekcija;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komanda);
            DataTable dataTable = new DataTable("Kud");
            dataAdapter.Fill(dataTable); 

            return dataTable;

        }

        public SqlCommand radSaUpitom(SqlConnection konekcija, String upit)
        {
            SqlCommand komanda = new SqlCommand();
            komanda.CommandText = upit;
            komanda.Connection = konekcija;
            return komanda;
        }

    }
}

