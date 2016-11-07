using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(MeshFilter))]
//[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class LayerMesh : MonoBehaviour {
	private Texture2D[] textures;
	private Texture2D terrainTiles;
	private Map map;
	private Material mat;
	private Layer layer;
	// Use this for initialization
	void Start () {
		// Needs some kind of grid?
		//BuildMesh (layer);
	}


	public void BuildTexture() {
		
		int sizeX = map.width;
		int sizeY = map.height;
		int texWidth = sizeX * map.tileWidth;
		int texHeight = sizeY * map.tileHeight;
		Texture2D texture = new Texture2D(texWidth, texHeight);



		for (int y = 0; y < sizeY; y++) {
			for (int x = 0; x < sizeX; x++) {
				int position = (y * sizeY) + x;
				int gid = layer.data[position];
				int pixels = map.tileWidth * map.tileHeight;
				Color[] p = new Color[pixels];
				if (gid == 0) {
					for (int i = 0; i < pixels; i++) {
						p[i] = new Color();
						p[i].a = 0;
					}
				} else {
					Tileset set = map.GetTilesetByGid (gid);
					p = set.GetTileByGid (gid);
					Debug.Log (p.GetValue(5));
				}
				texture.SetPixels(x*map.tileWidth, (texHeight - (y + 1)*map.tileHeight), map.tileWidth, map.tileHeight, p);
			}
		}

		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.Apply();

		//MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
		//mat = new Material (Shader.Find("Diffuse"));
		//mat.mainTexture = texture;
		//mesh_renderer.material = mat;
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		sprite.name = layer.name;ß
		renderer.material = mat;
		renderer.sprite = sprite;


		Debug.Log ("Done Texture!");
	}

	public void BuildMesh(Map mapParam, Layer layerParam,  Material matParam, Texture2D[] text2ds) {
		textures = text2ds;
		mat = matParam;
		map = mapParam;
		layer = layerParam;
		BuildTexture();
		/*
		int sizeX = map.width;
		int sizeY = map.height;
		int tileSize = map.tileWidth;
		int numTiles = sizeX * sizeY;
		int numTris = numTiles * 2;

		int vsizeX = sizeX + 1;
		int vsizeY = sizeY + 1;
		int numVerts = vsizeX * vsizeY;

		// Generate the mesh data
		Vector3[] vertices = new Vector3[ numVerts ];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[ numTris * 3 ];

		int x, y;
		for(y=0; y < vsizeY; y++) {
			for(x=0; x < vsizeX; x++) {
				vertices[ y * vsizeX + x ] = new Vector3( x*tileSize, -y*tileSize,  0);
				normals[ y * vsizeX + x ] = Vector3.up;
				uv[ y * vsizeX + x ] = new Vector2( (float)x / sizeX, 1f - (float)y / sizeY );
			}
		}
		Debug.Log ("Done Verts! Made " + (numVerts) + " verts." );

		for(y=0; y < sizeY; y++) {
			for(x=0; x < sizeX; x++) {
				int squareIndex = y * sizeX + x;
				int triOffset = squareIndex * 6;
				triangles[triOffset + 0] = y * vsizeX + x + 		   0;
				triangles[triOffset + 2] = y * vsizeX + x + vsizeX + 0;
				triangles[triOffset + 1] = y * vsizeX + x + vsizeX + 1;

				triangles[triOffset + 3] = y * vsizeX + x + 		   0;
				triangles[triOffset + 5] = y * vsizeX + x + vsizeX + 1;
				triangles[triOffset + 4] = y * vsizeX + x + 		   1;
			}
		}

		Debug.Log ("Done Triangles! Made " + (triangles.Length / 3) + " triangles");

		// Create a new Mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		// Assign our mesh to our filter/renderer/collider
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		Debug.Log ("Mesh filter name: " + meshFilter.name);
		MeshCollider meshCollider = GetComponent<MeshCollider>();

		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
		Debug.Log ("Done Mesh!");
		*/

	}
}
