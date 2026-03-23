namespace dungeonRPG.Input;

public class InputResult
{
    public bool ExitGame { get; set; } = false;
    public string? Message { get; set; } = null;

    public InputResult()
    {
        ExitGame = false;
        Message = null;
    }
    public InputResult(bool exitGame, string? message = null)
    {
        ExitGame = exitGame;
        Message = message;
    }
}