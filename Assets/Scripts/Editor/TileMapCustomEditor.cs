using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(TileMapGenerator))]
    public class TileMapCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var generator = target as TileMapGenerator;
            if (GUILayout.Button("Generate TileMap"))
            {
                if (generator != null) generator.GenerateTileMap(generator.tileMapSize.x, generator.tileMapSize.y);
            }
        }
    }
}