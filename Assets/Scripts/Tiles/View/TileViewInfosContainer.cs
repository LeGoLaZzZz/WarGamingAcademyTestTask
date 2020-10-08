using System;
using System.Collections.Generic;
using UnityEngine;
using static Tiles.TileStates.TileState;

namespace Tiles.View
{
    [CreateAssetMenu(fileName = "TileViewInfosContainer", menuName = "TileViewInfosContainer", order = 0)]
    public class TileViewInfosContainer : ScriptableObject
    {
        [SerializeField] private TileViewInfo[] tileViewInfoArray;
        private Dictionary<TileStateType, TileViewInfo> _tileViewInfosDict;


        public TileViewInfo GetTileView(TileStateType stateType)
        {
            if (_tileViewInfosDict == null) GenerateDictionary();

            if (_tileViewInfosDict.TryGetValue(stateType, out var tileViewState))
            {
                return tileViewState;
            }

            throw new Exception("There are no state :" + stateType);
        }


        private void GenerateDictionary()
        {
            if (tileViewInfoArray.Length > 0)
            {
                _tileViewInfosDict = new Dictionary<TileStateType, TileViewInfo>();
                foreach (var viewState in tileViewInfoArray)
                {
                    _tileViewInfosDict[viewState.tileStateType] = viewState;
                }
            }
        }
    }
}