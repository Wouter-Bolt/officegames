module Program

open System
open Gameloop
open GameState
open Player
open Util
open Grid

[<EntryPoint>]
let main args =
    Console.OutputEncoding <- Text.Encoding.Unicode
    Console.CursorVisible <- false

    Console.WriteLine "Type the name of player 1:"
    let player1 =
        Console.ReadLine()
        |> (fun name -> if name = "" then "player1" else name)
        |> (fun name -> {Id = 1; Name = name; Icon = "🔴"})

    
    Console.WriteLine "Type the name of player 2:"
    let player2 =
        Console.ReadLine()
        |> (fun name -> if name = "" then "player2" else name)
        |> (fun name -> {Id = 2; Name = name; Icon = "🔵"})
    
    let grid = generateInitialGrid
    let initialState: GameState = {
        CurrentPlayer = player1;
        CurrentColumn = 3;
        Players = (player1, player2);
        Grid = grid;
        VisualGrid = visualGrid
        RoundState = Undecided }

    moveCurrUp 4
    Console.Write "Use the arrow keys (<- ->) to move column, and hit enter to confirm\n"
    Console.Write "                                                                   \n"
    Console.Write "                                                                   \n"
    Console.Write "                                                                   \n"

    let finalState = Gameloop initialState
    moveCurrDown 15

    match finalState.RoundState with
    | Won byPlayer -> Console.Write $"{byPlayer.Icon} has won the game, congratulations player {byPlayer.Name}🥳"
    | Tied -> Console.Write "Well played, it looks like a tie to me"
    | _ -> Console.Write "This is really weird, the game ended with an ongoing state, i'd say you can safely fire the dev that wrote this"

    Console.CursorVisible <- true

    0
