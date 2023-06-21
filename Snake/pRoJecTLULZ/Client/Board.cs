using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pRoJecTLULZ
{
    public class Board
    {
        public int Width = 50;
        public int Height = 25;

        List<Dir> ToClear = new List<Dir>();

        public Board()
        {
        }

        public void Init()
        {
            Console.CursorVisible = false;
        }

        public void SetBoard()
        {
            Console.Clear();   

            for (int i = 1; i < Width-1; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("═╗");
            }


            for (int i = 1; i < Width-1; i++)
            {
                Console.SetCursorPosition(i, Height-1);
                Console.Write("═╝");
            }


            for (int i = 2; i < Height-1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("║");
            }
            for (int i = 2; i < Height-1; i++)
            {
                Console.SetCursorPosition(Width-1, i);
                Console.Write("║");
            }

            Console.SetCursorPosition(1, 1);
            Console.Write("╔");

            Console.SetCursorPosition(1, Height-1);
            Console.Write("╚");

            /* for (int i = 1; i < Width;)
             {
                 Console.SetCursorPosition(i, 1);
                 Console.Write("╔");
             }*/

        }

        public void AddToClear(Dir toAdd)
        {
            ToClear.Add(toAdd);
        }

        public void UpdateBoard()
        {
            foreach (Dir dir in ToClear)
            {
                Console.SetCursorPosition(dir.x, dir.y);
                Console.Write(" ");
            }
        }
        
      
    
    
    
    }
}
