using System;

[Serializable]
public class PlayerState
{
    public enum Player_State {

        CinematicEntrance,
        Gameplay,
        AutoPilot,
        Dead
    }

    private static Player_State Current_State;

    public static void SetCurrentState(Player_State state) {

        Current_State = state;
    }

    public static Player_State GetCurrentState() {

        return Current_State;
    }
}
