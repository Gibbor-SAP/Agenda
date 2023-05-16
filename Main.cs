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
            contactDetails.ShowDialog();
        }

        #endregion

        private void Contacts_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }

        private void PopulateContacts()
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(); // Se trae la lista al grid
            GridContact.DataSource = contacts; // Se Muestran los Datos  en el grid

        }
    }
}
