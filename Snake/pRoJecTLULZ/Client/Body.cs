using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace pRoJecTLULZ
{
    public class Body
    {
        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        ConsoleKey currentDirection = ConsoleKey.D;
        
        Board board;

        List<Dir> bodyDir;
        public int x { get; set; }
        public int y { get; set; }
        public int score { get; set; }  
       // public string username { get; set; }

        public Body(Board board)
        {
            x = 15;
            y = 15;
            //username;
            score = 0;

            bodyDir = new List<Dir>();
            bodyDir.Add(new Dir(x, y));
            this.board = board;
        }
       

        public void drawBody()
        {
            foreach(Dir dir in bodyDir)
            {
                Console.SetCursorPosition(dir.x, dir.y);
                Console.Write("■");
            }
        }


        public void readKey()
        {
            if(Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                
                direction(keyInfo.Key);
            }
        }

        private void direction(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.A:
                case ConsoleKey.S:
                case ConsoleKey.D:
                    currentDirection = key;
                    break;
            }
        }


        public void Move()
        {
            //direction(key);
            switch (currentDirection)
            {
                case ConsoleKey.W:
                    y--;  //mby x, y, or dir xy 
                    break;
                case ConsoleKey.A:
                    x--;
                    break;
                case ConsoleKey.S:
                    y++;
                    break;
                case ConsoleKey.D:
                    x++;
                    break;
            }
            bodyDir.Add(new Dir(x, y));
            board.AddToClear(bodyDir.First());
            bodyDir.RemoveAt(0); //draw all snakie
            Thread.Sleep(100);
            if (currentDirection == ConsoleKey.W || currentDirection == ConsoleKey.S)
            {
                Thread.Sleep(100);
            }
           
        }
        public void GrowTail(Dir Apple, Apple a)
        {
            Dir head = bodyDir[bodyDir.Count - 1];

            if(head.x == Apple.x && head.y == Apple.y)
            {
                bodyDir.Add(new Dir(x,y));
                a.NewApplePos();
                score++;
            }
        }

        public void GameOver()
        {
            Dir head = bodyDir[bodyDir.Count - 1];
            for(int i = 0; i < bodyDir.Count -2; i++)
            {
                Dir bod = (Dir)bodyDir[i];
                if (head.x == bod.x && head.y == bod.y) //collide w itself
                {
                    throw new Exception("You lost, your score is: " + score); //throw new exceot
                }

            }

            bool failX = head.x <= 1 || head.x >= board.Width - 1;
            bool failY = head.y <= 1 || head.y >= board.Height - 1;

            if(failX || failY)
            {
                throw new Exception("You lost, your score is: " + score);
            }
        }

        public void HitBoard()
        {
            Dir head = bodyDir[bodyDir.Count - 1];
            if (head.x <= 0 || head.x >= board.Width - 1 || head.y <= 0 || head.y >= board.Height - 1)
                //gameOver = true;
            //Dir head = bodyDir[bodyDir.Count - 1];
           // if (head.x >= board.Width || head.x <= 0 || head.y >= board.Height || head.y <= 0)
            {
                throw new Exception("You lost, your score is: " + score);
            }
        }



        private static Connection connection = Connection.getInstance();
        public async Task<bool> AddHighscore()
        {
            string result = await connection.AddHighscore(score); //getset?

            if(result != null)
            {
                return true;
            }
            else
            {
                Console.WriteLine("error");
                return false;
            }
        }

        public async Task<int> GetHighscores()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Highscores: ");
            string result = await connection.GetHighscores();
            dynamic stuff = JsonConvert.DeserializeObject(result);

            foreach (var item in stuff)
            {
                Console.WriteLine(" {0} {1}", item.username, item.score);
            }

            return score;
        }
        
    }

}
