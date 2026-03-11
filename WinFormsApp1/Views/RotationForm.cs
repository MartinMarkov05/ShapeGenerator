using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class RotationForm : Form
    {
        public float RotationAngle { get; private set; }

        public RotationForm(float currentAngle)
        {
            this.Text = "Rotate Shape";
            this.Size = new Size(500, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            Label lbl = new Label
            {
                Text = "Rotation angle (degrees):",
                Location = new Point(10, 20),
                AutoSize = true
            };
            Controls.Add(lbl);

            txtAngle = new TextBox
            {
                Location = new Point(10, 50),
                Width = 100
            };
            Controls.Add(txtAngle);

            btnOK = new Button
            {
                Text = "Apply",
                Location = new Point(120, 47),
                DialogResult = DialogResult.OK,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Padding = new Padding(0, 3, 0, 3)
            };
            btnOK.Click += BtnOK_Click;
            Controls.Add(btnOK);

            txtAngle.Text = currentAngle.ToString();
        }

        private TextBox txtAngle;
        private Button btnOK;

        

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtAngle.Text, out float angle))
            {
                RotationAngle = angle;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }
        }
    }
}

