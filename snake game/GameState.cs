using Snake_game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace snake_game
{
    public class GameState
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        private readonly Random random = new Random();

        public GameState(int rows , int cols) 
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows , cols];
            Dir = Direction.Right;

            AddSnake();
        }
        private void AddSnake()
        {
            int r = Rows / 2;

            for (int c = 1; c <= 3; c++) 
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> Emptypositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r,c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }
        private void AddFood()
        {
            List<Position> empty = new List<Position>(Emptypositions());

            if (empty.Count == 0)
            {
                return;
            }
            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row , pos.Col]= GridValue.Food;
        }
        public Position Headpositions()
        {
            return snakePositions.First.Value;
        }
        public Position Tailposition()
        {
            return snakePositions.Last.Value;
        }

        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }
        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePositions.First.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }
        public void ChangeDirection(Direction dir)
        {
            Dir = dir;

        }
        private bool OutsideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }
        private GridValue Willhit(Position newHeadpos)
        {
            if (OutsideGrid(newHeadpos))
            {
                return GridValue.Outside;
            }

            if (newHeadpos == Tailposition())
            {
                return GridValue.Empty;
            }
            return Grid [newHeadpos.Row, newHeadpos.Col];
        }

        public void move()
        {
            Position newheadPos = Headpositions().Translate(Dir);
            GridValue hit = Willhit(newheadPos);

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GameOver = true;
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newheadPos);
            }
            else if (hit == GridValue.Food)
            {
                AddHead(newheadPos);
                Score++;
                AddFood();
            }

        }
        
    }
}
