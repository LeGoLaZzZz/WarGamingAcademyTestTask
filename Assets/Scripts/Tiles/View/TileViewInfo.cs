using Tiles.TileStates;
using UnityEngine;

namespace Tiles.View
{
    [CreateAssetMenu(fileName = "tileViewInfo", menuName = "TileViewInfo", order = 0)]
    public class TileViewInfo : ScriptableObject
    {
        public GameObject tileViewPrefab;

        public TileState.TileStateType tileStateType;
    }
}