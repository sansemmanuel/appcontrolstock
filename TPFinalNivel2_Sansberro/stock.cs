﻿using System;
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
            
            cbCampo.Items.Add("Marca");
            cbCampo.Items.Add("Precio");
            cbCampo.Items.Add("Categoria");
            
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
           if(dgvStock.CurrentRow != null)
            {
                producto seleccionado = (producto)dgvStock.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
            
            
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
            cargar();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            producto seleccionado;
            seleccionado = (producto)dgvStock.CurrentRow.DataBoundItem;
            AgregarProducto modificar = new AgregarProducto(seleccionado);
            modificar.ShowDialog();
            cargar();
                
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            productoNegocio negocio = new productoNegocio();
            producto seleccionado;
            try
            {
               DialogResult respuesta = MessageBox.Show("Realmente desea eliminar este articulo?", "Eliminando", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (producto)dgvStock.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    cargar();
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        private void btnBuscar_Click(object sender, EventArgs e)
        {
            productoNegocio negocio = new productoNegocio();
            if (cbCampo.SelectedItem != null && cbCriterio.SelectedItem != null)
            {
                string campo = cbCampo.SelectedItem.ToString();
                string criterio = cbCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;
                if (campo == "Precio" && !EsNumeroValido(filtro))

                {
                    MessageBox.Show("El filtro de precio debe ser un número válido.");
                    return;

                }
                try
                {
                    dgvStock.DataSource = negocio.filtrar(campo, criterio, filtro);
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                
            }
            else
            {
                MessageBox.Show("Seleccione campo y criterio para realizar su busqueda!");
            }




        }
        private bool EsNumeroValido(string texto)
        {
            return decimal.TryParse(texto, out _);
        }

        private void tbBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<producto> listaFiltrada;
            string filtro = tbBuscar.Text;
            if (filtro.Length >= 3)
            {
                listaFiltrada = listaProducto.FindAll(Apple => Apple.Nombre.ToUpper().Contains(filtro.ToUpper()) || Apple.Categoria.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaProducto;
            }
            dgvStock.DataSource = null;
            dgvStock.DataSource = listaFiltrada;
            OcultarColumnas();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbCampo.SelectedItem.ToString();
            if(opcion == "Categoria" )
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Celulares");
                cbCriterio.Items.Add("Televisores");
                cbCriterio.Items.Add("Media");
                cbCriterio.Items.Add("Audio");
                txtFiltroAvanzado.Enabled = false;
            }
            else if(opcion == "Marca")
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Samsung");
                cbCriterio.Items.Add("Apple");
                cbCriterio.Items.Add("Sony");
                cbCriterio.Items.Add("Huawei");
                cbCriterio.Items.Add("Motorola");
                txtFiltroAvanzado.Enabled = false;



            }
            else
            {
                cbCriterio.Items.Clear();
                cbCriterio.Items.Add("Menor a");
                cbCriterio.Items.Add("Mayor a");
                txtFiltroAvanzado.Enabled = true;
            }
        }


        
        private void cbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            productoNegocio negocio = new productoNegocio();
            try
            {

                string Categoria = cbCriterio.SelectedItem.ToString();
                string Marca = cbCriterio.SelectedItem.ToString();
                string Precio = txtFiltroAvanzado.ToString();
                dgvStock.DataSource = negocio.filtrar(Categoria,Marca,Precio);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (dgvStock.SelectedRows.Count > 0)
            {
                producto selectedProduct = dgvStock.SelectedRows[0].DataBoundItem as producto;
                if (selectedProduct != null)
                {
                    detalleProducto detailForm = new detalleProducto(selectedProduct);
                    detailForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un producto para ver el detalle.", "No ha seleccionado ningun producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
