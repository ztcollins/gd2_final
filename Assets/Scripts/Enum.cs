using System.Security.Permissions;

public enum SceneHandlerInstruction
{
    EXIT = -1,
    CHANGESCENE = 0,
    CONTINUE = 1,
    NEWGAME = 2,
    FINISHORDER = 3,
    
}

public enum RitualButtonState
{
    UNCLICKED = 0,
    CONFIRM = 1,
}

public enum SizeCodes
{
    // 4 SIZE ARRAY [0, 0, 0, 0]
    TINY = 0, // 0000 = 0
    SMALL = 8, // 1000 = 8
    MEDIUM = 5, // 0101 = 5
    LARGE = 13, // 1101 = 13
    HUGE = 15, // 1111 = 15
}

public enum ColorCodes
{
    // 6 SIZE ARRAY [0, 0, 0, 0, 0, 0]
    BLUE = 36, // 100100 = 36
    GREEN = 42, // 101010 = 42
    YELLOW = 27, // 011011 = 27
    BROWN = 19, // 010011 = 19
    PURPLE = 63, // 111111 = 63
    ORANGE = 45, // 101101 = 45
    WHITE = 53, // 101110 = 53
    RED = 59, // 111011 = 59
    BLACK = 46, // 110101 = 46
}

public enum TypeCodes
{
    // 12 SIZE ARRAY [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    IMP = 2184, // 100010001000 = 2184
    WORM = 650, // 001010001010 = 650
    GOLEM = 340, // 000101010100 = 340
    HUMANOID = 2642, // 101001010010 = 2642
    CHIMERA = 1445, // 010110100101 = 1445
    SERPENT = 1879, // 011101010111 = 1879
    CURSE = 3445, // 110101110101 = 3445
}

public enum SpecialCodes
{
    // 22 SIZE ARRAY [SIZE, COLOR, TYPE]
    BEELZEBUB = 1484117 // 0101101010010101010101 = 1484117
}

public enum BookState
{
    MIN = -1,
    DEFAULT = 0,
    MAX = 1,
}

public enum PointerRotationState
{
    RIGHT = 0,
    UP = 1,
    LEFT = 2,
    DOWN = 3,
}

public enum PointerAnimationState
{
    UNDULATE = 0,
}

public enum TutorialState
{
    BLOCKING = 0,
}

public enum TutorialActionState
{
    ONKEY = 0,
    ONCLICK = 1,
    COMPLETE = 99,
}
