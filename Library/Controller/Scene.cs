using Library.Model.Contracts;
using Library.Model.Shapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Controller
{
    public class Scene
    {
        public List<Shape> shapes;
        public bool isFileSaved;

        public Scene()
        {
            shapes = new List<Shape>();
            isFileSaved = false;
        }

        public void AddShape(Shape shape)
        {
            shapes.Add(shape);

        }

        public void Draw(IDrawingUI g)
        {
            shapes.ForEach(shape => shape.Draw(g));
        }

        public void Delete(Shape shape)
        {
            shapes.Remove(shape);
        }

        public void SaveShapesToJson(string filepath)
        {
            string json = JsonConvert.SerializeObject(shapes, Formatting.Indented,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            File.WriteAllText(filepath, json);
            isFileSaved = true;
        }

        public void LoadShapesFromJson(string filepath)
        {
            string json = File.ReadAllText(filepath);
            List<Shape> shapes = JsonConvert.DeserializeObject<List<Shape>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            if (shapes != null)
            {
                this.shapes = shapes;
            }

        }

        public void LoadNewDrawPanel()
        {
            this.shapes = new List<Shape>();
        }
    }
}
