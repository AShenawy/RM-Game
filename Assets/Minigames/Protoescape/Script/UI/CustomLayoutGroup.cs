using UnityEngine;
using UnityEngine.UI;

public class CustomLayoutGroup : LayoutGroup
{
    public enum FitType { Uniform, Width, Height, FixedRows, FixedColumns/*, Fixed */}

    public FitType fitType;

    public int Rows;
    public int Columns;
    public Vector2 CellSize;
    public Vector2 Spacing;
    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {

            fitX = true;
            fitY = true;
            float sqrRoot = Mathf.Sqrt(transform.childCount);
            Rows = Mathf.CeilToInt(sqrRoot);
            Columns = Mathf.CeilToInt(sqrRoot);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            Rows = Mathf.CeilToInt(transform.childCount / Columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            Columns = Mathf.CeilToInt(transform.childCount / Rows);
        }

        //if (fitType == FitType.Fixed)
        //{
        //    Rows = Mathf.CeilToInt(transform.childCount / Columns);
        //    Columns = Mathf.CeilToInt(transform.childCount / Rows);
        //}
        if (Columns == 0 || Rows == 0)
        {
            return;
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth / Columns) - ((Spacing.x / Columns) * 2) - (padding.left / Columns) - (padding.right / Columns);
        float cellHeight = (parentHeight / Rows) - ((Spacing.y / Rows) * 2) - (padding.top / Rows) - (padding.bottom / Rows);

        CellSize.x = fitX ? cellWidth : CellSize.x;
        CellSize.y = fitY ? cellHeight : CellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / Columns;
            columnCount = i % Columns;

            var item = rectChildren[i];
            var xPos = (CellSize.x * columnCount) + (Spacing.x * columnCount) + padding.left;
            var yPos = (CellSize.y * rowCount) + (Spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, CellSize.x);
            SetChildAlongAxis(item, 1, yPos, CellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical() { }
    public override void SetLayoutHorizontal() { }
    public override void SetLayoutVertical() { }
}