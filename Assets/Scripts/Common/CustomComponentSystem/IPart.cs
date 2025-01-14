using UnityEngine;
using System.Collections;
public interface IPart
{
    IWhole whole { get; set; }
    bool allowed { get; set; }
    bool running { get; }
    void Check();
    void Assemble();
    void Resume();
    void Suspend();
    void Disassemble();
}