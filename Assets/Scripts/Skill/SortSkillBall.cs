using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSkillBall : MonoBehaviour
{
    public int columns = 6;
    public int rows = 4;
    public float horizontalSpacing = 5f;
    public float verticalSpacing = 5f;

    public void Sort()
    {
        RectTransform parentRect = GetComponent<RectTransform>();
        int childCount = Mathf.Min(parentRect.childCount, columns * rows);
        float parentWidth = parentRect.rect.width;
        float parentHeight = parentRect.rect.height;

        float cellWidth = (parentWidth - (columns - 1) * horizontalSpacing) / columns;
        float cellHeight = (parentHeight - (rows - 1) * verticalSpacing) / rows;

        for (int i = 0; i < childCount; i++)
        {
            RectTransform childRect = parentRect.GetChild(i).GetComponent<RectTransform>();
            int row = i / columns;
            int column = i % columns;

            float xPosition = (cellWidth + horizontalSpacing) * column;
            float yPosition = -(cellHeight + verticalSpacing) * row;

            childRect.anchoredPosition = new Vector2(xPosition, yPosition);
            childRect.sizeDelta = new Vector2(cellWidth, cellHeight);
        }
    }
}
