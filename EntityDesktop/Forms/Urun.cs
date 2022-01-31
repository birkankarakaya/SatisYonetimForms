using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityDesktop
{
    public partial class Urun : Form
    {
        public Urun()
        {
            InitializeComponent();
        }

        DBEntityUrunEntities db = new DBEntityUrunEntities();

        private void btnListe_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Tbl_Urun.ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Tbl_Urun t = new Tbl_Urun();
            t.UrunAD = txtAD.Text;
            t.Marka = txtMarka.Text;
            t.Stok = short.Parse(txtStok.Text);
            t.Fiyat = Convert.ToDecimal(txtFiyat.Text);
            t.Durum = true;
            t.Kategori = Convert.ToInt32(cmbKategori.SelectedValue);
            db.Tbl_Urun.Add(t);
            db.SaveChanges();
            MessageBox.Show("Ürün Eklendi!", "Başarılı");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtID.Text);
            var urun = db.Tbl_Urun.Find(x);
            if (urun != null)
            {
                db.Tbl_Urun.Remove(urun);
                db.SaveChanges();
                MessageBox.Show("Ürün Silindi!", "Başarılı");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(txtID.Text);
            var urun = db.Tbl_Urun.Find(x);
            urun.UrunAD = txtAD.Text;
            urun.Stok = short.Parse(txtStok.Text);
            urun.Fiyat = Convert.ToDecimal(txtFiyat.Text);
            db.SaveChanges();
            MessageBox.Show("Ürün Güncellendi!", "Başarılı");
        }

        private void Urun_Load(object sender, EventArgs e)
        {
            var kategoriler = (from x in db.Tbl_Kategori 
                               select new 
                               { 
                                   x.ID,
                                   x.AD 
                               }).ToList();
            cmbKategori.ValueMember = "ID";
            cmbKategori.DisplayMember = "AD";
            cmbKategori.DataSource = kategoriler;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtAD.Text = "";
            txtMarka.Text = "";
            txtStok.Text = "";
            txtFiyat.Text = "";
            txtDurum.Text = "";
            cmbKategori.Text = "";
        }
    }
}
