using System;

namespace Tiles.TileStates
{
    public abstract class TileState
    {
        public enum TileStateType
        {
            TileBlocked,
            TileEmpty,
            TileChipped
        }

        public abstract TileStateType GetViewStateType();
    }
}