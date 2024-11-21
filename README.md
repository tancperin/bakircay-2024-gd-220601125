# 3D Object Matching Game

## Table of Contents
- [Introduction](#introduction)
- [Gameplay](#gameplay)
- [Features](#features)
- [Future Work](#future-work)

## Introduction
This is a 3D object matching game that is developed using Unity3D and C#. 
In this game the player has to match the 3D objects with their pairs.
All objects are chosen randomly from a pool of objects and are placed in a grid.
There are 2 different object shapes and 4 different object colors.
- Object Shapes:
    - Cube
    - Sphere
- Object Colors:
    - Blue
    - Green
    - Pink
    - Purple

Players can pick an object and move it around the play area. If they want to pair an object with another object, they can place the object in the placement area. When they put an object in the placement area, the object will be locked in place. If the second object is selected and placed in the placement area, the game will check if the two objects are a pair. If they are a pair, the player will get a point and the objects will be removed from the play area. If they are not a pair, the player will lose a point and the objects will be returned to their original positions.

## Gameplay
![Gameplay](Images/GamePlay.gif)

## Features
- Random object selection
- Object movement
- Object placement

## Future Work
- Paired object selection
- Object matching mechanism
- Score system
- Animation effects