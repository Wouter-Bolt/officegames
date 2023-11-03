module Grid

open Player

type Coordinate = int * int

type GridCell =
    | Empty
    | Taken of by: Player

type Grid = GridCell array array

let generateInitialGrid =
    let rows = [|0..5|] |> Array.map (fun _ -> Empty)
    [|0..6|] |> Array.map (fun _ -> rows)

let visualGrid = "
+----+----+----+----+----+----+----+
|    |    |    |    |    |    |    |
+----+----+----+----+----+----+----+
|    |    |    |    |    |    |    |
+----+----+----+----+----+----+----+
|    |    |    |    |    |    |    |
+----+----+----+----+----+----+----+
|    |    |    |    |    |    |    |
+----+----+----+----+----+----+----+
|    |    |    |    |    |    |    |
+----+----+----+----+----+----+----+
|    |    |    |    |    |    |    |
+----+----+----+----+----+----+----+
"

let divider = "\n+----+----+----+----+----+----+----+\n"