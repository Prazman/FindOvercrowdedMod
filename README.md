# FindOvercrowded Mod

Locates and highlights public transport stops that are overcrowded.

![GIF](https://github.com/Prazman/FindOvercrowdedMod/blob/master/FindOvercrowdedgif.gif)

## Usage
Press **Ctrl+SHIFT+o** in-game to highlight all overcrowded stops. Press **ESC** or **Ctrl+SHIFT+o** again to remove stop highlighting.

## When is a public transport stop considered overcrowded?
A public transport stop is considered overcrowded when the number of passengers waiting exceeds the capacity of one vehicle of the line.
It should work with all public transport types and vehicles, as long as vehicles have a defined capacity.

## Possible improvements
- Shortcut configuration in mod settings
- Also highlight vehicles at full capacity
- Add a UI panel to run the search for overcrowded stops from a button. Loop through overcrowded stops with prev/next buttons to locate them on the map
- More highlighting levels (ex: green => 0 to 50% capacity, orange for 50% to 100%, red capacity exceeded)

