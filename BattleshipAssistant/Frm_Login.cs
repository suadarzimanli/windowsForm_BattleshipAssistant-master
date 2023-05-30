using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BattleshipAssistant
{
    public partial class Frm_Login : Form
    {
        string connectionString = @"Data Source=DESKTOP-P1TME7J\SQLEXPRESS;Initial Catalog=BattleShipAssistent;Integrated Security=True";
        public int O { get; private set; }

        public Frm_Login()
        {
            InitializeComponent();
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PnlRegister.Height = PnlMain.Height;
            PnlMain.Location = new Point(5, 5);
            Pnl.Location = new Point(380, 5);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            PnlRegister.Height = O;
            PnlMain.Location = new Point(315, 5);
            Pnl.Location = new Point(5, 5);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("If you want to register please enter username, email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the event handler without proceeding
            }
            else if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("The passwords entered do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the event handler without proceeding
            }
            else
            {

                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO users (UserName, Email, Password) VALUES (@UserName, @Email, @Password)";

                    using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                    {
                        sqlCon.Open();

                        sqlCmd.Parameters.AddWithValue("@UserName", textBox2.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Email", textBox1.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Password", textBox3.Text.Trim());

                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Registration is successful.");
                        Clear();
                    }


                }
                void Clear()
                {
                    textBox2.Text = textBox1.Text = textBox3.Text = textBox4.Text = "";
                }

                GameForm gameForm = new GameForm();

                // Hide the current form (login form)
                this.Hide();

                // Show the new form
                gameForm.Show();
            }
        }

        private void BtnClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Exit the event handler without proceeding
            }
            else
            {
                string query = "SELECT * FROM users WHERE UserName = @UserName";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", txtUserName.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Iterate through the retrieved rows
                            //if i bura at
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("Username or password is incorrect.");
                                return;
                            }
                            
                            while (reader.Read())
                            {
                                // Access the columns using the appropriate data types
                                
                                string Password = reader.GetString(reader.GetOrdinal("Password"));

                                if (Password == TxtPassword.Text)
                                {
                                    GameForm gameForm = new GameForm();

                                    // Hide the current form (login form)
                                    this.Hide();

                                    // Show the new form
                                    gameForm.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Error");
                                }
                                
                               


                            }
                        }
                    }
                }
            }
        }
        
        public static class test
        {
            public static int value;
        }


    }
}
