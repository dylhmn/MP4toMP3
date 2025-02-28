using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        //
        // SECTION 1 - UI
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
        // SECTION 2 -
        //
    }
}
