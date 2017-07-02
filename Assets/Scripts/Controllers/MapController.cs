using System.Collections.Generic;
using UnityEngine;

public class MapController
{
	readonly Dictionary<AxialCoordinate,Tile> _tileDictionary = new Dictionary<AxialCoordinate, Tile> ();
	readonly Dictionary<EdgeCoordinate,Edge> _edgeDictionary = new Dictionary<EdgeCoordinate, Edge> ();
	readonly Dictionary<VerticeCoordinate,Vertice> _verticeDictionary = new Dictionary<VerticeCoordinate, Vertice> ();

	public MapController ()
	{
		CreateHexagonMap (2);
	}

	#region Initialization

	void CreateHexagonMap (int mapRadius)
	{
		for (int u = -mapRadius; u <= mapRadius; u++) {
			int v1 = Mathf.Max (-mapRadius, u - mapRadius);
			int v2 = Mathf.Min (mapRadius, u + mapRadius);
			for (int v = v1; v <= v2; v++) {
				CreateTile (new AxialCoordinate (u, v));
				Debug.Log (u + " " + v);
			}
		}
	}

	void CreateTile (OffsetCoordinate offset)
	{
		var axial = MapUtils.ConvertOffsetToAxial (offset);
		CreateTile (axial);
	}

	void CreateTile (AxialCoordinate axial)
	{
		if (!_tileDictionary.ContainsKey (axial)) {
			_tileDictionary.Add (axial, new Tile ());
			CreateEdges (axial);
			CreateVertices (axial);
		}
	}

	void CreateEdges (AxialCoordinate axial)
	{
		var edgeCoordinates = MapUtils.ComputeTileBorders (axial);

		foreach (var edgeCoord in edgeCoordinates) {
			if (!_edgeDictionary.ContainsKey (edgeCoord)) {
				_edgeDictionary.Add (edgeCoord, new Edge ());
			}
		}
	}

	void CreateVertices (AxialCoordinate axial)
	{
		var verticeCoordinates = MapUtils.ComputeTileCorners (axial);

		foreach (var verticeCoord in verticeCoordinates) {
			if (!_verticeDictionary.ContainsKey (verticeCoord)) {
				_verticeDictionary.Add (verticeCoord, new Vertice ());
			}
		}
	}

	#endregion //Initialization

	#region Tile Getter&Setter

	#endregion //Tile Getter&Setter

	#region Edge Getter&Setter

	#endregion //Edge Getter&Setter

	public List<AxialCoordinate> GetAllTilePositions ()
	{
		return new List<AxialCoordinate> (_tileDictionary.Keys);
	}
}
