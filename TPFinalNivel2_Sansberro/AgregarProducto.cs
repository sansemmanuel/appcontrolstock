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
    public partial class AgregarProducto : Form
    {

        private producto Producto = null;
        public AgregarProducto()
        {
            InitializeComponent();
        }

        public AgregarProducto(producto Producto)
        {
            InitializeComponent();
            this.Producto = Producto;

        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categorianegocio = new CategoriaNegocio();
            MarcaNegocio marcanegocio = new MarcaNegocio();

            try
            {
                cboMarca.DataSource = marcanegocio.Listar();
                cboCat.DataSource = categorianegocio.Listar();
                if(Producto != null)
                {
                    CargarTextoNoNulo(txtCod, Producto.Codigo, "Código");
                    CargarTextoNoNulo(txtNombre, Producto.Nombre, "Nombre");
                    CargarTextoNoNulo(txtDesc, Producto.Descripcion, "Descripcion");
                    CargarNumeroNoNulo(txtPrecio, Producto.Precio, "Precio");
                    CargarTextoNoNulo(txtImagenUrl, Producto.ImagenUrl, "URL de Imagen");
                    cargarImagen(txtImagenUrl.Text);
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void CargarTextoNoNulo(TextBox textBox, string valor, string campo)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                textBox.Text = valor;
            }
            else
            {
                MessageBox.Show($"El campo {campo} no se ha logrado cargar");
            }
        }

        private void CargarNumeroNoNulo(TextBox textBox, decimal? valor, string campo)
        {
            if (valor.HasValue)
            {
                textBox.Text = valor.Value.ToString();
            }
            else
            {
                MessageBox.Show($"El campo {campo} no se ha logrado cargar");
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (CamposValidos())
            {
                producto prod = new producto();
                productoNegocio negocio = new productoNegocio();
                CategoriaNegocio negocio1 = new CategoriaNegocio();
                MarcaNegocio negocio2 = new MarcaNegocio();
                try
                {
                    prod.Nombre = txtNombre.Text;
                    prod.Descripcion = txtDesc.Text;
                    prod.Precio = decimal.Parse(txtPrecio.Text);
                    prod.ImagenUrl = txtImagenUrl.Text;
                    prod.Codigo = txtCod.Text;
                    prod.IdCategoria = ((Elemento)cboCat.SelectedItem).Id;
                    prod.IdMarca = ((Elemento)cboMarca.SelectedItem).Id;

                    negocio.agregar(prod); ///
                    MessageBox.Show("Agregado exitosamente");
                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Por favor verifique que los valores ingresados sean validos");
            }
        }
        private bool CamposValidos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDesc.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                cboCat.SelectedItem == null ||
                cboMarca.SelectedItem == null)
            {
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("El precio debe ser un valor numérico válido.");
                return false;
            }

            return true;
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

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

    }
}
