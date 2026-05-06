using dungeonRPG.Dungeon;

namespace dungeonRPG.Systems.Pathfinding;

public static class Pathfinder
{
    public static int CalculateNoiseDistance(int startX, int startY, int targetX, int targetY, Room room)
    {
        if (startX == targetX && startY == targetY) return 0;

        var queue = new Queue<(int x, int y, int distance)>();
        var visited = new HashSet<(int, int)>();

        queue.Enqueue((startX, startY, 0));
        visited.Add((startX, startY));

        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.x == targetX && current.y == targetY)
            {
                return current.distance;
            }

            for (int i = 0; i < 4; i++)
            {
                int newX = current.x + dx[i];
                int newY = current.y + dy[i];

                if (newX >= 0 && newX < Room.Width && newY >= 0 && newY < Room.Height && !visited.Contains((newX, newY)))
                {
                    if (room.GetCell(newX, newY).CanHoldItems)
                    {
                        visited.Add((newX, newY));
                        queue.Enqueue((newX, newY, current.distance + 1));
                    }
                }
            }
        }
        return -1;
    }
}