using System;
using System.Linq;
using System.Diagnostics;

namespace snake
{

    class Program
    {
        static readonly int fieldWidth = 30;
        static readonly int fieldHeight = 30;

        private const int Frames = 100;

        static void Main()
        {

            Console.CursorVisible = false;
            while (true)
            {
                GameStart();
                Console.ReadKey();
            }
        }

        static void GameStart()
        {
            Console.Clear();
            int record = 0;
            DrawBorder();

            Direction movement = Direction.Up;

            var snake = new Snake(initialX: fieldWidth / 2, initialY: fieldHeight / 2);
            Stopwatch stopwatch = new();
            int score = 0;

            Pixel food = GetFood(snake);
            food.Draw();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                stopwatch.Restart();

                Direction prevMovement = movement;
                while (stopwatch.ElapsedMilliseconds <= Frames)
                {
                    if (movement == prevMovement)
                    {
                        movement = Movement(movement);
                    }

                }
                if (snake.Head.X == food.X && snake.Head.Y == food.Y)
                {
                    snake.Move(movement, eat: true);
                    food = GetFood(snake);
                    food.Draw();
                    score++;
                    if (score > record)
                    {
                        record = score;
                    }
                }
                else
                {
                    snake.Move(movement);
                }



                if (snake.Head.X == fieldWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == fieldHeight - 1
                    || snake.Head.Y == 0
                    || snake.Body.Any(i => i.X == snake.Head.X && i.Y == snake.Head.Y))

                    break;
            }

            snake.Clear();
            Console.SetCursorPosition(left: 10, top: 10);
            Console.WriteLine("Game Over");
            Console.SetCursorPosition(left: 10, top: 13);
            Console.WriteLine($"Record: {record}");
            Console.SetCursorPosition(left: 10, top: 12);
            Console.WriteLine($"Score: {score}");
            Console.SetCursorPosition(left: 10, top: 14);
            Console.WriteLine("Для перезапуска нажмите любую кнопку");

        }

        static Direction Movement(Direction currrentDirection)
        {
            if (!Console.KeyAvailable) return currrentDirection;

            ConsoleKey key = Console.ReadKey(intercept: true).Key;

            currrentDirection = key switch
            {
                ConsoleKey.UpArrow when currrentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currrentDirection != Direction.Up => Direction.Down,
                ConsoleKey.RightArrow when currrentDirection != Direction.Left => Direction.Right,
                ConsoleKey.LeftArrow when currrentDirection != Direction.Right => Direction.Left,
                _ => currrentDirection
            };
            return currrentDirection;
        }
        static Pixel GetFood(Snake snake)
        {
            Pixel food;
            Console.ResetColor();
            do
            {
                
                food = new Pixel(new Random().Next(1, fieldWidth - 2), new Random().Next(1, fieldHeight - 2));
            }
            while (snake.Head.X == food.X && snake.Head.Y == food.Y || snake.Body.Any(i => i.X == snake.Head.X && i.Y == snake.Head.Y));

            return food;
        }
        static void DrawBorder()
        {
            for (int i = 0; i < fieldWidth; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                new Pixel(x: i, y: 0).Draw();
                new Pixel(x: i, y: fieldHeight - 1).Draw();
            }
            for (int i = 0; i < fieldHeight; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                new Pixel(x: 0, y: i).Draw();
                new Pixel(x: fieldWidth - 1, y: i).Draw();
            }
        }
    }

}
