using System;

namespace DL.InterfacesRuntime
{
    public interface IButtonClick
    {
        Action OnButtonClick { get; set; }
    }
}