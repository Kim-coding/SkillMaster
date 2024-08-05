using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSkillBall : MonoBehaviour
{
    public int columns = 6;
    public int rows = 4;

    private List<SkillBallController> skillBalls = new List<SkillBallController>();

    public void Sort()
    {
        SortByLevel();

        RectTransform parentRect = GetComponent<RectTransform>();
        int childCount = Mathf.Min(parentRect.childCount, columns * rows);
        float parentWidth = parentRect.rect.width;
        float parentHeight = parentRect.rect.height;

        float cellWidth = (parentWidth - (columns - 1)) / columns;
        float cellHeight = (parentHeight - (rows - 1)) / rows;

        for (int i = 0; i < childCount; i++)
        {
            RectTransform childRect = skillBalls[i].GetComponent<RectTransform>();
            int row = i / columns;
            int column = i % columns;

            float xPosition = cellWidth * column;
            float yPosition = -cellHeight * row;

            childRect.anchoredPosition = new Vector2(xPosition, yPosition) + new Vector2(cellWidth / 2f - parentWidth / 2f, -cellHeight / 2f + parentHeight / 2f); 
            childRect.sizeDelta = new Vector2(cellWidth, cellHeight);
        }
    }

    private void SortByLevel()
    {
        skillBalls = GameMgr.Instance.playerMgr.skillBallControllers;

        skillBalls.Sort((a, b) => b.tier.CompareTo(a.tier));

        foreach (var skill in skillBalls)
        {
            skill.transform.SetAsLastSibling();
        }
    }
}
