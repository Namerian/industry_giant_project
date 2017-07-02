using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
	readonly Vector2 centerUV = new Vector2 (0.5f, 0.5f);
	readonly Vector2 northUV = new Vector2 (0.75f, 1f);
	readonly Vector2 northeastUV = new Vector2 (1f, 0.5f);
	readonly Vector2 southeastUV = new Vector2 (0.75f, 0f);
	readonly Vector2 southUV = new Vector2 (0.25f, 0f);
	readonly Vector2 southwestUV = new Vector2 (0f, 0.5f);
	readonly Vector2 northwestUV = new Vector2 (0.25f, 1f);

	[SerializeField]
	MeshFilter _meshFilter = null;

	[SerializeField]
	MeshCollider _meshCollider = null;

	MapController _mapController;

	bool _initialized = false;

	public void Initialize (MapController mapController)
	{
		_mapController = mapController;

		_initialized = true;

		Draw ();
	}

	public void Draw ()
	{
		if (!_initialized) {
			return;
		}

		var tilePositions = _mapController.GetAllTilePositions ();

		var meshVertices = new List<Vector3> ();
		var meshIndices = new List<int> ();
		var meshUVs = new List<Vector2> ();

		//**********************************************
		foreach (AxialCoordinate tilePos in tilePositions) {
			VerticeCoordinate northVert = MapUtils.ComputeTileCorner (tilePos, eHexDirection.NORTH);
			VerticeCoordinate northeastVert = MapUtils.ComputeTileCorner (tilePos, eHexDirection.NORTHEAST);
			VerticeCoordinate southeastVert = MapUtils.ComputeTileCorner (tilePos, eHexDirection.SOUTHEAST);
			VerticeCoordinate southVert = MapUtils.ComputeTileCorner (tilePos, eHexDirection.SOUTH);
			VerticeCoordinate southwestVert = MapUtils.ComputeTileCorner (tilePos, eHexDirection.SOUTHWEST);
			VerticeCoordinate northwestVert = MapUtils.ComputeTileCorner (tilePos, eHexDirection.NORTHWEST);

			int index = meshVertices.Count;

			meshVertices.Add (MapUtils.ConvertAxialToPixel (tilePos));
			meshVertices.Add (MapUtils.ConvertVerticeToPixel (northVert));
			meshVertices.Add (MapUtils.ConvertVerticeToPixel (northeastVert));
			meshVertices.Add (MapUtils.ConvertVerticeToPixel (southeastVert));
			meshVertices.Add (MapUtils.ConvertVerticeToPixel (southVert));
			meshVertices.Add (MapUtils.ConvertVerticeToPixel (southwestVert));
			meshVertices.Add (MapUtils.ConvertVerticeToPixel (northwestVert));

			meshIndices.Add (index);
			meshIndices.Add (index + 1);
			meshIndices.Add (index + 2);

			meshIndices.Add (index);
			meshIndices.Add (index + 2);
			meshIndices.Add (index + 3);

			meshIndices.Add (index);
			meshIndices.Add (index + 3);
			meshIndices.Add (index + 4);

			meshIndices.Add (index);
			meshIndices.Add (index + 4);
			meshIndices.Add (index + 5);

			meshIndices.Add (index);
			meshIndices.Add (index + 5);
			meshIndices.Add (index + 6);

			meshIndices.Add (index);
			meshIndices.Add (index + 6);
			meshIndices.Add (index + 1);

			meshUVs.Add (centerUV);
			meshUVs.Add (northUV);
			meshUVs.Add (northeastUV);
			meshUVs.Add (southeastUV);
			meshUVs.Add (southUV);
			meshUVs.Add (southwestUV);
			meshUVs.Add (northwestUV);
		}

		//**********************************************
		var mesh = new Mesh ();
		mesh.SetVertices (meshVertices);
		mesh.SetTriangles (meshIndices, 0);
		mesh.RecalculateNormals ();
		mesh.SetUVs (0, meshUVs);

		_meshFilter.mesh = mesh;
		_meshCollider.sharedMesh = mesh;
	}
}
