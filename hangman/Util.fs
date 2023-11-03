module Util
open System

let stringToCharList (string: string) =
    string |> Seq.toList

let stringToCharArray (string: string) =
    string |> Seq.toArray

let charListToString (charList: char list) =
    System.String.Concat(Array.ofList(charList))

let charArrayToString (charArray: char array) =
    System.String.Concat(charArray)

let moveCurrUp rows =
    let currentPostition = Console.GetCursorPosition()
    match currentPostition with
    | (_, y) -> Console.SetCursorPosition (Console.CursorLeft, y - rows)

let moveCurrDown rows =
    let currentPostition = Console.GetCursorPosition()
    match currentPostition with
    | (_, y) -> Console.SetCursorPosition (Console.CursorLeft, y + rows)

let moveCurrRight cols =
    let currentPostition = Console.GetCursorPosition()
    match currentPostition with
    | (x, _) -> Console.SetCursorPosition (x + cols, Console.CursorTop)

let moveCurrLeft cols =
    let currentPostition = Console.GetCursorPosition()
    match currentPostition with
    | (x, _) -> Console.SetCursorPosition (x - cols, Console.CursorTop)


let rec printList list =
    match list with
    | [] -> printf ""
    | x :: [] -> printf "%A\n" x
    | x :: xs -> printf "%A, " x; printList xs

let allowedCharacters = ['a'..'z'] @ ['ø'; 'æ'; 'å']

let isAllowedCharacter char = List.exists (fun c -> c = char) allowedCharacters
