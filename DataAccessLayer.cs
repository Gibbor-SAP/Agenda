﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormContacts
{
   
    class DataAccessLayer
    {
        static String servidor = "localhost";
        static String bd = "contacts";
        static String usuario = "root";
        static String password = "0510";
        static String port = "3306";

        private MySqlConnection conn = new MySqlConnection("server=" + servidor + ";" + "port=" + port + ";" + "user=" + usuario
            + ";" + "password=" + password + ";" + "database=" + bd + ";");
        
     public void InsertContact (Contact contact)
        {
            try
            { 
                conn.Open();
                string query = @"
                                INSERT INTO contacts(FirstName, LastName, Phone, Address)
                                VALUES (@FirstName, @LastName, @Phone, @Address)";

                MySqlParameter firstName = new MySqlParameter("@FirstName", contact.FirstName);
                MySqlParameter lastName = new MySqlParameter("@LastName", contact.LastName);
                MySqlParameter phone = new MySqlParameter("@Phone", contact.Phone);
                MySqlParameter address = new MySqlParameter("@Address", contact.Address);

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();

                MessageBox.Show("Inserción Exitosa");
            }
            catch (MySqlException e)
            {
                MessageBox.Show("No se ha podido Insertar" + e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        
        }

     public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @" UPDATE contacts
                                    SET FirstName = @FirstName,
                                        LastName = @LastName,
                                        Phone = @Phone,
                                        Address = @Address
                                    WHERE id = @id";
                MySqlParameter id = new MySqlParameter("@id", contact.id);
                MySqlParameter firstName = new MySqlParameter("@FirstName", contact.FirstName);
                MySqlParameter lastName = new MySqlParameter("@LastName", contact.LastName);
                MySqlParameter phone = new MySqlParameter("@Phone", contact.Phone);
                MySqlParameter address = new MySqlParameter("@Address", contact.Address);

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error" + e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

     public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM contacts WHERE id = @id ";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.Add(new MySqlParameter("@id", id));

                command.ExecuteNonQuery();
                MessageBox.Show("Contacto eliminado");

            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error" + e);
                throw;
            }
            finally
            {
                conn.Close();
            }
            
        }

     public List<Contact> GetContacts(string search = null)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn.Open();
                String query = @"SELECT Id, FirstName, LastName, Phone, Address
                                FROM contacts ";

                MySqlCommand command = new MySqlCommand();

                if (!string.IsNullOrEmpty(search))
                {
                    query += @"WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Phone LIKE @Search OR 
                                Address LIKE @Search";
                    command.Parameters.Add(new MySqlParameter("@Search", $"%{search}%"));
                }

                command.CommandText = query;
                command.Connection = conn;
                                
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) //El método Read solo itera una vez, no vuelve a leer los datos.
                {
                    contacts.Add(new Contact
                    { 
                        id = int.Parse(reader["id"].ToString()), //Se parcea el id
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }
                //MessageBox.Show("Todo marcha bien"); //---------------------->
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Error" + e);
                throw;
            }
            finally
            {
                conn.Close();
            }
            return contacts;
        }
        
    }

}

