using System.Collections.Generic;
using UnityEngine;

public class WireTask : MonoBehaviour
{
    [SerializeField] private List<Color> wireColorList = new List<Color>();
    [SerializeField] private List<Wire> leftWireList = new List<Wire>();
    [SerializeField] private List<WireSlot> rightWireList = new List<WireSlot>();

    private List<Color> availableColorList;
    private List<int> availableLeftWireIndex;
    private List<int> availableRightWireIndex;

    private void Awake()
    {
        availableColorList = new List<Color>(wireColorList);
        availableLeftWireIndex = new List<int>();
        availableRightWireIndex = new List<int>();

        for(int i = 0; i < leftWireList.Count; i++)
        {
            availableLeftWireIndex.Add(i);
        }

        for (int i = 0; i < rightWireList.Count; i++)
        {
            availableRightWireIndex.Add(i);
        }

        while(availableColorList.Count > 0 && availableLeftWireIndex.Count > 0 && availableRightWireIndex.Count > 0)
        {
            Color pickedColor = availableColorList[Random.Range(0, availableColorList.Count)];
            int pickedLeftWireIndex = Random.Range(0, availableLeftWireIndex.Count);
            int pickedRightWireIndex = Random.Range(0, availableRightWireIndex.Count);

            leftWireList[availableLeftWireIndex[pickedLeftWireIndex]].SetColor(pickedColor);
            rightWireList[availableRightWireIndex[pickedRightWireIndex]].SetExpectedColor(pickedColor);

            availableColorList.Remove(pickedColor);
            availableLeftWireIndex.RemoveAt(pickedLeftWireIndex);
            availableRightWireIndex.RemoveAt(pickedRightWireIndex);
        }
    }
}
