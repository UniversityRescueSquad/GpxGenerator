# GpxGenerator

This project devides a given land area into smaller squares in order to coduct full-scale search and rescue operations.

## Description

University Rescue Squad is a first responders rescue organization in Bulgaria that conducts search and rescue (SaR) operations in all kinds of natural terain. In order to guarantee and ensure a complete and thorough search of a particular area, rescuers are splitting this area into smaller quadrants that are later used for better navigation, more specific task setting for different SaR teams and easier tracking of what areas have already been searched. This project is meant to help SaR operaions by prociding a .gpx file with a grid of quadrants in a given area where rescuers need to work. This .gpx file is after that sent to GPS devices and used for communications with operational headquatters.

## Installation

An executable file can be downloaded from here.

## Usage

Upon opening the GpxGenerator.exe file a console should pop-up. It would expect a long/lat inpit (in Decimal degreess), width and height of the searched area and quadrant width.
![Console](imgs/console.png)


It will then create two .gpx files in the directory of the .exe file - grid.gpx and grid_with_names.gpx.
![Map](imgs/map_snipped.png)


grid.gpx will contain a grid of quadrants (routes) that split the area in equal squares. 
![Map with grid](imgs/map_snipped_with_grid.png)


grid_with_names.gpx will contain the same grid with additional GPS points in the down left corner of each quadrant. The points names will correspond to the quadrant name.
![Map with grid and quadrant names](imgs/map_snipped_with_grid_and_names.png)

## Limitations

The project can only work with Decaimal degrees lat/long format.

## License

MIT License

Copyright (c) [year] [author]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

## Acknowledgments

The project is usually used with the map provided by https://bgmountains.org/bg/ - https://map.bgmountains.org/.