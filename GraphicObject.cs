﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2pointsNET4_8
{
    abstract class GraphicObject
    {
        public bool isSelected { get; set; }

        abstract public bool CheckSelection(float x, float y);
        abstract public void DrawObject(Graphics graphics, Pen pen, Brush brush);
        
    }
}