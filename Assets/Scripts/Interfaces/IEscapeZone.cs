using System;

namespace DL.InterfacesRuntime
{
    public interface IEscapeZone
    {
        Action OnEscaped { get; set; }
    }
}