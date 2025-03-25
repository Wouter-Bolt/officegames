module Gameloop

open System
open Player
open Grid
open GameState
open Util

let rec getInput currentColumn =
    let leadingWhiteSpace = generateRepeatedString "     " currentColumn
    let trailingWhiteSpace = generateRepeatedString "     " (6 - currentColumn)
    Console.Write (leadingWhiteSpace + "  🔽" + trailingWhiteSpace)
    moveCurrLeft 34

    let input = Console.ReadKey()

    match input.Key with
    | ConsoleKey.Enter | ConsoleKey.Spacebar -> currentColumn
    | ConsoleKey.LeftArrow when currentColumn > 0 -> getInput (currentColumn - 1)
    | ConsoleKey.RightArrow when currentColumn < 6 -> getInput (currentColumn + 1)
    | _ -> getInput currentColumn

let isDesiredColumnAvailable desiredColumn (grid: Grid) =
    grid[desiredColumn] |> Array.tryFindIndex (function | Empty -> true | _ -> false)

let shiftTurns currentPlayer players = if currentPlayer = fst players then snd players else fst players

let getGridRow rowIndex (grid: Grid) = Array.map (fun (row: GridCell array) -> row[rowIndex] ) grid

let updateGrid currentPlayer (coordinate: Coordinate) (grid: Grid) visualGrid =
    let col = grid[fst coordinate]

    let newCol = col |> Array.updateAt (snd coordinate) (Taken currentPlayer)

    let newGrid = grid |> Array.updateAt (fst coordinate) newCol

    let newVisualGrid =
        [|0..5|]
        |> Array.rev
        |> Array.map (fun x -> (getGridRow x newGrid))
        |> Array.concat
        |> Array.map (function | Empty -> "    " | Taken p -> " " + p.Icon + " ")
        |> Array.chunkBySize 7
        |> Array.map (fun x -> divider + "|" + (String.concat "|" x) + "|")
        |> String.concat ""
        |> (fun grid -> grid + divider)

    (newGrid, newVisualGrid)

let rec checkForFourOnARow arr =
    arr
    |> Array.groupBy (function | Empty -> "" | Taken t -> t.Icon)
    |> Array.filter (fun x -> fst x <> "")
    |> Array.map (fun (_, values) -> values.Length >= 4)
    |> Array.exists (fun x -> x = true)

let checkForFourOnARow' gridCellArray =
    let rec innerCheckForFourOnARow' gridCells (player: option<Player>) count =
        if count = 4 then Won player.Value else

        match gridCells with
        | [] -> Undecided
        | Empty :: remainingGridCells -> innerCheckForFourOnARow' remainingGridCells None 1
        | Taken p :: remainingGridCells ->
            if player.IsSome && p = player.Value
            then innerCheckForFourOnARow' remainingGridCells (Some p) (count + 1)
            else innerCheckForFourOnARow' remainingGridCells None 1
    
    innerCheckForFourOnARow' (List.ofArray gridCellArray) None 1

let checkGrid (grid: Grid) =
    let cols =
        grid |> Array.map (fun x -> checkForFourOnARow' x)
    
    let colRoundState =
        cols
        |> Array.tryFind (fun roundState -> roundState.IsWon)

    match colRoundState with
    | Some roundState -> roundState
    | None ->

    let rows =
        [|0..5|]
        |> Array.map (fun r -> (getGridRow r grid))
        |> Array.map (fun r -> checkForFourOnARow' r)

    let rowRoundState =
        rows
        |> Array.tryFind (fun roundState -> roundState.IsWon)

    match rowRoundState with
    | Some roundState -> roundState
    | None ->
    
    // let diagonals =
    //     grid |> 

    let gridRoundState =
        grid
        |> Array.concat
        |> Array.exists (fun cell -> cell.IsEmpty)

    if gridRoundState then Undecided else Tied


let rec Gameloop (state: GameState) =    
    let {
        CurrentPlayer = currentPlayer;
        CurrentColumn = currentColumn;
        Players = players;
        Grid = grid;
        VisualGrid = visualGrid } = state

    let newRoundState = checkGrid grid

    if not newRoundState.IsUndecided then { state with RoundState = newRoundState } else

    Console.Write visualGrid

    moveCurrUp 17

    let combinedLenght = (fst players).Name.Length + (snd players).Name.Length + 12
    Console.Write (generateRepeatedString " " combinedLenght)
    moveCurrLeft combinedLenght

    Console.Write (currentPlayer.Name + "'s turn (" + currentPlayer.Icon + ")\n\n")

    let desiredColumn = getInput currentColumn

    Console.WriteLine()

    let newCurrentPlayer = shiftTurns currentPlayer players
    let newState = { state with CurrentColumn = desiredColumn }

    match (isDesiredColumnAvailable desiredColumn grid) with
    | None -> Gameloop newState
    | Some row ->
        let (newGrid, newVisualGrid) = updateGrid currentPlayer (desiredColumn, row) grid visualGrid
        Gameloop { newState with CurrentPlayer = newCurrentPlayer; Grid = newGrid; VisualGrid = newVisualGrid }

    