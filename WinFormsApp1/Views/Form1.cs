
using WinFormsApp1.Controller;
using WinFormsApp1.Model.Commands;
using Library.Model.Contracts;
using Library.Model.Shapes;
using WinFormsApp1.Adapters;

namespace WinFormsApp1.Views
{
    public class Form1 : Form
    {
        CommandManager commandManager = new CommandManager();
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private Scene scene;  
        private Panel drawPanel;
        private SplitContainer splitContainer;
        private ListBox shapeListBox;
        private ContextMenuStrip shapeContextMenu;
        private Shape selectedShape;
        private Point lastMousePosition;
        private Point mouseOffset; 
        private Point originalPosition;

        public Form1()
        {
            InitializeComponent();
            scene = new Scene();  
            Setup();
        }
        private void Setup()
        {
            splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = (int)(ClientSize.Width * 0.67), 
            };

           
            drawPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
            };
            drawPanel.Paint += DrawPanel_Paint; 
            drawPanel.MouseDown += DrawPanel_MouseDown; 
            drawPanel.MouseMove += DrawPanel_MouseMove;
            drawPanel.MouseUp += DrawPanel_MouseUp;
            splitContainer.Panel1.Controls.Add(drawPanel); 

          Panel shapeListPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            Label lblTitle = new Label
            {
                Text = "Your Shapes",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 120
            };
          
            shapeListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 12),
            };

            shapeListBox.DrawMode = DrawMode.OwnerDrawFixed;
            shapeListBox.ItemHeight = 55;
            shapeListBox.DrawItem += ShapeListBox_DrawItem;

            shapeListPanel.Controls.Add(shapeListBox);
            shapeListPanel.Controls.Add(lblTitle);

            shapeContextMenu = new ContextMenuStrip();
            shapeContextMenu.Items.Add("Edit", null, EditShape_Click);
            shapeContextMenu.Items.Add("Rotate", null, RotateShape_Click);
            shapeContextMenu.Items.Add("Calculate Area", null, CalculateArea_Click);
            shapeContextMenu.Items.Add("Calculate Perimeter", null, CalculatePerimeter_Click);
            shapeContextMenu.Items.Add("Delete", null, DeleteShape_Click);

            shapeListBox.MouseDown += ShapeListBox_MouseDown;

            splitContainer.Panel2.Controls.Add(shapeListPanel);

            Controls.Add(splitContainer);

            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60, 
                BackColor = Color.Transparent
            };

            Button btnAddShape = new Button
            {
                Text = "Add Shape",
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Size = new Size(300, 70),
                Location = new Point(buttonPanel.Width-370, 0) 
            };
            btnAddShape.Click += BtnAddShape_Click;

            buttonPanel.Controls.Add(btnAddShape);
            Controls.Add(buttonPanel);
        }

        private void RotateShape_Click(object sender, EventArgs e)
        {
            if (shapeListBox.SelectedItem is Shape selectedShape)
            {
                using (var rotateForm = new RotationForm(selectedShape.Rotation))
                {
                    if (rotateForm.ShowDialog() == DialogResult.OK)
                    {
                        var command = new RotateShapeCommand(selectedShape, rotateForm.RotationAngle);
                        commandManager.ExecuteCommand(command);
                        drawPanel.Invalidate(); 
                    }
                }
            }
        }
        private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedShape != null && e.Button == MouseButtons.Left)
            {
                Library.Model.Graphics.PointF offset = new Library.Model.Graphics.PointF
                {
                    X = e.X - lastMousePosition.X,
                    Y = e.Y - lastMousePosition.Y
                };
                selectedShape.Move(offset);
                lastMousePosition = e.Location;

                drawPanel.Invalidate();
            }
        }

        private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Library.Model.Graphics.PointF p = new Library.Model.Graphics.PointF(e.X, e.Y);
            
            foreach (var shape in scene.shapes)
            {
                if (shape.Contains(p))
                {
                    selectedShape = shape;
                    mouseOffset.X = e.X - (int)selectedShape.X;
                    mouseOffset.Y = e.Y - (int)selectedShape.Y;
                    originalPosition = new Point((int)shape.X, (int)shape.Y);
                    lastMousePosition = e.Location;
                    break;
                }
            }
        }

        private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedShape != null)
            {
                
                var newPosition = new Library.Model.Graphics.PointF(selectedShape.X, selectedShape.Y);
                var oldPosition = new Library.Model.Graphics.PointF(originalPosition.X, originalPosition.Y);
                var moveCommand = new MoveShapeCommand(selectedShape, oldPosition, newPosition);
                commandManager.ExecuteCommand(moveCommand);
                selectedShape = null;
            }
        }
        private void ShapeListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string text = shapeListBox.Items[e.Index].ToString();

            Brush textBrush = Brushes.Black;
            Brush bgBrush = (e.State & DrawItemState.Selected) != 0 ? Brushes.LightBlue : Brushes.White;
            Pen borderPen = new Pen(Color.Black, 2); // Border color and thickness

            e.Graphics.FillRectangle(bgBrush, e.Bounds);
            e.Graphics.DrawString(text, new Font("Arial", 14, FontStyle.Bold), textBrush, e.Bounds.X + 5, e.Bounds.Y + 5);
            e.Graphics.DrawRectangle(borderPen, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);

            borderPen.Dispose();
            e.DrawFocusRectangle();
        }
        private void ShapeListBox_MouseDown(object sender, MouseEventArgs e)
        {
            int index = shapeListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                shapeListBox.SelectedIndex = index; 

                if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
                {
                    shapeContextMenu.Show(shapeListBox, e.Location);
                }
            }
        }
        private void EditShape_Click(object sender, EventArgs e)
        {
            if (shapeListBox.SelectedItem is Shape selectedShape)
            {
                Type type = selectedShape.GetType();

                using (ShapeCreationForm editForm = new ShapeCreationForm(type.Name))
                {
                    editForm.SetShapeData(selectedShape);

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        var editCommand = new EditCommand(selectedShape, editForm.CreatedShape);
                        commandManager.ExecuteCommand(editCommand);
                        drawPanel.Invalidate(); 
                        shapeListBox.Refresh(); 
                    }
                }
            }
        }
        private void CalculateArea_Click(object sender, EventArgs e)
        {
            if (shapeListBox.SelectedItem is Shape selectedShape)
            {
                MessageBox.Show($"Area: {selectedShape.CalculateArea():F2} ", "Shape Info");
            }
        }
        private void CalculatePerimeter_Click(object sender, EventArgs e)
        {
            if (shapeListBox.SelectedItem is Shape selectedShape)
            {
                MessageBox.Show($"Perimeter: {selectedShape.CalculatePerimeter():F2}", "Shape Info");
            }
        }

        private void DeleteShape_Click(object sender, EventArgs e)
        {
            DeleteShape();
        }

        private void DeleteShape()
        {
            if (shapeListBox.SelectedItem is Shape selectedShape)
            {
                var shapeListAdapter = new ShapeListAdapter(shapeListBox);
                var deleteCmd = new DeleteShapeCommand(scene, selectedShape,shapeListAdapter);
                commandManager.ExecuteCommand(deleteCmd);
                drawPanel.Invalidate();

            }
        }

        private void BtnAddShape_Click(object sender, EventArgs e)
        {
            AddNewShape();

        }

        private void AddNewShape()
        {
            using (ShapeSelectionForm selectionForm = new ShapeSelectionForm())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    using (ShapeCreationForm creationForm = new ShapeCreationForm(selectionForm.SelectedShape))
                    {
                        if (creationForm.ShowDialog() == DialogResult.OK && creationForm.CreatedShape != null)
                        {
                            var shapeListAdapter = new ShapeListAdapter(shapeListBox);
                            var command = new AddShapeCommand(scene, creationForm.CreatedShape, shapeListAdapter);
                            commandManager.ExecuteCommand(command);
                            drawPanel.Invalidate();
                        }
                    }
                }
            }
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            IDrawingUI drawing = new DrawingAdapter(e.Graphics);
            scene.Draw(drawing); 
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            commandManager.Undo();
            drawPanel.Invalidate();
            shapeListBox.Refresh();
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            commandManager.Redo();
            drawPanel.Invalidate();
            shapeListBox.Refresh();
        }

        private void Save_Click(object? sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON files (*.json)|*.json";
                saveDialog.Title = "Save Shapes As JSON";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    scene.SaveShapesToJson(saveDialog.FileName);
                }
            }

           
        }

        private void OpenFromJsonFile()
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "JSON files (*.json)|*.json";
                openDialog.Title = "Open Shapes from JSON";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    scene.LoadShapesFromJson(openDialog.FileName);
                    shapeListBox.DataSource = null;
                    shapeListBox.DataSource = scene.shapes;
                    drawPanel.Invalidate();
                }
            }
        }

        private void Open_Click(object? sender, EventArgs e)
        {
            if (scene.isFileSaved)
            {
                OpenFromJsonFile();
            }
            else
            {
                DialogResult result = MessageBox.Show(
              "Do you want to save the current file before proceeding?",
              "Save Changes",
              MessageBoxButtons.YesNoCancel,
              MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Save_Click(null, EventArgs.Empty);
                    OpenFromJsonFile();
                }
                else if (result == DialogResult.No)
                {
                    OpenFromJsonFile();
                }
               
            }
        }
        private void New_Click(object? sender, EventArgs e)
        {
            if (scene.isFileSaved)
            {
                Redraw();
            }
            else
            {
                DialogResult result = MessageBox.Show(
                "Do you want to save the current file before proceeding?",
                "Save Changes",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Save_Click(null, EventArgs.Empty);

                    
                }
                else if (result == DialogResult.No)
                {

                    return;
                }
                
            }
        }

        private void Redraw()
        {
            scene.LoadNewDrawPanel();
            shapeListBox.DataSource = null;
            shapeListBox.DataSource = scene.shapes;
            drawPanel.Invalidate();
        }

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();

            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, undoToolStripMenuItem, redoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1058, 40);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";

            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(71, 36);
            fileToolStripMenuItem.Text = "File";

            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(359, 44);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += New_Click;

            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(359, 44);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += Open_Click;
       
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(359, 44);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += Save_Click;


            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {  copyToolStripMenuItem, pasteToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(74, 36);
            editToolStripMenuItem.Text = "Edit";
            
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(205, 44);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += Undo_Click;
         
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(205, 44);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += Redo_Click;
       
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new Size(205, 44);
            copyToolStripMenuItem.Text = "Copy";
         
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.Size = new Size(205, 44);
            pasteToolStripMenuItem.Text = "Paste";
        
            ClientSize = new Size(1058, 647);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Click += BtnAddShape_Click;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
