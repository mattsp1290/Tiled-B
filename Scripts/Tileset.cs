using System.Collections;
using System;
using UnityEngine;

[System.Serializable]
public class Tileset {
	public int columns;
	public int firstgid;
	public string image;
	public int imageheight;
	public int imagewidth;
	public int margin;
	public string name;
	public int spacing;
	public int tilecount;
	public int tilewidth;
	public int tileheight;
	[System.NonSerialized]
	private Color[][] tiles;
	[System.NonSerialized]
	private Texture2D texture;


	public void InitializeTiles(Texture2D[] textures) {
		int tileWithPadding = tilewidth + spacing;
		int numTilesPerRow = (imagewidth - spacing) / tileWithPadding;
		int numRows = (imageheight - spacing) / tileWithPadding;
		string[] imageNameSplit = image.Split ('/');
		string textureName = imageNameSplit[imageNameSplit.Length - 1].Split('.')[0];
		foreach (Texture2D tex in textures){
			if (tex.name == textureName){
				Debug.Log ("Texture found!");
				texture = tex;
				break;
			}
		}

		tiles = new Color[numTilesPerRow*numRows][];

		Debug.Log ("Tiles for " + name);
		Debug.Log ("Texture: " + texture.name);
		Debug.Log ("Num Rows: " + numRows);
		Debug.Log ("Num Tiles Per Row: " + numTilesPerRow);

		for(int y=0; y<numRows; y++) {
			for(int x=0; x<numTilesPerRow; x++) {
				//Debug.Log ("Getting tile at (" + GetPosition(x) + ", " + GetPosition(y) + ")");
				tiles[y*numTilesPerRow + x] = texture.GetPixels( GetPosition(x) , imageheight - GetPosition(y) - tileheight, tilewidth, tileheight );
			}
		}
	}

	public Color[] GetTileByGid(int gid) {
		return tiles [gid - firstgid];
	}

	private int GetPosition(int val) {
		return (val * spacing) + (val * tilewidth) + spacing;
	}
}
