using System.Security.Permissions;

public enum SceneHandlerInstruction
{
    EXIT = -1,
    CHANGESCENE = 0,
    CONTINUE = 1,
    NEWGAME = 2,
    FINISHORDER = 3,
    
}

public enum SizeCodes
{
    // 4 SIZE ARRAY [0, 0, 0, 0]
    SMALL = 8, // 1000 = 8
    MEDIUM = 13, // 1101 = 13
    LARGE = 15, // 1111 = 15
}

public enum ColorCodes
{
    // 6 SIZE ARRAY [0, 0, 0, 0, 0, 0]
    BLUE = 36, // 100100 = 36
    GREEN = 42, // 101010 = 42
    RED = 59, // 111011 = 59
}

public enum TypeCodes
{
    // 12 SIZE ARRAY [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    WORM = 650, // 001010001010
    IMP = 2184, // 100010001000
    HUMANOID = 2642, // 101001010010
}

public enum BookState
{
    MIN = -1,
    DEFAULT = 0,
    MAX = 1,
}
