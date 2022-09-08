using System.Collections;
using UnityEngine;

public interface ICrowdMember
{
    void OnAdd(CrowdController crowd);
    void OnRemove(CrowdController crowd);
}