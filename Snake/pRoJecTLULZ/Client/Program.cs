using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pRoJecTLULZ
{
    class Program
    {
        private static Connection connection = Connection.getInstance();

        static public async Task Main(String[] args)
        {
            /*if (true)
            {
                string result1 = await connection.Register("test", "world");
                Console.WriteLine(result1);

                string result = await connection.Login("test", "world");
                Console.WriteLine(result);

                string result3 = await connection.AddHighscore(25);
                Console.WriteLine(result3);

                string result2 = await connection.GetHighscores();
                Console.WriteLine(result2);

            }*/

            // Console.SetWindowSize(120, 30);

            bool gameOver = false;
            bool init = false;
            Login login = new Login();
            Board board = new Board();
            board.Init();
            Body body = new Body(board);
            Apple apple = new Apple();


            while (!gameOver)
            {
                if (!init)
                {
                    if (!await login.thing())
                    {
                        return;
                    }
                    Console.WriteLine("test");
                    board.SetBoard();
                    init = true;
                }
                try
                {
                    Console.SetCursorPosition(60, 5);
                    Console.WriteLine("Score: {0}", body.score);
                    board.UpdateBoard();
                    body.readKey();
                    apple.drawApple();
                    body.drawBody();
                    body.Move();
                    body.GrowTail(apple.ApplePos(), apple);
                    body.GameOver();

                }
                catch(Exception e)
                {
                    Console.SetCursorPosition(60, 5);
                    Console.WriteLine(e.Message);
                    Thread.Sleep(5000);
                    Console.SetCursorPosition(60, 8);
                    await body.AddHighscore();
                    await body.GetHighscores();
                    gameOver = true;
                   
                }
            }
        }


    }
}
