using Chips;

namespace Tiles.TileStates
{
    public class TileChipedState : TileState
    {
        public Chip Chip;
        public override TileStateType GetViewStateType() => TileStateType.TileChipped;

        public TileChipedState(Chip chip)
        {
            Chip = chip;
        }
    }
}