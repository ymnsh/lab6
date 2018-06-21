﻿using System;
using System.Linq;
using ConsoleUI;

namespace GraphicsEditor
{
    public class RedoCommand : ICommand
    {
        public string Name => "redo";
        public string Help => "Возвращает отменённое действие";
        public string Description => "Введите 'redo', чтобы вернуть отменённое действие";
        public string[] Synonyms => new string[] {};

        private Picture picture;

        public RedoCommand(Picture picture)
        {
            this.picture = picture;
        }
        
        public void Execute(params string[] parameters)
        {
            var shapes = CommandHistoryContainer.GetInstance().OnRedo();

            if (shapes == null)
            {
                Console.WriteLine("Нет действией, которые можно было бы вернуть");
                return;
            }

            var currentShapes = picture.shapes;
            
            foreach (var shape in currentShapes.ToList())
            {
                picture.Remove(shape);
            }

            foreach (var shape in shapes)
            {
                picture.Add(shape);
            }
        }
    }
}