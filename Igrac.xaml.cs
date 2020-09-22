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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Igrac : Window
    {
        SQL sqlUtils;
        public Igrac()
        {
            InitializeComponent();
            sqlUtils = new SQL(ConfigurationManager.ConnectionStrings["connKudovi"].ConnectionString);
            prikaziKudove();
            prikaziIgrace();
        } 

        private void prikaziKudove()
        {
            SqlConnection konekcija = sqlUtils.kreirajKonekciju();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [KUD]", konekcija);
            DataSet ds = new DataSet();
            da.Fill(ds);

            IgracComboBox.ItemsSource = ds.Tables[0].DefaultView;
            IgracComboBox.DisplayMemberPath = "Naziv";
        }

        private void prikaziIgrace()
        {
            SqlConnection konekcija = sqlUtils.kreirajKonekciju();
            String upit = "SELECT * FROM [Igrac]";
            DataTable dataTabela = sqlUtils.vratiIzBaze(konekcija, upit);
            DataGridIgrac.ItemsSource = dataTabela.DefaultView;
        }

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection konekcija = sqlUtils.kreirajKonekciju();
            String upit = "INSERT INTO [Igrac] (Ime, Prezime, GodRodj, Grupa, Kud) VALUES (@Ime, @Prezime, @GodRodj, @Grupa, @Kud)";
            SqlCommand komanda = sqlUtils.radSaUpitom(konekcija, upit);
            komanda.Parameters.AddWithValue("@Ime", txtIme.Text);
            komanda.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
            komanda.Parameters.AddWithValue("@GodRodj", txtGodRodj.Text);
            komanda.Parameters.AddWithValue("@Grupa", txtGrupa.Text);
            komanda.Parameters.AddWithValue("@Kud", IgracComboBox.Text);

            if (txtIme.Text != "" && txtPrezime.Text != "" && txtGodRodj.Text != "" && txtGrupa.Text != "" && IgracComboBox.Text != "")
            {
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o igracu su uspešno upisani");
                    prikaziKudove();
                    prikaziIgrace();
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
            String upit = "UPDATE [Igrac] SET Ime = @Ime, Prezime = @Prezime, GodRodj = @GodRodj, Grupa = @Grupa, Kud = @Kud WHERE ID = @IgracId";
            SqlCommand komanda = sqlUtils.radSaUpitom(konekcija, upit);
            komanda.Parameters.AddWithValue("@IgracId", txtIgracId.Text);
            komanda.Parameters.AddWithValue("@Ime", txtIme.Text);
            komanda.Parameters.AddWithValue("@Prezime", txtPrezime.Text);
            komanda.Parameters.AddWithValue("@GodRodj", txtGodRodj.Text);
            komanda.Parameters.AddWithValue("@Grupa", txtGrupa.Text);
            komanda.Parameters.AddWithValue("@Kud", IgracComboBox.Text);

            if (txtIme.Text != "" && txtPrezime.Text != "" && txtGodRodj.Text != "" && txtGrupa.Text != "" && IgracComboBox.Text != "")
            {
                int provera = komanda.ExecuteNonQuery();
                if (provera == 1)
                {
                    MessageBox.Show("Podaci o igracu su uspešno izmenjeni");
                    prikaziKudove();
                    prikaziIgrace();
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
            String upit = "DELETE FROM [Igrac] WHERE ID = @IgracId";
            SqlCommand komanda = sqlUtils.radSaUpitom(konekcija, upit);
            komanda.Parameters.AddWithValue("@IgracId", txtIgracId.Text);

            int provera = komanda.ExecuteNonQuery();
            if (provera == 1)
            {
                MessageBox.Show("Podaci o igracu su uspešno obrisani");
                prikaziKudove();
                prikaziIgrace();
            }
            ponistiUnosTxt();
        }

        private void DataGridIgrac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                txtIgracId.Text = dr["ID"].ToString();
                txtIme.Text = dr["Ime"].ToString();
                txtPrezime.Text = dr["Prezime"].ToString();
                txtGodRodj.Text = dr["GodRodj"].ToString();
                txtGrupa.Text = dr["Grupa"].ToString();
                IgracComboBox.Text = dr["Kud"].ToString();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ponistiUnosTxt()
        {
            txtIgracId.Text = "";
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtGodRodj.Text = "";
            txtGrupa.Text = "";
            

        }
    }
}
