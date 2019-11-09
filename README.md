Recursive knight (rknight)
====

Exercise in recursive programming from my first university courses back in the day solving a knight's journey across a chess board. It must visit each square once and only once.

Five implementations available
* C# - .NET Core
* TypeScript for node
* Go
* Python
* C

The applications take three optional parameters:
1. Size of the board itself.
2. Start column number. 0 = A, 1 = B, etc.
3. Start row number 0 = 1.

Default is a 7x7 board, and the starting position is A1. I.e. the equivalent of `7 0 0` as command line arguments.


.NET Core 
----

How to build and run:

```
cd csharp
dotnet run -c Release [arguments]
```

TypeScript
---

Build:

```
cd js
npm i
npm run build
```

Run:

```
node dist/main.js [arguments]
```
or
```
npm run [arguments]
```

Go
----

How to build and run:

```
cd golang
go build rknight.go
./rknight [arguments]
```

Python
------

In Python, all three arguments must be supplied. Argparse requires positional
arguments to always be there.

```
cd python
python rknight.py 7 0 0
```

C
-

```
cd c
gcc -O2 -o rknight *.c
./rknight
```

Informal performance comparison
===============================

To find a solution on an 8x8 board takes a lot of time. Too long
some would say. A more suitable performance test would be on an
7x7 board starting from B2.

The current tests are indicative. Only run once on a Dell XPS laptop with an
Intel i7-7500U 2.7 GHz with Ubuntu 18.04.3 LTS.

Timing was done with `time`. Reported running time is the resulting *User*
contribution. It thus includes some startup time and other non-productive
tasks, which seemed to be mostly significant for .NET, but that's how the 
programs will be run in real life and thus relevant from a user's perspective.

| Language   | Compiler/version  | Time [sec] | Optimized [sec] | Relative |
|------------|-------------------|-----------:|----------------:|---------:|
| C          | GCC 7.4           |   3.94     |                 |     1.00 |
| Go         | 1.10.4            |   4.61     |                 |     1.17 |
| C#         | .NET Core 3.0.100 |   6.44     |                 |     1.63 |
| TypeScript | 3.5.3, Node 10.16.0 |   8.55   |                 |     2.17 |
| Python     | 2.7.15+ (GCC 7.4) | 281.21     | 225.35          |    57.20 |
| Python     | 3.6.8 (GCC 8.3)   | 322.60     | 249.05          |    63.21 |
| Python     | 3.7.5 (GCC 7.3)   | 350.43     | 259.17          |    65.78 |

Relative numbers are multiple times of the fastest implementation; C compiled
with optimization option -O3. For Python the relative metrics are based on the
"optimized" Python implementation without a KnightSolver class.
