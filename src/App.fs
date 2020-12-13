module App

open System
open Browser.Dom
open Browser.Types

open Canvas
open Vector

let rnd = Random()

let height = 800.
let width = 800.

let body = (document.getElementsByTagName "body").[0] :?> HTMLBodyElement

let canvas = document.getElementById "canvas" :?> HTMLCanvasElement 

let ctx = canvas.getContext_2d()

let getRandomPoints (): Vector list =
    List.init 50 (fun _ ->
        // Limit range of random values to the center of the canvas
        let x = float (int (0.25 * width) + (rnd.Next (int (0.5 * width))))
        let y = float (int (0.25 * height) + (rnd.Next (int (0.5 * height))))
        (x, y))

let mutable points = getRandomPoints ()

let startWrapping () = ()

let randomizeButton = document.getElementById "randomizeButton"
randomizeButton.onclick <- fun _ ->
    clear ctx width height
    points <- getRandomPoints ()
    points |> drawPoints ctx

let wrapButton = document.getElementById "wrapButton"
wrapButton.onclick <- fun _ -> startWrapping ()

points |> drawPoints ctx