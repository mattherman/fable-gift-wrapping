module Canvas

open System
open Browser.Types
open Vector

let clear (ctx: CanvasRenderingContext2D) width height =
    ctx.clearRect (0., 0., width, height)

let point (ctx: CanvasRenderingContext2D) (p: Vector) =
    let (x, y) = p
    ctx.beginPath ()
    ctx.arc (x, y, 2., 0., 2. * Math.PI, true)
    ctx.fill ()

let line (ctx: CanvasRenderingContext2D) fromPoint toPoint =
    ctx.moveTo fromPoint
    ctx.lineTo toPoint
    ctx.stroke ()

let drawPoints (ctx: CanvasRenderingContext2D) points =
    points |> List.iter (point ctx)

let getCanvasCtx htmlElementId = ()
