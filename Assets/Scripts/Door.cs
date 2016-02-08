using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof (Collider))]
public class Door : Reward {
    public enum DoorState { Unlocked, Locked }

    public Direction Orientation;
    public DoorState State {
        get {
            return state;
        }
        set {
            state = value;
            switch (value) {
                case DoorState.Unlocked:
                    animator.Play("Unlocked");
                    break;
                case DoorState.Locked:
                    animator.Play("Locked");
                    break;
            }
        }
    }

    DoorState state;
    Animator animator;

    Map map;
    Tile parent;

    void Awake() {
        state = DoorState.Unlocked;
        animator = GetComponent<Animator>();
    }

    void Start() {
        map = GameObject.Find("Map").GetComponent<Map>();
        parent = transform.parent.gameObject.GetComponent<Tile>();
        UpdateDoorState();
    }

    // for when tile is moved,
    // test adjacencies
    public void UpdateDoorState() {
        switch (Orientation) {
            case Direction.Up:
                if (parent.MapIndexY + 1 < map.worldSize) {
                    Tile adjacent = map.grid[parent.MapIndexX, parent.MapIndexY + 1];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        State = DoorState.Unlocked;
                    }
                } else if (parent.MapIndexY + 1 == map.worldSize) {
                    State = DoorState.Locked;
                }
                break;
            case Direction.Down:
                if (parent.MapIndexY - 1 >= 0) {
                    Tile adjacent = map.grid[parent.MapIndexX, parent.MapIndexY - 1];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        State = DoorState.Unlocked;
                    }
                } else if (parent.MapIndexY - 1 < 0) {
                    State = DoorState.Locked;
                }
                break;
            case Direction.Left:
                if (parent.MapIndexX - 1 >= 0) {
                    Tile adjacent = map.grid[parent.MapIndexX - 1, parent.MapIndexY];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        State = DoorState.Unlocked;
                    }
                } else if (parent.MapIndexX - 1 < 0) {
                    State = DoorState.Locked;
                }
                break;
            case Direction.Right:
                if (parent.MapIndexX + 1 < map.worldSize) {
                    Tile adjacent = map.grid[parent.MapIndexX + 1, parent.MapIndexY];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        State = DoorState.Unlocked;
                    }
                } else if (parent.MapIndexX + 1 == map.worldSize) {
                    State = DoorState.Locked;
                }
                break;
        }
    }


}
