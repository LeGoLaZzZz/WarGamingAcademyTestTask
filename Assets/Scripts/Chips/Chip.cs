using System;
using System.Collections;
using Tiles;
using UnityEngine;
using UnityEngine.Events;
using static Chips.ChipColorsConfigContainer;

namespace Chips
{
    [Serializable]
    public class AnyChipClicked : UnityEvent<Chip>
    {
    }


    public class Chip : MonoBehaviour
    {
        public static AnyChipClicked AnyChipClickedEvent = new AnyChipClicked();


        public Tile tile;
        public float swapSpeed = 1f;

        [SerializeField] private float swapDistance = 0.01f;
        [SerializeField] private ChipColorConfig _chipColorConfig;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private ChipColor _chipColor;
        private Transform _transform;

        public enum ChipColor
        {
            RedChip = 0,
            YellowChip = 1,
            BlueChip = 2
        }

        public Tile Tile
        {
            get => tile;
            private set => tile = value;
        }

        public ChipColor CurrentChipColor
        {
            get => _chipColor;
            private set => _chipColor = value;
        }


        public void SetOnTile(Tile newTile, bool withAnimation = true)
        {
            Tile = newTile;
            if (withAnimation) StartCoroutine(MoveToTile(Tile.transform));
            else
                transform.position = Tile.transform.position;
        }

        public void SetUpColorConfig(ChipColorConfig colorConfig)
        {
            _chipColorConfig = colorConfig;
            _chipColor = _chipColorConfig.chipColor;
            spriteRenderer.sprite = _chipColorConfig.colorSprite;
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            spriteRenderer.sprite = _chipColorConfig.colorSprite;
        }


        private void OnMouseDown()
        {
            AnyChipClickedEvent.Invoke(this);
        }


        private IEnumerator MoveToTile(Transform tileTransform)
        {
            while ((_transform.position - tileTransform.position).magnitude > swapDistance)
            {
                var move = (tileTransform.position - _transform.position).normalized;
                _transform.Translate(move * (swapSpeed * Time.deltaTime));
                yield return null;
            }

            _transform.position = tileTransform.position;
        }
    }
}