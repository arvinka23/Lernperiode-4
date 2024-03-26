using System;
using System.Collections.Generic;

namespace snake_game
{
    public class Direction
    {
        public readonly static Direction Left = new Direction(0, -1);
        public readonly static Direction Right = new Direction(0, 1);
        public readonly static Direction Up = new Direction(-1, 0);
        public readonly static Direction Down = new Direction(1, 0);
        public int Rowoffset { get; }
        public int Coloffset { get; }

        private Direction(int rowoffset, int coloffset) 
        {
            Rowoffset = rowoffset;
            Coloffset = coloffset;
        }

        public Direction Opposite()
        {
            return new Direction(-Rowoffset, -Coloffset);
        }

        public override bool Equals(object obj)
        {
            return obj is Direction direction &&
                   Rowoffset == direction.Rowoffset &&
                   Coloffset == direction.Coloffset;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rowoffset, Coloffset);
        }

        public static bool operator ==(Direction left, Direction right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        public static bool operator !=(Direction left, Direction right)
        {
            return !(left == right);
        }
    }
}
