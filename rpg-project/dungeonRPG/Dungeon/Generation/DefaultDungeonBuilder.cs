using dungeonRPG.Dungeon.Cells;
using dungeonRPG.Entities.Enemies;
using dungeonRPG.Input;
using dungeonRPG.Items.Currencies;
using dungeonRPG.Items.Others;
using dungeonRPG.Items.Weapons;

namespace dungeonRPG.Dungeon.Generation;

public class DefaultDungeonBuilder : IDungeonBuilder
{
    private Cell[,] _grid;
    private List<string> _instructions;
    private Random _random;
    private List<ConsoleKey> _keys;

    private int _roomsToAdd = 0;
    private bool _wantsStarterRoom = false;
    private bool _wantsCorridors = false;
    private int _itemsToPlace = 0;
    private int _weaponsToPlace = 0;
    private (int w, int h)? _centralRoomSize = null;
    private int _enemiesToPlace = 0;

    private class Rectangle
    {
        public int X, Y, Width, Height;
        public int CenterX => Math.Max(0, Math.Min(Room.Width - 1, X + Width / 2));
        public int CenterY => Math.Max(0, Math.Min(Room.Height - 1, Y + Height / 2));

        public Rectangle(int x, int y, int w, int h)
        {
            X = x; Y = y; Width = w; Height = h;
        }

        public bool Intersects(Rectangle other)
        {
            return X <= other.X + other.Width + 1 && X + Width + 1 >= other.X &&
                   Y <= other.Y + other.Height + 1 && Y + Height + 1 >= other.Y;
        }
    }

    private List<Rectangle> _rooms;

    public DefaultDungeonBuilder()
    {
        _grid = new Cell[Room.Height, Room.Width];
        _instructions = new List<string>();
        _keys = new List<ConsoleKey>();
        _random = new Random();
        _rooms = new List<Rectangle>();
    }

    public void BuildEmpty() 
    {
        _rooms.Clear();
        for (int y = 0; y < Room.Height; y++)
            for (int x = 0; x < Room.Width; x++)
                _grid[y, x] = new EmptyCell();
        
        AddSharedMovementInstructions();
    }

    public void BuildFull() 
    {
        _rooms.Clear();
        for (int y = 0; y < Room.Height; y++)
            for (int x = 0; x < Room.Width; x++)
                _grid[y, x] = new WallCell();

        AddSharedMovementInstructions();
    }

    public void AddCentralRoom(int width, int height)
    {
        _centralRoomSize = (width, height);
    }

    public void AddStarterRoom()
    {
        _wantsStarterRoom = true;
    }

    public void AddRooms()
    {
        _roomsToAdd += _random.Next(8, 16);
    }

    public void AddCorridors()
    {
        _wantsCorridors = true;
    }

    public void AddItems(int count)
    {
        _itemsToPlace += count;
        AddSharedItemInstructions();
    }

    public void AddWeapons()
    {
        _weaponsToPlace += _random.Next(8, 22);
        AddSharedItemInstructions();
    }

    public void AddEnemies(int count)
    {
        _enemiesToPlace += count;
    }

    private void AddSharedMovementInstructions()
    {
        if (!_keys.Contains(MovementKeybinds.MoveUp))
        {
            _keys.Add(MovementKeybinds.MoveUp);
            _keys.Add(MovementKeybinds.MoveLeft);
            _keys.Add(MovementKeybinds.MoveDown);
            _keys.Add(MovementKeybinds.MoveRight);
            _keys.Add(InventoryKeybinds.ToggleInventory);
            
            _instructions.Add($" [{MovementKeybinds.MovementInfo}]\t- Move");
            
            _instructions.Add($" [{InventoryKeybinds.ToggleInventoryInfo}]\t- Switch between inventory/ground mode");
        }
    }
    
    private void AddSharedItemInstructions()
    {
        if (!_keys.Contains(InventoryKeybinds.Interact))
        {
            _keys.Add(InventoryKeybinds.Interact);
            _keys.Add(InventoryKeybinds.DropItem);
            _keys.Add(InventoryKeybinds.UnequipAll);
            _keys.Add(InventoryKeybinds.SelectPrev);
            _keys.Add(InventoryKeybinds.SelectNext);
            
            _instructions.Add($" [{InventoryKeybinds.InteractInfo}]\t- Pick up / use (equip) selected item");
            _instructions.Add($" [{InventoryKeybinds.DropItemInfo}]\t- Drop selected item");
            _instructions.Add($" [{InventoryKeybinds.UnequipAllInfo}]\t- Unequip items from hands");
            _instructions.Add($" [{InventoryKeybinds.SelectInfo}]\t- Select item in inventory/ground list");
        }
    }

    public Room GetResult()
    {
        if (_centralRoomSize.HasValue)
            GenerateCentralRoom(_centralRoomSize.Value.w, _centralRoomSize.Value.h);

        if (_wantsStarterRoom)
            GenerateStarterRoom();

        if (_wantsCorridors)
            GenerateMaze();
        
        if (_roomsToAdd > 0)
            GenerateRooms(_roomsToAdd);

        if (_itemsToPlace > 0) PlaceItems(_itemsToPlace);
        if (_weaponsToPlace > 0) PlaceWeapons(_weaponsToPlace);
        if(_enemiesToPlace > 0) PlaceEnemies(_enemiesToPlace);
        
        // FOR TESTING OF STAGE 3:
        _grid[1, 1] = new EmptyCell();
        _grid[2, 2] = new EmptyCell();
        
        _grid[1, 1].TryAddItem(new HeavyModifier(new Greataxe()));
        _grid[1, 1].TryAddItem(new UnluckyModifier(new StrongModifier(new Staff())));
        _grid[2, 2].TryAddItem(new StrongModifier(new StrongModifier(new Greataxe())));
        _grid[2, 2].TryAddItem(new Greataxe());

        return new Room(_grid);
    }

    public List<string> GetInstructions()
    {
        if (!_keys.Contains(MovementKeybinds.ExitGame))
        {
            _keys.Add(ConsoleKey.J);
            _instructions.Add($" [{ConsoleKey.J}]\t- Show/close all logs");
            
            _keys.Add(ConsoleKey.H);
            _instructions.Add($" [{ConsoleKey.H}]\t- Open/close Help menu");
            
            _keys.Add(MovementKeybinds.ExitGame);
            _instructions.Add($" [{MovementKeybinds.ExitInfo}]\t- Exit game");
        }
        
        return _instructions;
    }

    public List<ConsoleKey> GetKeys()
    {
        if (!_keys.Contains(ConsoleKey.Escape))
        {
            _keys.Add(ConsoleKey.Escape);
            _instructions.Add(" [ESC]\t- Exit game");
        }
        
        return _keys;
    }
    

    private void GenerateCentralRoom(int width, int height)
    {
        int startX = (Room.Width - width) / 2;
        int startY = (Room.Height - height) / 2;
        var centralRoom = new Rectangle(startX, startY, width, height);
        _rooms.Add(centralRoom);
        CarveRoom(centralRoom);
    }

    private void GenerateStarterRoom()
    {
        var spawnRoom = new Rectangle(0, 0, 3, 2); 
        if (!IsOverlapping(spawnRoom))
        {
            _rooms.Add(spawnRoom);
            CarveRoom(spawnRoom);
        }
        else
        {
            _grid[0, 0] = new EmptyCell(); 
        }
    }

    private void GenerateRooms(int targetRooms)
    {
        if (!_wantsStarterRoom)
        {
            var spawnRoom = new Rectangle(0, 0, 5, 5); 
            if (!IsOverlapping(spawnRoom))
            {
                _rooms.Add(spawnRoom);
                CarveRoom(spawnRoom);
            }
            else _grid[0, 0] = new EmptyCell(); 
        }

        int attempts = 0;
        int currentExtraRooms = 0;

        while (currentExtraRooms < targetRooms && attempts < 250)
        {
            int w = _random.Next(5, 12);
            int h = _random.Next(4, Math.Min(9, w)); 
            int x = _random.Next(-w / 2, Room.Width - w / 2);
            int y = _random.Next(-h / 2, Room.Height - h / 2);

            var newRoom = new Rectangle(x, y, w, h);

            if (!IsOverlapping(newRoom))
            {
                _rooms.Add(newRoom);
                CarveRoom(newRoom);
                currentExtraRooms++;
            }
            attempts++;
        }
    }

    private void GenerateMaze()
    {
        var stack = new Stack<(int X, int Y)>();
        int startX = 1;
        int startY = 1;
        
        stack.Push((startX, startY));
        _grid[startY, startX] = new EmptyCell();

        var directions = new (int dx, int dy)[] 
        { 
            (0, -2), (0, 2), (-2, 0), (2, 0) 
        };

        while (stack.Count > 0)
        {
            var current = stack.Peek();
            var validNeighbors = new List<(int nx, int ny, int mx, int my)>();

            foreach (var dir in directions)
            {
                int nx = current.X + dir.dx;
                int ny = current.Y + dir.dy;

                int mx = current.X + dir.dx / 2;
                int my = current.Y + dir.dy / 2;

                if (nx > 0 && nx < Room.Width && ny > 0 && ny < Room.Height)
                {
                    if (!_grid[ny, nx].CanHoldItems) 
                    {
                        validNeighbors.Add((nx, ny, mx, my));
                    }
                }
            }

            if (validNeighbors.Count > 0)
            {
                var next = validNeighbors[_random.Next(validNeighbors.Count)];

                _grid[next.my, next.mx] = new EmptyCell();
                _grid[next.ny, next.nx] = new EmptyCell();
 
                stack.Push((next.nx, next.ny));
            }
            else
            {
                stack.Pop();
            }
        }
    }

    private void PlaceItems(int count)
    {
        int placed = 0;
        int safetyNet = 0; 
        while (placed < count && safetyNet < 1000)
        {
            int x = _random.Next(Room.Width);
            int y = _random.Next(Room.Height);

            if (_grid[y, x].CanHoldItems)
            {
                int itemType = _random.Next(5);
                switch (itemType)
                {
                    case 0: _grid[y, x].TryAddItem(new HealthPotion()); break;
                    case 1: _grid[y, x].TryAddItem(new Fireball()); break;
                    case 2: _grid[y, x].TryAddItem(new Quiver()); break;
                    case 3: _grid[y, x].TryAddItem(new Coin(_random.Next(1, 5))); break;
                    case 4: _grid[y, x].TryAddItem(new Gold(_random.Next(1, 5))); break;
                }
                placed++;
            }
            safetyNet++;
        }
    }

    private void PlaceWeapons(int count)
    {
        int placed = 0;
        int safetyNet = 0;
        
        while (placed < count && safetyNet < 1000)
        {
            int x = _random.Next(Room.Width);
            int y = _random.Next(Room.Height);

            if (_grid[y, x].CanHoldItems)
            {
                int currentGridWeaponCount = _random.Next(1, 4);
                int currentGridPlaced = 0;
                
                while (currentGridWeaponCount > currentGridPlaced && placed < count)
                {
                    int weaponType = _random.Next(4);
                    switch (weaponType)
                    {
                        case 0: _grid[y, x].TryAddItem(new StrongModifier(new Staff())); break;
                        case 1: _grid[y, x].TryAddItem(new HeavyModifier(new Spear())); break;
                        case 2: _grid[y, x].TryAddItem(new UnluckyModifier(new Greatbow())); break;
                        case 3: _grid[y, x].TryAddItem(new StrongModifier(new Greataxe())); break;
                    }
                    placed++;
                    currentGridPlaced++;
                }
            }
            safetyNet++;
        }
    }
    
    private void PlaceEnemies(int count)
    {
        int placed = 0;
        int safetyNet = 0;
    
        while (placed < count && safetyNet < 1000)
        {
            int x = _random.Next(Room.Width);
            int y = _random.Next(Room.Height);
            int enemyType = _random.Next(3);

            if (_grid[y, x].CanHoldItems && _grid[y, x].Enemy == null)
            {
                switch (enemyType)
                {
                    case 0:
                        _grid[y, x].Enemy = new Goblin(100, 10, 10); 
                        break;
                    case 1:
                        _grid[y, x].Enemy = new Bat(50, 2, 2); 
                        break;
                    case 2:
                        _grid[y, x].Enemy = new EvilKnight(500, 15, 20); 
                        break;
                }
                placed++;
            }
            safetyNet++;
        }
    }

    private bool IsOverlapping(Rectangle newRoom)
    {
        foreach (var room in _rooms)
            if (room.Intersects(newRoom)) return true;
        return false;
    }

    private void CarveRoom(Rectangle room)
    {
        for (int y = room.Y; y < room.Y + room.Height; y++)
            for (int x = room.X; x < room.X + room.Width; x++)
                if (x >= 0 && x < Room.Width && y >= 0 && y < Room.Height)
                    _grid[y, x] = new EmptyCell();
    }
}