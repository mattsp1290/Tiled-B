using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map {

	public int height;
	public int width;
	public int tileHeight;
	public int tileWidth;
	public List<Layer> layers;
	public List<Tileset> tilesets;
	public string renderOrder;


	public Tileset GetTilesetByGid(int gid){
		foreach (Tileset set in tilesets){
			if ((gid > (set.firstgid - 1)) && (gid < (set.firstgid + set.tilecount))){
				return set;
			}
		}
		Debug.Log ("gid " + gid + " does not have a tileset.");
		return null;
	}
}
