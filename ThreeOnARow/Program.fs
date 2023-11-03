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
        VisualGrid = visualGrid }

    moveCurrUp 4
    Console.Write "Use the arrow keys (<- ->) to move column, and hit enter to confirm\n"
    Console.Write "                                                                   \n"
    Console.Write "                                                                   \n"
    Console.Write "                                                                   \n"

    Gameloop initialState
    moveCurrDown 15
    Console.CursorVisible <- true

    0
