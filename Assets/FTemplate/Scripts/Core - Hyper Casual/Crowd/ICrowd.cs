using System.Collections;
using UnityEngine;

public interface ICrowd
{
    public int Size { get; set; }

    void Add(int amount);
    void Remove(int amount);
}