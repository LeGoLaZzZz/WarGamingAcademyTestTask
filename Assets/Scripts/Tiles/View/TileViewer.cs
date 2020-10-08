using System;
using System.Collections.Generic;
using Tiles.TileStates;
using UnityEngine;

namespace Tiles.View
{
    public class TileViewer : MonoBehaviour
    {
        [SerializeField] private Tile tile;
        [SerializeField] private TileViewInfo currentViewInfo;
        [SerializeField] private SpriteRenderer chosenViewSpriteRenderer;
        [SerializeField] private TileViewInfosContainer tileViewInfosContainer;


        private Dictionary<TileState.TileStateType, GameObject> _spawnedTileViews =
            new Dictionary<TileState.TileStateType, GameObject>();


        private void Awake()
        {
            if (tile == null)
            {
                tile = GetComponentInParent<Tile>();
                if (tile == null)
                {
                    throw new Exception("Cant find tile script");
                }
            }
        }


        private void OnEnable()
        {
            tile.tileStateChangedEvent.AddListener(OnTileStateChangeEvent);
            tile.tileChosenStateChanged.AddListener(OnTileChosenStateChanged);
        }

        private void Start()
        {
            chosenViewSpriteRenderer.enabled = false;
        }

        private void OnDisable()
        {
            tile.tileChosenStateChanged.RemoveListener(OnTileChosenStateChanged);
            tile.tileStateChangedEvent.RemoveListener(OnTileStateChangeEvent);
        }

        private void OnTileChosenStateChanged(Tile tile, bool isChosen)
        {
            chosenViewSpriteRenderer.enabled = isChosen;
        }

        private void OnTileStateChangeEvent(TileStateChangedArgs args)
        {
            ChangeView(tileViewInfosContainer.GetTileView(args.newState.GetViewStateType()));
        }

        private void ChangeView(TileViewInfo newViewInfo)
        {
            DeactivateView(currentViewInfo.tileStateType);
            currentViewInfo = newViewInfo;
            ActivateView(currentViewInfo.tileStateType);
        }

        private void DeactivateView(TileState.TileStateType stateType)
        {
            if (_spawnedTileViews.TryGetValue(stateType, out var spawnedTileView)) spawnedTileView.SetActive(false);
        }

        private void ActivateView(TileState.TileStateType stateType)
        {
            if (_spawnedTileViews.TryGetValue(stateType, out var spawnedTileView))
                spawnedTileView.SetActive(true);
            else
                InstantiateNewView(stateType);
        }

        private void InstantiateNewView(TileState.TileStateType stateType)
        {
            if (_spawnedTileViews.ContainsKey(stateType) && _spawnedTileViews[stateType] != null)
                throw new Exception(String.Format("TileView of state {0} already instantiated", stateType));

            var newTileView = Instantiate(tileViewInfosContainer.GetTileView(stateType).tileViewPrefab,
                transform, false);
            _spawnedTileViews[stateType] = newTileView;
        }
    }
}