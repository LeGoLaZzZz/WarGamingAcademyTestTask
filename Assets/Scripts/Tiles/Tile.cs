using System;
using System.Diagnostics;
using Chips;
using Tiles.TileStates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace Tiles
{
    [Serializable]
    public class TileStateChanged : UnityEvent<TileStateChangedArgs>
    {
    }

    [Serializable]
    public class TileStateChangedArgs
    {
        public Tile tile;
        public TileState newState;

        public TileStateChangedArgs(Tile tile, TileState newState)
        {
            this.tile = tile;
            this.newState = newState;
        }
    }


    [Serializable]
    public class TileChosenStateChanged : UnityEvent<Tile, bool>
    {
    }


    public class Tile : MonoBehaviour
    {
        public TileState currentState;

        [SerializeField] private TileState.TileStateType startState;
        [SerializeField] private int x;
        [SerializeField] private int y;


        private bool _isChosen = false;

        public TileStateChanged tileStateChangedEvent = new TileStateChanged();
        public TileChosenStateChanged tileChosenStateChanged = new TileChosenStateChanged();

        public int X
        {
            get => x;
            private set => x = value;
        }


        public int Y
        {
            get => y;
            private set => y = value;
        }


        public TileState.TileStateType StartState
        {
            get => startState;
            private set => startState = value;
        }

        public bool IsChosen
        {
            get => _isChosen;
            private set => _isChosen = value;
        }

        public bool IsBlocked => currentState.GetType() == typeof(TileBlockedState);
        public bool IsEmpty => currentState.GetType() == typeof(TileEmptyState);
        public bool IsChiped => currentState.GetType() == typeof(TileChipedState);


        public void SetBlocked() => SetState(new TileBlockedState());

        public void SetEmpty() => SetState(new TileEmptyState());

        public void SetChiped(Chip chip) => SetState(new TileChipedState(chip));


        public void SetChosen(bool isChosen)
        {
            _isChosen = isChosen;
            tileChosenStateChanged.Invoke(this, _isChosen);
        }


        public bool TryGetChip(out Chip chip)
        {
            chip = null;
            if (!IsChiped) return false;
            chip = ((TileChipedState) currentState).Chip;
            return true;
        }

        public void Configure(int x, int y)
        {
            X = x;
            Y = y;
        }


        private void SetState(TileState newState)
        {
            currentState = newState;

            InvokeTileStateChangedEvent(this, newState);
        }


        private void InvokeTileStateChangedEvent(Tile tile, TileState tileState)
        {
            tileStateChangedEvent.Invoke(new TileStateChangedArgs(tile, tileState));
        }
    }
}