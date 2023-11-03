module Gameloop
open System
open Util
open Hangman

let rec private Innerloop (word: char array) (acc: char array) (guesses: char list) tries =
    let input = Console.ReadKey(true).KeyChar |> System.Char.ToLower

    if not (isAllowedCharacter input) then Innerloop word acc guesses tries else

    let newGuesses = input :: guesses

    printList newGuesses

    let newAcc =
        acc
        |> Array.mapi (fun k v ->
            if word[k] = input
            then input
            else v)

    Console.WriteLine(charArrayToString newAcc)

    let newTries = if (charArrayToString acc) = (charArrayToString newAcc) then tries + 1 else tries

    Console.WriteLine(frames[newTries])

    match (charArrayToString newAcc = charArrayToString word) with
    | true -> "Congratulations!🥳"
    | false when newTries >= 10 -> "Bummer, better luck next time! the word was: " + (charArrayToString word)
    | _ -> moveCurrUp 11; Innerloop word newAcc newGuesses newTries

let Gameloop (word: string) =
    let wordAsCharArray = word.ToCharArray()
    let InitialAcc =
        wordAsCharArray
        |> Array.map (fun _ -> '_')
    Innerloop wordAsCharArray InitialAcc [] 0