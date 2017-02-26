﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Farmerchess.Gui
{
    class Cell
    {
        public Cell(int posx, int posy, int value, int id)
        {
            PosX = posx;
            PosY = posy;
            Value = value;
            Id = id;
            //var blocksX = (int)Tools.ReadSetting(Tools.SettingsKey_BlockCountX, true);
            //GridX = Id % blocksX;
            //GridY = Id / blocksX;
        }

        public int Value { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Id { get; set; }
        public int GridX { get; private set; }
        public int GridY { get; private set; }

        public RectangleGeometry RectGeo { get; set; }
        public Rect Rectangle { get; set; }
    }
}