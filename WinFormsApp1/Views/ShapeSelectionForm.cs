using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ShapeSelectionForm : Form
    {
        public string SelectedShape { get; private set; }

        public ShapeSelectionForm()
        {
             this.Text = "Select Shape";
            this.Size = new Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            ComboBox shapeComboBox = new ComboBox
            {
                Items = { "Circle", "Rectangle", "Triangle" },
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Button btnOK = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Padding = new Padding(0, 3, 0, 3)
            };

            btnOK.Click += (sender, e) =>
            {
                SelectedShape = shapeComboBox.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(SelectedShape))
                {
                    MessageBox.Show("Please select a shape!");
                    this.DialogResult = DialogResult.None;
                }
            };

            this.Controls.Add(shapeComboBox);
            this.Controls.Add(btnOK);
        }
    }
}