using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLol : MonoBehaviour
{
    /*
     * This code is under heavy revision, and is bald
     */

    public int[,] RoomColliders = new int[200, 200];
    private List<Room> AllRooms= new List<Room>();
    public void ClearRooms()
    {
        RoomColliders = new int[200, 200];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRandomLayout();
        }
    }

    public void GenerateRandomLayout()
    {
        ClearRooms();
        PopulateRooms();
        PlaceRoom(3, new Vector2(RoomColliders.GetLength(0) / 2, RoomColliders.GetLength(0) / 2), -1);

    }
    public void PopulateRooms()
    {
        AllRooms = new List<Room>()
        {
            new Room(0),
            new Room(1),
            new Room(2),
            new Room(4),
        };
    }

    public int PlaceRoom(int level, Vector2 top_pos, int dir)
    {
        //dir is the direction the room is being generated in
        // -1 = any dir
        // 0 = Top
        // 1 = Right
        // 2 = Bottom
        // 3 = Left
        level--;
        List<Room> Rooms = new List<Room>();
        foreach (var a in AllRooms)
        {
            if (level == 0 && a.IsEndpoint)
            {
                bool sex = false;
                if (dir == -1) sex = true;
                else if (dir == 0 && a.BottomDoor != new Vector2(-6969, -6969)) sex = true;
                else if (dir == 2 && a.TopDoor != new Vector2(-6969, -6969)) sex = true;
                else if (dir == 1 && a.LeftDoor != new Vector2(-6969, -6969)) sex = true;
                else if (dir == 3 && a.RightDoor != new Vector2(-6969, -6969)) sex = true;
                if (sex) Rooms.Insert(Random.Range(0, Rooms.Count), a);
            }
            else if (!a.IsEndpoint)
            {
                bool sex = false;
                if (dir == -1) sex = true;
                else if (dir == 0 && a.BottomDoor != new Vector2(-6969, -6969)) sex = true;
                else if (dir == 2 && a.TopDoor != new Vector2(-6969, -6969)) sex = true;
                else if (dir == 1 && a.LeftDoor != new Vector2(-6969, -6969)) sex = true;
                else if (dir == 3 && a.RightDoor != new Vector2(-6969, -6969)) sex = true;
                if (sex) Rooms.Insert(Random.Range(0, Rooms.Count), a);
            }
        }
        bool wasplaced = false;
        for (int i = 0; i < Rooms.Count; i++)
        {
            if (RoomColliders[(int)top_pos.x, (int)top_pos.y] > 0)
            {
                continue;
            }
            RoomColliders[(int)top_pos.x, (int)top_pos.y]++;

            if (level != 0)
            {
                if (PlaceRoom(level, new Vector2(top_pos.x + 1, top_pos.y), 1) == -1)
                {
                    RoomColliders[(int)top_pos.x, (int)top_pos.y]--;

                    continue;
                }
            }
            wasplaced = true;

        }
        if (!wasplaced)
        {
            return -1;
        }

        return 1;
    }

}



public class Room
{
    public bool IsEndpoint = false;
    public Vector2 LeftDoor = new Vector2(-6969, -6969);
    public Vector2 RightDoor = new Vector2(-6969, -6969);
    public Vector2 TopDoor = new Vector2(-6969, -6969);
    public Vector2 BottomDoor = new Vector2(-6969, -6969);
    public Room(int roomid)
    {

    }
}
public class ComlpetedRoom
{
    public int RoomId;
    public Vector2 Pos;
    public ComlpetedRoom(int roomid, Vector2 pos)
    {
        RoomId= roomid;
        Pos= pos;
    }
}
