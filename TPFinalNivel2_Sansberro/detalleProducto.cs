using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;
namespace TPFinalNivel2_Sansberro
{
    public partial class detalleProducto : Form
    {
        private producto selectedProduct;



        public detalleProducto(producto product)
        {
            InitializeComponent();
            selectedProduct = product;
        }
        private void detalleProducto_Load(object sender, EventArgs e)
        {
            if (selectedProduct != null)
            {
                lblNombre.Text = selectedProduct.Nombre;
                lblDescripcion.Text = selectedProduct.Descripcion;
                lblPrecio.Text = selectedProduct.Precio.ToString("C");
                lblMarca.Text = selectedProduct.Marca;
                lblCategoria.Text = selectedProduct.Categoria;
                cargarImagen(selectedProduct.ImagenUrl);
            }
        }
        public void cargarImagen(string imagen)
        {
            if (!string.IsNullOrEmpty(selectedProduct.ImagenUrl))
            {
                pbxDetalle.ImageLocation = selectedProduct.ImagenUrl;
            }
            else
            {
                pbxDetalle.Load("https://w7.pngwing.com/pngs/507/59/png-transparent-dolphin-error-404-blue-marine-mammal-mammal.png");
            }
        }
    }
}
