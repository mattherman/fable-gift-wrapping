module Types

type Point = {
    X: int;
    Y: int;
}

type Direction =
    | Left
    | Right
    | Colinear