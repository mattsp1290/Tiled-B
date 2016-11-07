using UnityEngine;
using System.Collections;

public class Tiled : MonoBehaviour {
	[SerializeField]
	private string fileName;
	[SerializeField]
	private Material material;
	private Map map;
	private string json;

	// Use this for initialization
	void Start () {
		json = System.IO.File.ReadAllText("Assets/Maps/" + fileName);
		Debug.Log (json);
		map = JsonUtility.FromJson<Map>(json);
		RenderMap (map);
	}

	void RenderMap(Map map) {
		Texture2D[] textures = Resources.LoadAll<Texture2D>("");

		foreach (Tileset tileset in map.tilesets) {
			tileset.InitializeTiles (textures);
		}


		foreach (Layer layer in map.layers) {
			GameObject layerObject = new GameObject ();
			layerObject.name = layer.name;
			layerObject.transform.position = transform.position;
			layerObject.transform.parent = transform;
			LayerMesh mesh = layerObject.AddComponent<LayerMesh> ();
			mesh.BuildMesh (map, layer, material, textures);
			Debug.Log (layer.name);
		}
	}
		
}
