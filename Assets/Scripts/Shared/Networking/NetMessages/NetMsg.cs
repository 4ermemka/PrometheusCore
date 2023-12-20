using System;

public static class NetOP
{
    public const int None = 0;
    public const int UpdateSessionConfiguration = 1;
    public const int UpdatePlayer = 20;
}


[Serializable]
public abstract class NetMsg 
{
    public byte OP { get; set; }

    public NetMsg() 
    { 
        OP = NetOP.None;
    }
}
