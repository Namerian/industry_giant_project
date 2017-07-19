using System;

/// <summary>
/// Struct with the position of a Vertice. U and V are the axial coordinates of a hexagon.
/// </summary>
public struct VerticeCoordinate : IEquatable<VerticeCoordinate>
{
	public int U{ get; private set; }

	public int V{ get; private set; }

	public eVerticeDirection D{ get; private set; }

	public VerticeCoordinate (int u, int v, eVerticeDirection d)
	{
		this.U = u;
		this.V = v;
		this.D = d;
	}

	public VerticeCoordinate (AxialCoordinate coord, eVerticeDirection d)
	{
		this.U = coord.U;
		this.V = coord.V;
		this.D = d;
	}

	#region Object overrides

	public override int GetHashCode ()
	{
		return 13 + (7 * this.U) + (7 * this.V);
	}

	public override bool Equals (object obj)
	{
		return obj is VerticeCoordinate && Equals ((VerticeCoordinate)obj);
	}

	#endregion

	#region IEquatable implementation

	public bool Equals (VerticeCoordinate other)
	{
		return this.U == other.U && this.V == other.V && this.D == other.D;
	}

	#endregion
}
