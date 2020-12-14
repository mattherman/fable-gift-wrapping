module GiftWrapping

open Types

// The cross product of two vectors, V1 X V2, is positive if the angle V1 -> V2 is counter-clockwise. If we have a line
// segment formed by the points (vectors) A and B and would like to determine whether another point P is to the left or
// right of the line we can use the cross-product, B X P. This calculation must be performed from the origin (0, 0) so
// we must first subtract the vector A from both B and P. Once that is done, a positive cross-product indicates that P
// is left of the line and a negative cross-product indicates it is right of the line.
//
// The cross product of B and P can be calculated as follows:
// B X P = (Bx * Py) - (By * Px)
// but since we must first subtract the vector A from each, we get:
// (B - A) X (P - A) = ((Bx - Ax) * (Py - Ay)) - ((By - Ay) * (Px - Ax))
let directionFromLine a b p =
    let crossProduct =
        (b.X - a.X) * (p.Y - a.Y) - (b.Y - a.Y) * (p.X - a.X)
    if crossProduct > 0 then Left
    elif crossProduct < 0 then Right
    else Colinear

let isLeftOfLine (line: Point * Point) point =
    let (lineStart, lineEnd) = line
    match (directionFromLine lineStart lineEnd point) with
    | Left -> true
    | _ -> false

module Imperative =
    let toConvexHull (points: Point list): Point list = 
        // Start at the leftmost point since it is guaranteed to be on the hull
        let startingPoint = points |> List.minBy (fun p -> p.X)

        let mutable hull = []
        let mutable pointOnHull = startingPoint

        let mutable atStartingPoint = false
        while not atStartingPoint do
            hull <- pointOnHull::hull
            let mutable endpoint = points |> List.head
            for candidatePoint in points do
                if (endpoint = pointOnHull) || (candidatePoint |> isLeftOfLine (pointOnHull, endpoint)) then
                    endpoint <- candidatePoint
            pointOnHull <- endpoint
            atStartingPoint <- startingPoint = endpoint
        hull

module Hybrid =
    let toConvexHull (points: Point list): Point list = 
        let findNextPointOnHull pointOnHull candidatePoints =
            candidatePoints
            |> List.reduce (fun endpoint candidatePoint ->
                if (endpoint = pointOnHull) || (candidatePoint |> isLeftOfLine (pointOnHull, endpoint)) then
                    candidatePoint
                else
                    endpoint)

        // Start at the leftmost point since it is guaranteed to be on the hull
        let startingPoint = points |> List.minBy (fun p -> p.X)

        let mutable hull = []
        let mutable pointOnHull = startingPoint

        let mutable atStartingPoint = false
        while not atStartingPoint do
            hull <- pointOnHull:: hull
            pointOnHull <- findNextPointOnHull pointOnHull points
            atStartingPoint <- startingPoint = pointOnHull
        hull

module Functional =
    let toConvexHull (points: Point list): Point list = 
        let findNextPointOnHull pointOnHull candidatePoints =
            candidatePoints
            |> List.reduce (fun endpoint candidatePoint ->
                if (endpoint = pointOnHull) || (candidatePoint |> isLeftOfLine (pointOnHull, endpoint)) then
                    candidatePoint
                else
                    endpoint)

        let rec findRemainingPointsOnHull startingPoint pointOnHull candidatePoints =
            let nextPointOnHull = findNextPointOnHull pointOnHull points
            if nextPointOnHull = startingPoint then
                [nextPointOnHull]
            else
                nextPointOnHull::(findRemainingPointsOnHull startingPoint nextPointOnHull candidatePoints)

        // Start at the leftmost point since it is guaranteed to be on the hull
        let startingPoint = points |> List.minBy (fun p -> p.X)
        let pointOnHull = startingPoint
        findRemainingPointsOnHull startingPoint pointOnHull points