using System.Runtime.Serialization;

/// <summary>
/// A 3D point structure
/// </summary>
[DataContract]
public class Point3D
{
    [DataMember]
    public float X { get; set; }

    [DataMember]
    public float Y { get; set; }

    [DataMember]
    public float Z { get; set; }
}
