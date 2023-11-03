module GameState

open Player
open Grid

type GameState =
    {   CurrentPlayer: Player
        CurrentColumn: int
        Players: Player * Player
        Grid: Grid
        VisualGrid: string }

