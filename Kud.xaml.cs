using ProdavnicaPica;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KlasePodataka;

namespace Kudovi

{
   
    public partial class Kud : Window
    {
        SQL sqlUtils;
        public Kud()
        {
            InitializeComponent();
            sqlUtils= new SQL(ConfigurationManager.ConnectionStrings["connKudovi"].ConnectionString);
            prikaziKudove();
        }

        private void prikaziKudove()
        {
            SqlConnection konekcija = sqlUtils.kreirajKonekciju();
            String upit = "SELECT * FROM [KUD]";
            DataTable dataTabela = sqlUtils.vratiIzBaze(konekcija, upit);
            DataGridKud.ItemsSource = dataTabela.DefaultView;
        }

        private void DataGridKud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtKudId.Text = dr["ID"].ToString();
                txtAdresa.Text = dr["Adresa"].ToString();
                txtMesto.Text = dr["Mesto"].ToString();
                txtSekcija.Text = dr["Sekcija"].ToString();
                txtDiploma.Text = dr["Diploma"].ToString();
                txtNaziv.Text = dr["Naziv"].ToString();


            }
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = sqlUtils.kreirajKonekciju();
            String upit = "INSERT INTO [KUD] (Adresa, Mesto, Sekcija, Diploma, Naziv) VALUES (@Adresa, @Mesto, @Sekcija, @Diploma, @Naziv)";
            SqlCommand komanda = sqlUtils.radSaUpitom(konekcija, upit);
            komanda.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
            komanda.Parameters.AddWithValue("@Mesto", txtMesto.Text);
            komanda.Parameters.AddWithValue("@Sekcija", txtSekcija.Text);
            komanda.Parameters.AddWithValue("@Diploma", txtDiploma.Text);
            komanda.Parameters.AddWithValue("@Naziv", txtNaziv.Text);

            if (txtAdresa.Text != "" && txtMesto.Text != "" && txtSekcija.Text != "" && txtDiploma.Text != "" && txtNaziv.Text != "")
            {
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o kudu su uspešno upisani");
                    prikaziKudove();
                }
                ponistiUnosTxt();
            }
            else
            {
                MessageBox.Show("Greska: Polja moraju biti popunjena.");
            }
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection konekcija = sqlUtils.kreirajKonekciju();
            String upit = "UPDATE [KUD] SET Adresa = @Adresa, Mesto = @Mesto, Sekcija = @Sekcija, Diploma = @Diploma, Naziv = @Naziv WHERE ID = @KudId";
            SqlCommand komanda = sqlUtils.radSaUpitom(konekcija, upit);
            komanda.Parameters.AddWithValue("@KudId", txtKudId.Text);
            komanda.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
            komanda.Parameters.AddWithValue("@Mesto", txtMesto.Text);
            komanda.Parameters.AddWithValue("@Sekcija", txtSekcija.Text);
            komanda.Parameters.AddWithValue("@Diploma", txtDiploma.Text);
            komanda.Parameters.AddWithValue("@Naziv", txtNaziv.Text);

            if (txtAdresa.Text != "" && txtMesto.Text != "" && txtSekcija.Text != "" && txtDiploma.Text != "" && txtNaziv.Text != "")
            {
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o kudu su uspešno izmenjeni");
                    prikaziKudove();
                }
                ponistiUnosTxt();
            }
            else
            {
                MessageBox.Show("Greska: Polja moraju biti popunjena.");
            }
        }

        private void BtnObrisi_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = sqlUtils.kreirajKonekciju();
            String upit = "DELETE FROM [KUD] WHERE ID = @KudId";
            SqlCommand komanda = sqlUtils.radSaUpitom(konekcija, upit);
            komanda.Parameters.AddWithValue("@KudId", txtKudId.Text);
            
            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o kudu su uspešno obrisani");
                prikaziKudove();
            }
            ponistiUnosTxt();
        }
       
        private void ponistiUnosTxt()
        {
            txtKudId.Text = "";
            txtAdresa.Text = "";
            txtMesto.Text = "";
            txtSekcija.Text = "";
            txtDiploma.Text = "";
            txtNaziv.Text = "";

        }
    }
}
