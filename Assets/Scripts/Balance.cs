using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : SingletonBehaviour<Balance> {

	public Vector2 Up { get { return transform.up; } }

    public Vector2 TransformPoint(Vector2 point)
    {
        return transform.TransformPoint(point);
    }

    public Vector2 InverseTransformPoint(Vector2 point)
    {
        return transform.InverseTransformPoint(point);
    }

    public Vector2 ClampPosition(Vector2 position, Food.Player side)
    {
        Vector2 pos = transform.TransformVector(position);
        switch (side)
        {
            case Food.Player.Player1:
                pos.x = Mathf.Min(-GameManager.Instance.OffMiddleZoneWidth, pos.x);
                break;
            case Food.Player.Player2:
                pos.x = Mathf.Max(GameManager.Instance.OffMiddleZoneWidth, pos.x);
                break;
        }
        pos = transform.InverseTransformVector(pos);
        return pos;
    }

    public bool IsInGoodSide(Food.FoodType type, Vector2 position)
    {
        Vector2 pos = transform.InverseTransformPoint(position);
        switch (type)
        {
            case Food.FoodType.Fruit:
                return pos.x < 0f;
            case Food.FoodType.Vegetable:
                return pos.x >= 0f;
            case Food.FoodType.Both:
                return true;
            default:
                return false;
        }
    }
}
