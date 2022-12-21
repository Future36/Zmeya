using System;
using System.Collections.Generic;

namespace snake
{

    class Snake
    {
        public Snake(int initialX, int initialY, int bodyLenght = 0)
        {

            Head = new Pixel(initialX, initialY);

            for (int i = bodyLenght; i >= 0; i--)
            {
                Body.Enqueue(item: new Pixel(x: Head.X - i - 1, initialY));

            }

            Draw();
        }

        public Pixel Head { get; private set; }

        public Queue<Pixel> Body { get; } = new Queue<Pixel>();

        public void Move(Direction direction, bool eat = false)
        {
            Clear();
            Body.Enqueue(item: new Pixel(Head.X, Head.Y));
            if (!eat)
                Body.Dequeue();

            Head = direction switch
            {
                Direction.Up => new Pixel(Head.X, y: Head.Y - 1),
                Direction.Down => new Pixel(Head.X, y: Head.Y + 1),
                Direction.Right => new Pixel(x: Head.X + 1, Head.Y),
                Direction.Left => new Pixel(x: Head.X - 1, Head.Y),
                _ => Head
            };

            Draw();
        }
        public void Draw()
        {
            Head.Draw();

            foreach (Pixel pixel in Body)
            {
                pixel.Draw();
            }
        }

        public void Clear()
        {
            Head.Clear();
            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }
    }
}
