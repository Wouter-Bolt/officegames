module GameState

open Player
open Grid

type RoundState =
    | Undecided
    | Tied
    | Won of Player

type GameState =
    {   CurrentPlayer: Player
        CurrentColumn: int
        Players: Player * Player
        Grid: Grid
        VisualGrid: string
        RoundState: RoundState }

