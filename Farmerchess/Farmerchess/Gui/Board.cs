﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Farmerchess.Gui
{
    class Board
    {
        private GameCanvas _canvas;
        private static int _blockCountX;
        private static int _blockCountY;
        private static int _blockSize;
        private static int _lineThickness;
        private Cell[,] _grid;
        private SolidColorBrush _bgColour;
        private SolidColorBrush _gridColour;
        private SolidColorBrush _oColour;
        private SolidColorBrush _xColour;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public GameCanvas Canvas
        {
            get { return _canvas; }
            set { _canvas = value; }
        }

        public Board(int blockCountX, int blockCountY, int blockSize)
        {
            _blockCountX = blockCountX;
            _blockCountY = blockCountY;
            _blockSize = blockSize;
            InitGui();
            InitGrid();
        }

        private void InitGui()
        {
            Width = _blockCountX * _blockSize;
            Height = _blockCountY * _blockSize;
            _bgColour = Tools.GetBrush(Tools.SettingsKey_BgColour);
            _gridColour = Tools.GetBrush(Tools.SettingsKey_GridColour);
            _oColour = Tools.GetBrush(Tools.SettingsKey_OColour);
            _xColour = Tools.GetBrush(Tools.SettingsKey_XColour);
            Canvas = new GameCanvas(_blockCountX, _blockCountY);
            Canvas.Width = Width;
            Canvas.Height = Height;
            Canvas.Background = _bgColour;
            int thickness = (int)Tools.ReadSetting(Tools.SettingsKey_LineThickness, true);
            _lineThickness = thickness < 0 ? 1 : thickness;
        }

        private void InitGrid()
        {
            _grid = new Cell[_blockCountX, _blockCountY];
            int id = 0;
            for (var y = 0; y < _blockCountY; y++)
            {
                for (var x = 0; x < _blockCountX; x++)
                {
                    id = y * _blockCountX + x;
                    _grid[x, y] = new Cell(x * _blockSize, y * _blockSize, 0, id);
                    _grid[x, y].Rectangle = new Rect(_grid[x, y].PosX, _grid[x, y].PosY, _blockSize, _blockSize);
                    _grid[x, y].RectGeo = new RectangleGeometry();
                    _grid[x, y].RectGeo.Rect = _grid[x, y].Rectangle;
                }
            }
        }

        /// <summary>
        /// Returns the size for main window dimensions.
        /// </summary>
        /// <returns></returns>
        public Size GetWindowSize()
        {
            return new Size(Width + _blockSize, Height + 2 * _blockSize);
        }

        public void Draw()
        {
            int x, y;

            for (y = 0; y < _blockCountY; y++)
            {
                for (x = 0; x < _blockCountX; x++)
                {
                    DrawCell(_grid[x, y]);
                }
            }
        }

        public void DrawCell(Cell cell)
        {
            var value = cell != null ? cell.Value : 0;
            var path = (Path)Canvas.Children[cell.Id]; //cell.GridY * _blockCountX + cell.GridX
            if (cell != null)
            {
                path.Data = cell.RectGeo;
                path.Stroke = _gridColour;
                path.StrokeThickness = _lineThickness;
                path.Fill = value > 0 ? _oColour : Brushes.Transparent;
            }
        }

        public Cell GetCell(int posx, int posy, int value = -1)
        {
            for (var y = 0; y < _blockCountY; y++)
            {
                for (var x = 0; x < _blockCountX; x++)
                {
                    if (posx > _grid[x, y].PosX && posx < _grid[x, y].PosX + _blockSize &&
                        posy > _grid[x, y].PosY && posy < _grid[x, y].PosY + _blockSize)
                    {
                        if (value > -1)
                        {
                            _grid[x, y].Value = Math.Abs(value - _grid[x, y].Value);
                        }
                        return _grid[x, y];
                    }
                }
            }
            return null;
        }
    }
}