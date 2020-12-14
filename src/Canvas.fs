module Canvas

open System
open Browser.Dom
open Browser.Types
open Types

let toTuple point =
    (float point.X, float point.Y)

let clear (ctx: CanvasRenderingContext2D) width height =
    ctx.clearRect (0., 0., width, height)

let point (ctx: CanvasRenderingContext2D) (point: Point) =
    let (x, y) = point |> toTuple
    ctx.beginPath ()
    ctx.arc (float point.X, float y, 2., 0., 2. * Math.PI, true)
    ctx.fill ()

let line (ctx: CanvasRenderingContext2D) (fromPoint: Point) (toPoint: Point) =
    ctx.moveTo (fromPoint |> toTuple)
    ctx.lineTo (toPoint |> toTuple)
    ctx.stroke ()
    toPoint

let drawPoints (ctx: CanvasRenderingContext2D) points =
    points |> List.iter (point ctx)

let connectPoints (ctx: CanvasRenderingContext2D) (points: Point list) =
    points @ [points |> List.head] // Add the first point to the end of the list to complete the loop
    |> List.reduce (fun previous next -> line ctx previous next)

let getCanvasCtx htmlElementId =
    let canvas = document.getElementById htmlElementId :?> HTMLCanvasElement
    canvas.getContext_2d()
