using NReco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mp4to3
{
    public partial class frmConvert : Form
    {
        public frmConvert()
        {
            InitializeComponent();

            // Assigning the established mouse events to move the form
            this.lblRibbon.MouseDown += new MouseEventHandler(this.lblRibbon_MouseDown);
            this.lblRibbon.MouseMove += new MouseEventHandler(this.lblRibbon_MouseMove);
            this.lblRibbon.MouseUp += new MouseEventHandler(this.lblRibbon_MouseUp);
        }
        // VARIABLES
        bool drag;
        Point displacement;
        string mp4path, mp4name, mp3path, mp3name;
        bool mp4pathValid, mp3pathValid;
        int totalConversions = 0;

        //
        // UNIT 1 - UI
        //
        private void lblRibbon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                displacement = new Point(e.X, e.Y);
            } // MOUSE EVENT - Upon a left click, record the position and set drag = true
            else if (e.Button == MouseButtons.Right)
            {
                this.CenterToScreen();
            }// MOUSE EVENT - The form is centered on a right click
        }
        private void lblRibbon_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag) // (When the mouse is down)
            {
                this.Location = new Point(
                    this.Location.X + e.X - displacement.X,
                    this.Location.Y + e.Y - displacement.Y);
            } // The calculations for the form to move with the mouse
        }
        private void lblRibbon_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        } // Drag release
        private void picExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        } // Exit button safely closes the program


        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //
        // UNIT 2 - FUNCTIONALITY
        //
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, Filter = "MP4 File|*.mp4" };
            // OpenFileDialog prompts users to open a file (One file, with the format MP4)

            if (ofd.ShowDialog() == DialogResult.OK) // If the user didn't cancel the file selection
            {
                mp4path = ofd.FileName; // Returns the path
                mp4name = ofd.SafeFileName; // Returns only the file name
                mp4pathValid = true;
                btnSave.Visible = true; // A strict order reduces crashing
            }
            txtOpen.Text = mp4path; // Displays path
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK) // If the user doesn't cancel...
            {
                mp3path = fbd.SelectedPath; // Returns the path
                mp3name = mp4name.Substring(0, mp4name.Length - 4); // Starting from index 0, and taking away the last 4 (.mp4)
                mp3path += ("\\" + mp3name + ".mp3"); // Adds to the path with the addition of the file
                mp3pathValid = true;
            }
            txtSave.Text = mp3path; // Displays (concatenated) path
        }
        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (mp4pathValid && mp3pathValid) // Validates both fields are entered
            {
                // NuGet Package Manager >> NReco.VideoConverter (This is a library depedency)
                var convert = new NReco.VideoConverter.FFMpegConverter(); // Library reference
                convert.ConvertMedia(txtOpen.Text.Trim(), txtSave.Text.Trim(), "mp3");
                mp4pathValid = false;
                mp3pathValid = false;
                btnSave.Visible = false;
                totalConversions++;
                lblCount.Text = "Total conversions: " + totalConversions.ToString();
                txtOpen.Text = "";
                txtSave.Text = "";

            }
            else if (mp4pathValid == true && mp3pathValid == false)
            {
                lblHandler.Text = "Please select a save path";
            }
            else
            {
                lblHandler.Text = "Please select a video";
                // Await marks the method as async. So the main thread isn't disturbed.
            }
            await Task.Delay(4000);
            lblHandler.Text = "";
        }
    }
}
