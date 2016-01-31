using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof (Collider))]
public class Door : MonoBehaviour {
    public enum DoorState { Unlocked, Locked }

    public Direction Orientation;
    public bool IsPortal { get; private set; }
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
                IsPortal = (parent.MapIndexY == map.worldSize - 1) ? true : false;
                if (parent.MapIndexY + 1 < map.worldSize) {
                    Tile adjacent = map.grid[parent.MapIndexX, parent.MapIndexY + 1];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        state = DoorState.Unlocked;
                    }
                }
                break;
            case Direction.Down:
                IsPortal = (parent.MapIndexY == 0) ? true : false;
                if (parent.MapIndexY - 1 >= 0) {
                    Tile adjacent = map.grid[parent.MapIndexX, parent.MapIndexY - 1];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        state = DoorState.Unlocked;
                    }
                }
                break;
            case Direction.Left:
                IsPortal = (parent.MapIndexX == 0) ? true : false;
                if (parent.MapIndexX - 1 >= 0) {
                    Tile adjacent = map.grid[parent.MapIndexX - 1, parent.MapIndexY];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        state = DoorState.Unlocked;
                    }
                }
                break;
            case Direction.Right:
                IsPortal = (parent.MapIndexX == map.worldSize - 1) ? true : false;
                if (parent.MapIndexX + 1 < map.worldSize) {
                    Tile adjacent = map.grid[parent.MapIndexX + 1, parent.MapIndexY];
                    if (adjacent == null) {
                        State = DoorState.Locked;
                    } else {
                        state = DoorState.Unlocked;
                    }
                }
                break;
        }
    }


}
