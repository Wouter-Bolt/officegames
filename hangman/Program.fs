module Program
open System
open Gameloop
open Util

let backspace (acc: char list) =
  if acc.Length > 0
  then
    let newAcc = acc |> List.removeAt (acc.Length - 1)
    newAcc
  else acc

[<EntryPoint>]
let main args =
    Console.OutputEncoding <- Text.Encoding.Unicode
    Console.CursorVisible <- true

    Console.Write "Type the word to guess and hit enter (input will be hidden): "

    let word =
        let rec readWord (acc: char list) =
            let inputChar = Console.ReadKey(true)
            match inputChar.Key with
            | ConsoleKey.Enter -> charListToString acc
            | ConsoleKey.Backspace -> acc |> backspace |> readWord
            | _ when (isAllowedCharacter inputChar.KeyChar) -> readWord (acc @ [inputChar.KeyChar])
            | _ -> readWord acc

        readWord []

    moveCurrLeft 61

    Console.Write "Start guessing by typing characters, press enter to continue."

    Console.ReadKey(true) |> ignore

    moveCurrLeft 61

    Console.Write "                                                             "

    moveCurrLeft 61

    let result = Gameloop word
    printf "%s" result

    Console.CursorVisible <- true

    0
