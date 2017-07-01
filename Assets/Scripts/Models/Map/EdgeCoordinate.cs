using System;

/// <summary>
/// Struct with the position of an Edge. U and V are the axial coordinates of a hexagon.
/// </summary>
public struct EdgeCoordinate : IEquatable<EdgeCoordinate>
{
	public int U{ get; private set; }

	public int V{ get; private set; }

	public eEdgeDirection D { get; private set; }

	public EdgeCoordinate (int u, int v, eEdgeDirection d)
	{
		this.U = u;
		this.V = v;
		this.D = d;
	}

	public EdgeCoordinate (AxialCoordinate coord, eEdgeDirection d)
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
		return obj is EdgeCoordinate && Equals ((EdgeCoordinate)obj);
	}

	#endregion

	#region IEquatable implementation

	public bool Equals (EdgeCoordinate other)
	{
		return this.U == other.U && this.V == other.V && this.D == other.D;
	}

	#endregion
}
