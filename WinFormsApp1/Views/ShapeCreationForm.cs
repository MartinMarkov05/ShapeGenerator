using Library.Model.Graphics;
using Library.Model.Shapes;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Rectangle = Library.Model.Shapes.Rectangle;

namespace WinFormsApp1
{
    public partial class ShapeCreationForm : Form
    {
        public  Shape CreatedShape { get; private set; }
        private bool isFilled = false;
        private TextBox nameInput;
        private NumericUpDown radiusInput;
        private NumericUpDown widthInput;
        private NumericUpDown heightInput;
        private NumericUpDown baseInput;
        private CheckBox fillInput;
        FlowLayoutPanel nameRow;
        FlowLayoutPanel colorRow;
        private DrawingColor selectedColor = new DrawingColor(0,0, 0, 0);
        private Panel colorPreview;

        public ShapeCreationForm(string shapeType)
        {
            
            this.Text = $"Create {shapeType}";
            this.Size = new Size(600, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                Location = new Point(20, 20),
                WrapContents = false
            };

            Label lblName = new Label { Text = "Name: ", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular) };
            nameInput = new TextBox();

            nameRow = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
            nameRow.Controls.AddRange(new Control[] {lblName , nameInput});


            Button btnChooseColor = new Button
            {
                Text = "Choose Color",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            this.Controls.Add(btnChooseColor);
            btnChooseColor.Click += BtnChooseColor_Click;

             colorPreview = new Panel
            {
                Size = new Size(40, 40),
                Location = new Point(150, 15),
                BackColor = ToSystemColor(selectedColor)
             };
            this.Controls.Add(colorPreview);

            fillInput = new CheckBox { Text = "Fill",  AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular) };
           
            colorRow = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
            colorRow.Controls.AddRange(new Control[] {btnChooseColor, colorPreview, fillInput});
           

            if (shapeType == "Circle")
            {
                Label lblRadius = new Label { Text = "Radius:",AutoSize = true,Font = new Font("Segoe UI", 9, FontStyle.Regular), };
                 radiusInput = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 100 };

                FlowLayoutPanel radiusRow = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
                radiusRow.Controls.AddRange(new Control[] { lblRadius,radiusInput});
               
                panel.Controls.AddRange(new Control[] { nameRow ,radiusRow,colorRow});
                
            }
            else if (shapeType == "Rectangle")
            {
                
                Label lblWidth = new Label {Text = "Width:", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular), Padding = new Padding(0, 3, 0, 3) };
                 widthInput = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 200 };

                FlowLayoutPanel widthRow = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
                widthRow.Controls.AddRange(new Control[] { lblWidth, widthInput });

                Label lblHeight = new Label {Text = "Height:", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular), Padding = new Padding(0, 3, 0, 3) };
                 heightInput = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 100 };

                FlowLayoutPanel heightRow = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
                heightRow.Controls.AddRange(new Control[] { lblHeight, heightInput });

                panel.Controls.AddRange(new Control[] {nameRow, widthRow, heightRow,colorRow }); 
            }
            else if(shapeType == "Triangle")
            {

                Label lblHeight = new Label { Text = "Height:" ,AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular), Padding = new Padding(0, 3, 0, 3) };
                heightInput = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 100 };

                FlowLayoutPanel heightRow = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
                heightRow.Controls.AddRange(new Control[] { lblHeight, heightInput });

                Label lblBase = new Label { Text = "Base:", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Regular), Padding = new Padding(0, 3, 0, 3) };
                baseInput = new NumericUpDown { Minimum = 1, Maximum = 1000, Value = 100 };

                FlowLayoutPanel baseRow = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
                baseRow.Controls.AddRange(new Control[] { lblBase, baseInput });

                panel.Controls.AddRange(new Control[] { nameRow, heightRow, baseRow, colorRow });
            }

                Button btnCreate = new Button
                {
                    Text = "Create",
                    DialogResult = DialogResult.OK,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Padding = new Padding(0, 3, 0, 3)
                };

            btnCreate.Click += (sender, e) =>
            {
                isFilled = fillInput.Checked;
                switch (shapeType)
                {
                    case "Circle":

                        string circleName = nameRow.Controls[1].Text;
                        if (string.IsNullOrWhiteSpace(circleName))
                        {
                            MessageBox.Show("Please enter a name for the shape!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.None; 
                            return;
                        }
                        int radius = (int)(int)((NumericUpDown)panel.Controls[1].Controls[1]).Value;
                        if (radius <= 0)
                        {
                            MessageBox.Show("Please enter a positive number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.None;
                            return;
                        }
                        CreatedShape = new Circle(radius, selectedColor , circleName,isFilled) ;
                        break;

                    case "Rectangle":
                        {    
                            string recName = nameRow.Controls[1].Text;
                        if (string.IsNullOrWhiteSpace(recName))
                        {
                            MessageBox.Show("Please enter a name for the shape!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.None; // Prevent form from closing
                            return;
                        }
                        var width = (int)(int)((NumericUpDown)panel.Controls[1].Controls[1]).Value;
                            if (width <= 0)
                            {
                                MessageBox.Show("Please enter a positive number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DialogResult = DialogResult.None;
                                return;
                            }
                            var height = (int)(int)((NumericUpDown)panel.Controls[2].Controls[1]).Value;
                            if (height <= 0)
                            {
                                MessageBox.Show("Please enter a positive number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DialogResult = DialogResult.None;
                                return;
                            }
                            CreatedShape = new Rectangle(width, height, selectedColor, recName, isFilled);
                     
                        break; 
                        }
                    
                    case "Triangle":
                        {
                            string triangleName = nameRow.Controls[1].Text;
                            if (string.IsNullOrWhiteSpace(triangleName))
                            {
                            MessageBox.Show("Please enter a name for the shape!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.DialogResult = DialogResult.None; // Prevent form from closing
                            return;
                            }
                            var height = (int)(int)((NumericUpDown)panel.Controls[1].Controls[1]).Value;
                            if (height <= 0)
                            {
                                MessageBox.Show("Please enter a positive number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DialogResult = DialogResult.None;
                                return;
                            }
                            var base_ = (int)(int)((NumericUpDown)panel.Controls[2].Controls[1]).Value;
                            if (base_ <= 0)
                            {
                                MessageBox.Show("Please enter a positive number!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.DialogResult = DialogResult.None;
                                return;
                            }
                            CreatedShape = new Triangle(base_, height, selectedColor, triangleName, isFilled);
                       
                        break;
                        }
                }
            };
            panel.Controls.Add(btnCreate);
            this.Controls.Add(panel);
        }

        private Color ToSystemColor(DrawingColor drawingColor)
        {
            return Color.FromArgb(drawingColor.R, drawingColor.G, drawingColor.B);
        }


        private void BtnChooseColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.FullOpen = true; 
                colorDialog.Color = ToSystemColor(selectedColor);

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedColor = new DrawingColor(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                    colorPreview.BackColor = colorDialog.Color;
                }
            }
        }

        public void SetShapeData(Shape shape)
        {
                if (shape is Circle circle)
            {
                nameInput.Text = circle.Name;
                radiusInput.Value = (decimal)circle.Radius;
                fillInput.Checked = circle.IsFilled;
                selectedColor = circle.Color;
                colorPreview.BackColor = ToSystemColor(selectedColor);

            }
            else if (shape is Rectangle rect)
            {
                nameInput.Text = rect.Name;
                widthInput.Value = rect.Width;
                heightInput.Value = rect.Height;
                fillInput.Checked = rect.IsFilled;
                selectedColor= rect.Color;
                colorPreview.BackColor = ToSystemColor(selectedColor);

            }
            else if(shape is Triangle triangle)
            {
                nameInput.Text = triangle.Name;
                heightInput.Value =triangle.Height;
                baseInput.Value = triangle.Base;
                fillInput.Checked = triangle.IsFilled;
                selectedColor = triangle.Color;
                colorPreview.BackColor = ToSystemColor(selectedColor);
            }
        }
    }
}