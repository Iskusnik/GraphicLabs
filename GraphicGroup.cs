using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2pointsNET4_8
{
    class GraphicGroup : GraphicObject
    {
        public List<GraphicObject> objectsGroup { get; set; }

        public GraphicGroup()
        {
            objectsGroup = new List<GraphicObject>(10);
        }

        public GraphicGroup(List<GraphicObject> group)
        {
            objectsGroup = group;
        }

        public void EmptyGroup(List<GraphicObject> group)
        {
            objectsGroup = new List<GraphicObject>(10);
        }

        public override bool ChangeSelection(float x, float y)
        {
            isSelected = CheckSelection(x, y);

            return isSelected;
        }

        public override bool CheckSelection(float x, float y)
        {
            for (int i = 0; i < objectsGroup.Count; i++)
                if (objectsGroup[i].CheckSelection(x, y))
                    return true;
                else;

            return false;
        }

        public override void DrawObject(Graphics graphics, Pen pen, Brush brush)
        {
            for (int i = 0; i < objectsGroup.Count; i++)
                objectsGroup[i].DrawObject(graphics, pen, brush);
        }

        public override string GetInfo(Size size)
        {
            string info = "Группа:";
            for (int i = 0; i < objectsGroup.Count; i++)
                info += objectsGroup[i].GetInfo(size)+"\n";
            return info;
        }
    }
}
