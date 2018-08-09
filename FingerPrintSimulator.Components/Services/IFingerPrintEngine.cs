using FingerPrintSimulator.Components.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FingerPrintSimulator.Components.Services
{
    public interface IFingerPrintEngine
    {
        bool EnrollFingerPrint(string name, byte[] figerPrint);
        UserEntry GetInfoForFingerPrint(byte[] figerPrint);
    }
}
