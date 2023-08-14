using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;
namespace TPFinalNivel2_Sansberro
{
    public partial class stock : Form
    {
        private List<producto> listaProducto;
        public stock()
        {
            InitializeComponent();
        }

        private void stock_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            productoNegocio negocio = new productoNegocio();
            try
            {
                listaProducto = negocio.Listar();
                dgvStock.DataSource = listaProducto;
                OcultarColumnas();
                cargarImagen(imagen: listaProducto[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       
        private void dgvStock_SelectionChanged(object sender, EventArgs e)
        {
           producto seleccionado = (producto)dgvStock.CurrentRow.DataBoundItem;
           cargarImagen(seleccionado.ImagenUrl);
            
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxProd.Load(imagen);

            }
            catch (Exception ex)
            {
                pbxProd.Load("https://w7.pngwing.com/pngs/507/59/png-transparent-dolphin-error-404-blue-marine-mammal-mammal.png");
            }
        }
        private void OcultarColumnas()
        {
            dgvStock.Columns["ImagenUrl"].Visible = false;
            dgvStock.Columns["Id"].Visible = false;
            dgvStock.Columns["IdMarca"].Visible = false;
            dgvStock.Columns["IdCategoria"].Visible = false;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto alta = new AgregarProducto();
            alta.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            dgvStock.Refresh();
        }
    }
}
