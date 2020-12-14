module App

open System
open Browser.Dom
open Browser.Types

open Canvas
open Types
open GiftWrapping

let rnd = Random()

let height = 800.
let width = 800.
let numberOfPoints = 50

let canvas = document.getElementById "canvas" :?> HTMLCanvasElement 

let ctx = canvas.getContext_2d()

let getRandomPoints (): Point list =
    List.init numberOfPoints (fun _ ->
        // Limit range of random values to the center of the canvas
        let x = int (0.25 * width) + (rnd.Next (int (0.5 * width)))
        let y = int (0.25 * height) + (rnd.Next (int (0.5 * height)))
        { X = x; Y = y })

let mutable points = getRandomPoints ()

let startWrapping () =
    points
    |> Functional.toConvexHull
    |> connectPoints ctx

let randomizeButton = document.getElementById "randomizeButton"
randomizeButton.onclick <- fun _ ->
    clear ctx width height
    points <- getRandomPoints ()
    points |> drawPoints ctx

let wrapButton = document.getElementById "wrapButton"
wrapButton.onclick <- fun _ -> startWrapping ()

points |> drawPoints ctx