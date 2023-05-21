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
    public partial class ContactDetails : Form
    {
        //VARIABLES GLOBALES IDENTIFICADAS  CON _
        private BusinessLogicLayer _businessLogicLayer;
        private Contact _contact; 

    
        public ContactDetails()
        {
            InitializeComponent();
           _businessLogicLayer = new BusinessLogicLayer();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       private void btnSave_Click(object sender, EventArgs e)
       {
            SaveContact();
            this.Close();
            ((Contacts)this.Owner).PopulateContacts(); // Se castea la función de control Parent citando la clase Contacts
       }

        private void SaveContact()
        {
            Contact contact = new Contact();
            contact.FirstName = txtFirtName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;

            contact.id = _contact != null ? _contact.id : 0; //Si contact es distinto de null, se ejecuta el método, sino false

            _businessLogicLayer.SaveContact(contact);
        }

        public void LoadContact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                ClearForm(); // Antes de llenar las cajas de texto, tienen que estar vacías

                txtFirtName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;

            }
        }

        private void ClearForm()
        {
            txtFirtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;

        }
    }
}
