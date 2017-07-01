using System.Collections.Generic;

public class MapController
{
	readonly Dictionary<AxialCoordinate,Tile> _tileDictionary = new Dictionary<AxialCoordinate, Tile> ();
	readonly Dictionary<EdgeCoordinate,Edge> _edgeDictionary = new Dictionary<EdgeCoordinate, Edge> ();
	readonly Dictionary<VerticeCoordinate,Vertice> _verticeDictionary = new Dictionary<VerticeCoordinate, Vertice> ();

	public MapController ()
	{
		
	}

	#region Initialization

	public bool CreateTile (OffsetCoordinate offset)
	{
		var axial = MapUtils.ConvertOffsetToAxial (offset);

		if (!_tileDictionary.ContainsKey (axial)) {
			_tileDictionary.Add (axial, new Tile ());
			CreateEdges (axial);
			CreateVertices (axial);

			return true;
		}

		return false;
	}

	private void CreateEdges (AxialCoordinate axial)
	{
		var edgeCoordinates = MapUtils.ComputeTileBorders (axial);

		foreach (var edgeCoord in edgeCoordinates) {
			if (!_edgeDictionary.ContainsKey (edgeCoord)) {
				_edgeDictionary.Add (edgeCoord, new Edge ());
			}
		}
	}

	private void CreateVertices (AxialCoordinate axial)
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
}
