using FingerPrintSimulator.Components.Model;
using System;

namespace FingerPrintSimulator.GUI.View
{
    public interface ILoginView
    {
        UserEntry FoundUser { get; set; }
        event Action<byte[]> SearchFingerPrint;
        event Action<byte[], string> AddUser;
    }
}