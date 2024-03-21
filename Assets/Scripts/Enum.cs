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
    //6 SIZE ARRAY [0, 0, 0, 0, 0, 0]
    RED = 59, // 111011 = 59
    GREEN = 42, // 101010 = 42
    BLUE = 36, // 100100 = 36
}

public enum BookState
{
    MIN = -1,
    DEFAULT = 0,
    MAX = 1,
}