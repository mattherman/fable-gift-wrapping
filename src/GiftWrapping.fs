module GiftWrapping

open Vector

// The cross product of two vectors, A X B, is positive if the angle from A -> B is counter-clockwise.
// If A = (ax, ay) and B = (bx, by), then A X B = (ax * by) - (ay * bx).
// However, this assumes A and B are vectors starting at the origin (0, 0). If this is not the case, we
// need to first adjust the vectors by subtracting the starting point.

// A->B X A->P = 

let directionFromLine lineStart lineEnd point = ()
    
let toConvexHull (points: Vector list): Vector list = 
    let startingPoint = points |> List.minBy (fun (x, _) -> x)
    [ startingPoint ]