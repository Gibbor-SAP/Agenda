using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormContacts
{
    public partial class Contacts : Form
    {
        private BusinessLogicLayer _businessLogicLayer;

        public Contacts()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        #region EVENTS
        private void button1_Click(object sender, EventArgs e)
        {
            OpenContactDetailDialog();
        }


        #endregion

        #region PRIVATE METHODS

        private void OpenContactDetailDialog()
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails.ShowDialog(this);
        }

        #endregion

        private void Contacts_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }

        public void PopulateContacts(string searchText = null) //Al agregar el valor null al parámetro de una función, el parámetro es opcional 
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText); // Se trae la lista al grid
            gridContact.DataSource = contacts; // Se Muestran los Datos  en el grid

        }

        private void gridContact_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContact.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadContact(new Contact
                {
                    id = int.Parse(gridContact.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContact.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContact.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContact.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContact.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                contactDetails.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContact.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateContacts();
            }
            
        }
        private void DeleteContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }
    }
}
