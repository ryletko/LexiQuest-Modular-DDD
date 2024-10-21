namespace LexiQuest.QuizGame.Domain.GameStates;

// public class GameStatus : Enumeration
// {
//     public static readonly GameStatus PreInit = new PreInitStatus();
//     public static readonly GameStatus Ready = new ReadyStatus();
//     public static readonly GameStatus Active = new ActiveStatus();
//     public static readonly GameStatus Completed = new CompletedStatus();
//     public static readonly GameStatus Abandoned = new AbandonedStatus();
//
//     private GameStatus(int id, string name) : base(id, name)
//     {
//     }
//
//     private class PreInitStatus() : GameStatus(0, "Pre-init");
//
//     private class ReadyStatus() : GameStatus(1, "Ready");
//
//     private class ActiveStatus() : GameStatus(2, "Active");
//
//     private class CompletedStatus() : GameStatus(3, "Completed");
//
//     private class AbandonedStatus() : GameStatus(4, "Abandoned");
// }

public enum GameStatus
{
    PreInit = 0,
    Ready = 1,
    Active = 2,
    Completed = 3,
    Abandoned = 4
}