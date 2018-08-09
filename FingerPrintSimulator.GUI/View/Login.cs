using FingerPrintSimulator.Components.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintSimulator.GUI.View
{
    public partial class Login : Form, ILoginView, IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        private int keyPressed = -1;
        private bool AddUserMode = false;

        public UserEntry FoundUser { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        public event Action<byte[]> SearchFingerPrint;
        public event Action<byte[], string> AddUser;

        private void ValidateUserProc()
        {
            var appPath = Directory.GetCurrentDirectory();
            var imageFile = Path.Combine(appPath, "Pictures", $"pic{keyPressed}.png");
            this.pictureBox1.Image = Image.FromFile(imageFile);
            var name = string.Empty;
            var fingerPrintBytes = Encoding.ASCII.GetBytes($"{keyPressed}");
            SearchFingerPrint(fingerPrintBytes);

            if (FoundUser != null)
            {
                var privateForm = new PrivateWindow();
                privateForm.Message.Text = $"Bienvenido {FoundUser.Name}!";
                privateForm.ShowDialog();
                this.pictureBox1.Image = null;
            }
            else
            {
                MessageBox.Show("Usuario no encontrado. Por favor registre esta huella.", "Simulador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.pictureBox1.Image = null;
            }

        }

        private void AddUserProc()
        {
            var appPath = Directory.GetCurrentDirectory();
            var imageFile = Path.Combine(appPath, "Pictures", $"pic{keyPressed}.png");
            this.pictureBox1.Image = Image.FromFile(imageFile);
            var name = string.Empty;
            using (Prompt prompt = new Prompt("", "Ingrese su nombre"))
            {
                name = prompt.Result;
            }
            var fingerPrintBytes = Encoding.ASCII.GetBytes($"{keyPressed}");

            AddUser(fingerPrintBytes, name);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            AddUserMode = true;
            this.label1.Text = "Coloque su huella (elija un numero del 0 al 9)...";
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == ((int)WM_KEYDOWN))
            {
                switch ((int)m.WParam)
                {
                    case (int)Keys.D0:
                    case (int)Keys.NumPad0:
                        keyPressed = 0;
                        break;
                    case (int)Keys.D1:
                    case (int)Keys.NumPad1:
                        keyPressed = 1;
                        break;
                    case (int)Keys.D2:
                    case (int)Keys.NumPad2:
                        keyPressed = 2;
                        break;
                    case (int)Keys.D3:
                    case (int)Keys.NumPad3:
                        keyPressed = 3;
                        break;
                    case (int)Keys.D4:
                    case (int)Keys.NumPad4:
                        keyPressed = 4;
                        break;
                    case (int)Keys.D5:
                    case (int)Keys.NumPad5:
                        keyPressed = 5;
                        break;
                    case (int)Keys.D6:
                    case (int)Keys.NumPad6:
                        keyPressed = 6;
                        break;
                    case (int)Keys.D7:
                    case (int)Keys.NumPad7:
                        keyPressed = 7;
                        break;
                    case (int)Keys.D8:
                    case (int)Keys.NumPad8:
                        keyPressed = 8;
                        break;
                    case (int)Keys.D9:
                    case (int)Keys.NumPad9:
                        keyPressed = 9;
                        break;
                    default:
                        return false;
                }

                if (AddUserMode)
                {
                    this.AddUserProc();
                    AddUserMode = false;
                    this.label1.Text = string.Empty;
                    this.pictureBox1.Image = null;
                }
                else
                {
                    this.ValidateUserProc();
                }
                return true;
            }

            return false;
        }
    }
}
